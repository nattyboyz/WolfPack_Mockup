﻿using System.Collections;
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

    public void SetGem(int slot, GemType gemType)
    {
        diamondUi.SetGem(slot, gemType);
    }

}
