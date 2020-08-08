using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public enum TargetMode { Single, Team, All }
public enum TargetFilter { None, Allies, Enemy, Any}

public class UnitSelection : MonoBehaviour
{
    bool activeSelectTarget = false;

    [SerializeField] List<BattleCharacterSlot> allySlots;
    [SerializeField] List<BattleCharacterSlot> enemySlots;
    [SerializeField] List<BattleCharacterSlot> allSlot;

    [SerializeField] List<BattleCharacterSlot> selected_slots;

    [SerializeField] GameObject highlight;
    public Action<List<BattleCharacterSlot>, int> onSelect;
    public Action<List<BattleCharacterSlot>> onSubmit;
    public Action onExit;

    BattleCharacter owner;
    TargetOption option;

   // TargetMode mode;
    int selected_index = 0;

    [SerializeField] protected MainControl input;
    protected Vector2 navigate;

    protected virtual void Awake()
    {
        input = new MainControl();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Enable()
    {
        input.UI.Navigate.started += Navigate_started;
        input.UI.Navigate.canceled += Navigate_canceled;
        input.UI.Navigate.performed += Navigate_performed;
        input.UI.Submit.performed += Submit_performed;
        input.UI.Cancel.performed += Cancel_performed;

        input.UI.Navigate.Enable();
        input.UI.Submit.Enable();
        input.UI.Cancel.Enable();
    }

    protected virtual void Disable()
    {
        input.UI.Navigate.started -= Navigate_started;
        input.UI.Navigate.canceled -= Navigate_canceled; ;
        input.UI.Navigate.performed -= Navigate_performed;
        input.UI.Submit.performed -= Submit_performed;
        input.UI.Cancel.performed -= Cancel_performed;

        input.UI.Navigate.Disable();
        input.UI.Submit.Disable();
        input.UI.Cancel.Disable();
    }

    protected virtual void Navigate_started(InputAction.CallbackContext obj)
    {

    }

    protected virtual void Navigate_canceled(InputAction.CallbackContext obj)
    {

    }

    protected virtual void Navigate_performed(InputAction.CallbackContext obj)
    {
        navigate = obj.ReadValue<Vector2>();
        if (navigate.x > 0 || navigate.y < 0)
        {
            if (this.option.mode == TargetMode.Single) TargetShift(1);
            else GroupShift(1);
        }
        else
        {
            if (this.option.mode == TargetMode.Single) TargetShift(-1);
            else GroupShift(-1);
        }
    }

    protected virtual void Submit_performed(InputAction.CallbackContext obj)
    {
        Disable();
        Submit();
    }

    protected virtual void Cancel_performed(InputAction.CallbackContext obj)
    {
        Disable();
        activeSelectTarget = false;
        Deselect(selected_slots);
        selected_slots.Clear();
        onExit?.Invoke();
    }

    public void Active(BattleCharacter owner, TargetOption option, int startValue)
    {
        Enable();
        StartCoroutine(ieActive(owner, option, startValue));
    }

    IEnumerator ieActive(BattleCharacter owner, TargetOption option, int startValue)
    {
        this.option = option;
        this.owner = owner;
        yield return new WaitForEndOfFrame();

        if (option.mode == TargetMode.Single)
        {
            selected_index = TryGetIndex(startValue);
            Select(new List<BattleCharacterSlot> { allSlot[selected_index] });
        }

        yield return new WaitForEndOfFrame();

        activeSelectTarget = true;
    }

    public void Deactive()
    {

    }

    public void Submit()
    {
        Deselect(selected_slots);
        activeSelectTarget = false;
        onSubmit?.Invoke(selected_slots);
    }

    private void LateUpdate()
    {
        //if (activeSelectTarget)
        //{
        //    if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        //    {
        //        if (this.option.mode == TargetMode.Single) TargetShift(-1);
        //        else GroupShift(-1);
        //    }
        //    else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        //    {

        //        if (this.option.mode == TargetMode.Single) TargetShift(1);
        //        else GroupShift(1);
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        Submit();
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Escape) || (Input.GetMouseButtonDown(1)))
        //    {
        //        activeSelectTarget = false;
        //        Deselect(selected_slots);
        //        selected_slots.Clear();
        //        StartCoroutine(ieExit());
        //    }
        //}
    }

    //public IEnumerator ieExit()
    //{
    //    //yield return new WaitForSeconds(0.2f);
    //    yield return new WaitForEndOfFrame();
    //    onExit?.Invoke();
    //}

    //-1 +1
    public void TargetShift(int mod)
    {
        int idx = selected_index;
        if (selected_index < 0) idx = selected_index = 0;

        BattleCharacter target = null;
        int time = 0;
        do
        {
            if (time > allSlot.Count)//Prevent infinity loop
            {
                //Deselect(new List<BattleCharacterSlot>() { allSlot[selected_index] });
                return;
            }
            if (idx + mod >= allSlot.Count) idx = 0;
            else if (idx + mod < 0) idx = allSlot.Count - 1;
            else idx += mod;
            target = allSlot[idx].Character;
            time++;

        } while (!IsMatch(this.owner,target,this.option));

        

        Deselect(new List<BattleCharacterSlot>() { allSlot[selected_index] });

        selected_index = idx;
        Select(new List<BattleCharacterSlot>() { allSlot[selected_index] });

    
        //Debug.Log("Select " + character.Data.Base.c_name);
    }

    int TryGetIndex(int idx)
    {
        //BattleCharacter target = allSlot[idx].Character;
        int iteration = 0;

        while (!IsMatch(this.owner, allSlot[idx].Character, this.option))
        {
            if (iteration > allSlot.Count)//Prevent infinity loop
            {
                return idx;
            }

            idx++;
            iteration++;
            if (idx >= allSlot.Count) idx = 0;
            else if (idx < 0) idx = allSlot.Count - 1;
        }

        return idx;
    }

    public void GroupShift(int mod)
    {
        if (selected_index + mod > 1) selected_index = 0;
        else if (selected_index + mod < 0) selected_index = 1;
        else selected_index += mod;

        if (selected_index == 0)
        {
            Deselect(enemySlots);
            Select(allySlots);
        }
        else
        {
            Deselect(allySlots);
            Select(enemySlots);
         
        }
    }

    void Select(List<BattleCharacterSlot> slots)
    {
        selected_slots.Clear();

        //else onSelect?.Invoke(slots, selected_index);

        foreach (BattleCharacterSlot slot in slots)
        {
            if (slot.Character != null)
            {
                slot.Character.Focus(true);
                slot.Highlight(true);
                selected_slots.Add(slot);
            }
        }
        onSelect?.Invoke(selected_slots, selected_index);
    }

    /// <summary>
    /// Select previous targets either single or group
    /// </summary>
    public void SelectPrevious()
    {
        Enable();
        foreach (BattleCharacterSlot slot in selected_slots)
        {
            if (slot.Character != null)
            {
                slot.Character.Focus(true);
                slot.Highlight(true);
                //selected_slots.Add(slot);
            }
        }

        onSelect?.Invoke(selected_slots, selected_index);
        activeSelectTarget = true;
    }

    public void Deselect(List<BattleCharacterSlot> slots)
    {
        foreach (BattleCharacterSlot slot in slots)
        {
            if (slot.Character != null)
            {
                //Debug.Log("Deselect " + slot.Character.Data.Base.c_name);
                slot.Highlight(false);
                slot.Character.Focus(false);
            }
        }
    }

    static bool IsMatch(BattleCharacter owner, BattleCharacter target, TargetOption option)
    {
        if (target == null) return false;
        if (target.Data.Battle.isDead) return false;

        //Debug.Log("[Match] " + owner.name + " " + target.name + option.selfOnly + " " + option.allowSelf);

        if (option.selfOnly) {
            if (target == owner) return true;
            else return false;
        }

        if (!option.allowSelf && target == owner) return false;

        if (option.filter == TargetFilter.Allies && (owner.Type != target.Type)) return false;

        if (option.filter == TargetFilter.Enemy && (owner.Type == target.Type)) return false;
        return true;
    }

}

[Serializable]
public class TargetOption
{
    public TargetMode mode;
    public TargetFilter filter;
    public bool allowSelf;
    public bool selfOnly;
}