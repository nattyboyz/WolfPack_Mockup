﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Team { Player,CPU}

public class BattleCharacter : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] Team type;
    [SerializeField] string id;
    [SerializeField] CharacterData characterData;
    [Header("Graphic")]
    [SerializeField] GameObject graphic;
    [SerializeField] CharacterSpine characterSpine;
    [Header("UI")]
    [SerializeField] OverHeadUI overheadUI;


    public CharacterData Data { get => characterData;}
    public OverHeadUI OverheadUI { get => overheadUI;}
    public Team Type { get => type; set => type = value; }
    public CharacterSpine CharacterSpine { get => characterSpine; set => characterSpine = value; }

    private void Awake()
    {
        overheadUI.Active(false);
    }

    public void Init()
    {
        Data.InitBattleData();
        overheadUI.SetGems(Data.Battle.gems);
    }

    public void Focus(bool active)
    {
        overheadUI.Active(active);
        float sign = Mathf.Sign(graphic.transform.localScale.x);
        if (active)
        {
          
            graphic.transform.localScale = new Vector3(graphic.transform.localScale.x + (sign*0.1f), graphic.transform.localScale.y +  0.1f, 1f);
        }
        else
        {
            graphic.transform.localScale = new Vector3(graphic.transform.localScale.x - (sign * 0.1f), graphic.transform.localScale.y -  0.1f, 1f);
        }
    }

    public void TakeGemDamage(Gem gem)
    {
        Gem[] g = Data.Battle.gems;
        //Util.Shuffle(g);
        int start = UnityEngine.Random.Range(0, 4);

        for (int i = 0; i < g.Length; i++)
        {
            int idx = (start + i);
            if (idx > g.Length) idx -= g.Length;
            Debug.Log("Set index " + idx + " to " + gem);

            if (g[idx] != gem)
            {
                g[idx] = gem;
                overheadUI.SetGem(idx, gem);
                break;
            }
        }
    }

    public IEnumerator ieTakeGemDamage(Dictionary<int,Gem> gemSlots)
    {
        foreach(KeyValuePair<int,Gem> kvp in gemSlots)
        {
            Data.Battle.gems[kvp.Key] = kvp.Value;
            //overheadUI.SetGem(kvp.Key, kvp.Value);
        }
        yield return overheadUI.DiamondUi.ieSetGems(gemSlots);
    }
}
