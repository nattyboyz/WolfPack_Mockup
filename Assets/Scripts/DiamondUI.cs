using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class DiamondUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] protected GemUI[] gems;
    [SerializeField] protected GemUI[] animationGems;


    public GemUI[] Gems { get => gems; set => gems = value; }

    public void SetGems(Gem[] gemType)
    {
       for(int i = 0; i < gemType.Length; i++)
        {
            gems[i].Set(gemType[i]);
        }
    }

    public void SetGem(int slot, Gem gemType)
    {
        gems[slot].Set(gemType);
    }

    public void SetAmount(int amount)
    {
        text.text = amount.ToString();
    }

    //Set gem with animation@!
    public IEnumerator ieModifyGems(Dictionary<int, Gem> gemSlots)
    {
        List<int> ind = new List<int>();
        foreach (KeyValuePair<int, Gem> kvp in gemSlots)
        {
            GemUI gemUI = animationGems[kvp.Key];
            gemUI.Set(kvp.Value);
            gemUI.gameObject.SetActive(true);
            gemUI.transform.position = 
                new Vector3(gems[kvp.Key].transform.position.x,
                gems[kvp.Key].transform.position.y + .5f,
                gems[kvp.Key].transform.position.z);
            gemUI.transform.DOMoveY(gems[kvp.Key].transform.position.y,0.2f);
            ind.Add(kvp.Key);
        }

        yield return new WaitForSeconds(0.2f);
        yield return ieGemBreak(ind);

        foreach (KeyValuePair<int, Gem> kvp in gemSlots)
        {
            SetGem(kvp.Key, kvp.Value);
            animationGems[kvp.Key].gameObject.SetActive(false);
        }
    }

    public IEnumerator ieGemBreak(List<int> indices)
    {
        yield return null;
        for(int i =0;i< indices.Count; i++)
        {
            gems[indices[i]].Break();
        }
        yield return new WaitForSeconds(0.3f);
        Debug.Log("Break");
        
    }

    public IEnumerator ieSetGemsBreak(Dictionary<int, Gem> gemSlots)
    {
        yield return ieModifyGems(gemSlots);
        //yield return
        
    }

    //public static int GetEmptySlot(Gem[] gems)
    //{
    //    for(int i = 0; i < gems.Length; i++)
    //    {
    //        if (gems[i] == Gem.None) return i;
    //    }
    //    return -1;
    //}

    public static int GetSlotExclude(Gem[] gems, Gem exclude)
    {
        for (int i = 0; i < gems.Length; i++)
        {
            if (gems[i] != exclude) return i;
        }
        return -1;
    }

    public static int GetSlotInclude(Gem[] gems, Gem include)
    {
        for (int i = 0; i < gems.Length; i++)
        {
            if (gems[i] != include) return i;
        }
        return -1;
    }
}
