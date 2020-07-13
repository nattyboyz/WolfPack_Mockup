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

    public void ClearFocus(Side side)
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

}
