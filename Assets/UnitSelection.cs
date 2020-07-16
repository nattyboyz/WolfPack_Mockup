using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TargetMode { Single, Team, All }

public class UnitSelection : MonoBehaviour
{
    bool activeSelectTarget = false;

    [SerializeField] List<BattleCharacterSlot> allySlots;
    [SerializeField] List<BattleCharacterSlot> enemySlots;
    [SerializeField] List<BattleCharacterSlot> allSlot;

    [SerializeField] List<BattleCharacterSlot> selected_slots;

    int oldTarget = 0;
    int currentTeam = 0;


    [SerializeField] GameObject highlight;
    public Action<List<BattleCharacterSlot>> onSelect;
    public Action<List<BattleCharacterSlot>> onSubmit;
    public Action onCancel;

    TargetMode mode;

    public void InitSelection()
    {
       //allSlot = new List<BattleCharacterSlot>(allySlots);
        //allSlot.AddRange(enemySlots);//Add all posible slot into account
    }

    public void ActiveUnitSelection(TargetMode mode, int startValue)
    {
        Debug.Log("ActiveUnitSelection");
        StartCoroutine(ieActiveUnitSelection(mode, startValue));
    }

    IEnumerator ieActiveUnitSelection(TargetMode mode, int startValue)
    {
        //Set default
        if (mode == TargetMode.Single)
        {
            Select(new List<BattleCharacterSlot> { allSlot[startValue] });
            oldTarget = startValue;
        }
        else
        {
            if (startValue == 0)
                Select(allySlots);
            else
                Select(enemySlots);

            currentTeam = startValue;
        }

        yield return new WaitForEndOfFrame();
        this.mode = mode;
        activeSelectTarget = true;
    }

    public void Submit()
    {
        onSubmit?.Invoke(selected_slots);
        Deselect(selected_slots);
        activeSelectTarget = false;

    }

    private void Update()
    {
        if (activeSelectTarget)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (mode == TargetMode.Single) TargetShift(-1);
                else GroupShift(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {

                if (mode == TargetMode.Single) TargetShift(1);
                else GroupShift(1);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                Submit();
                //Deselect(selected_slots);
                //activeSelectTarget = false;
            }
            else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Escape))
            {
                onCancel?.Invoke();
                Deselect(selected_slots);
                selected_slots.Clear();
                activeSelectTarget = false;
            }
        }
    }

    //-1 +1
    public void TargetShift(int mod)
    {
        int target = oldTarget;
        BattleCharacter character = null;
        do
        {
            if (target + mod >= allSlot.Count) target = 0;
            else if (target + mod < 0) target = allSlot.Count - 1;
            else target += mod;
            character = allSlot[target].Character;

        } while (character == null || character.Data.Battle.isDead);

        Deselect(new List<BattleCharacterSlot>() { allSlot[oldTarget] });
        Select(new List<BattleCharacterSlot>() { allSlot[target] });

        oldTarget = target; 
        //Debug.Log("Select " + character.Data.Base.c_name);
    }

    public void GroupShift(int mod)
    {
        if (currentTeam + mod > 1) currentTeam = 0;
        else if (currentTeam + mod < 0) currentTeam = 1;
        else currentTeam += mod;

        if (currentTeam == 0)
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

    public void SelectAll()
    {


    }

    public void Select(List<BattleCharacterSlot> slots/*, rule*/)
    {
        selected_slots.Clear();
        onSelect?.Invoke(slots);
        foreach (BattleCharacterSlot slot in slots)
        {
            if (slot.Character != null)
            {
                slot.Character.Focus(true);
                slot.Highlight(true);
                selected_slots.Add(slot);
            }
        }
    }

    public void Deselect(List<BattleCharacterSlot> slots/*, rule*/)
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
    #region Test button functions

    public void Btn_SelectTarget()
    {
        mode = TargetMode.Single;
        activeSelectTarget = true;
    }

    public void Btn_SelectTeam()
    {
        mode = TargetMode.Team;
        activeSelectTarget = true;
    }

    public void Btn_SelectAll()
    {
        mode = TargetMode.All;
        activeSelectTarget = true;
    }

    #endregion

}

public class TargetOption
{
    public TargetMode mode;
}