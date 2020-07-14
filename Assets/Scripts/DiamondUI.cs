using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiamondUI : MonoBehaviour
{
    //[SerializeField] int amount;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GemUI[] gems;

    public void SetGem(GemType[] gemType)
    {
       for(int i = 0; i < gemType.Length; i++)
        {
            gems[i].Set(gemType[i]);
        }
    }

    public void SetGem(int slot, GemType gemType)
    {
        gems[slot].Set(gemType);
    }

    public void SetAmount(int amount)
    {
        text.text = amount.ToString();
    }

    //public void Add(GemType gem)
    //{
    //    GemType target;
    //    bool found = false;
    //    for (int i = 0; i< gems.Length; i++)
    //    {
    //        if(gems[i] == GemType.None)
    //        {
    //            target = gems[i];
    //            found = true;
    //            continue;
    //        }
    //    }

    //    if (!found)
    //    {
    //        for(int i = 0; i<)
    //    }


    //}



}
