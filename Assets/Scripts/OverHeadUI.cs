using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHeadUI : MonoBehaviour
{
    [SerializeField] HpUI hpUi;
    [SerializeField] DiamondUI diamondUi;

    public void ModifyHp(float value)
    {
        hpUi.Modify(value);
    }

    public void SetGem(int slot, Gem gemType)
    {
        diamondUi.SetGem(slot, gemType);
    }

    public void SetGems(Gem[] gemTypes)
    {
        diamondUi.SetGems(gemTypes);
    }

}
