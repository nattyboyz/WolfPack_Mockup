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

    int selected_index = 0;
    int currentTeam = 0;

    [SerializeField] GameObject highlight;
    public Action<List<BattleCharacterSlot>, int> onSelect;
    public Action<List<BattleCharacterSlot>> onSubmit;
    public Action onExit;

    TargetMode mode;

    public void Active(TargetMode mode, int startValue)
    {
        //Debug.Log("ActiveUnitSelection");
        StartCoroutine(ieActive(mode, startValue));
    }

    IEnumerator ieActive(TargetMode mode, int startValue)
    {
        //Set default
        if (mode == TargetMode.Single)
        {
            Select(new List<BattleCharacterSlot> { allSlot[startValue] });
            selected_index = startValue;
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
        Deselect(selected_slots);
        activeSelectTarget = false;
        onSubmit?.Invoke(selected_slots);
    }

    private void LateUpdate()
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
            }
            else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Escape))
            {
                activeSelectTarget = false;
                Deselect(selected_slots);
                selected_slots.Clear();
                //onExit?.Invoke();
                StartCoroutine(ieExit());
            }
        }
    }

    public IEnumerator ieExit()
    {
        yield return new WaitForSeconds(0.2f);
        onExit?.Invoke();
    }

    //-1 +1
    public void TargetShift(int mod)
    {
        int target = selected_index;
        if (selected_index < 0) target = selected_index = 0;

        BattleCharacter character = null;
        int time = 0;
        do
        {
            if (time > allSlot.Count)
            {
                Deselect(new List<BattleCharacterSlot>() { allSlot[selected_index] });
                return;
            }
            if (target + mod >= allSlot.Count) target = 0;
            else if (target + mod < 0) target = allSlot.Count - 1;
            else target += mod;
            character = allSlot[target].Character;
            time++;

        } while (character == null || character.Data.Battle.isDead);

        Deselect(new List<BattleCharacterSlot>() { allSlot[selected_index] });

        selected_index = target;
        Select(new List<BattleCharacterSlot>() { allSlot[target] });

    
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

    public void Select(List<BattleCharacterSlot> slots/*, rule*/)
    {
        selected_slots.Clear();
        onSelect?.Invoke(slots, selected_index);
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