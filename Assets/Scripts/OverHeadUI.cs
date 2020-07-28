using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OverHeadUI : MonoBehaviour
{
    [SerializeField] HpUI hpUi;
    [SerializeField] DiamondUI diamondUi;
    [SerializeField] DiamondPreview diamondSelection;

    public DiamondUI DiamondUi { get => diamondUi; }
    public DiamondPreview DiamondSelection { get => diamondSelection;}
    public HpUI HpUi { get => hpUi;}

    public void Active(bool value)
    {
        gameObject.SetActive(value);
    }

    public void ModifyHp(float value)
    {
        hpUi.Modify(value);
    }

    public IEnumerator ieModifyHp(float value)
    {
        yield return hpUi.ieModify(value);
    }


    public void SetGem(int slot, Gem gemType)
    {
        diamondUi.SetGem(slot, gemType);
    }

    public void SetGems(Gem[] gemTypes)
    {
        diamondUi.SetGems(gemTypes);
    }

    public void ChooseGemSlot(Gem[] gemTypes,
        Action<Dictionary<int, Gem>> onSubmit, 
        Action onCancel)
    {
        diamondSelection.onCancel = onCancel;
        diamondSelection.onSubmit = onSubmit;
        diamondSelection.StartChoose(gemTypes, 0);
        diamondSelection.Active(true);
    }

}
