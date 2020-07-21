using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatsUIController : MonoBehaviour
{
    public enum Side { Left,Right}
    [SerializeField] UnitStatsUI leftUI;
    [SerializeField] UnitStatsUI rightUI;

    public void Show(CharacterData data, Side side)
    {
        if(side == Side.Left)
        {
            leftUI.gameObject.SetActive(true);
            leftUI.SetData(data);
        }
        else
        {
            rightUI.gameObject.SetActive(true);
            rightUI.SetData(data);
        }
    }

    public void Clear(Side side)
    {
        if (side == Side.Left)
        {
            leftUI.gameObject.SetActive(false);
        }
        else
        {
            rightUI.gameObject.SetActive(false);
        }
    }

    public void AddGem(Side side,int slot, Gem gemType)
    {
        UnitStatsUI ui;
    }

    public void RemoveGem(Side side,int slot)
    {
        UnitStatsUI ui;
    }

    public void DiamondBreak(Side side,Gem gemType)
    {
        UnitStatsUI ui;
    }

    public void SetHp(Side side,float hp)
    {
        UnitStatsUI ui;
    }

    public void SetSp(Side side, float sp)
    {
        UnitStatsUI ui;
        

    }

}
