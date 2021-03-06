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
    [SerializeField] UnitStatsUI unitStatUI;


    public CharacterData Data { get => characterData;}
    public OverHeadUI OverheadUI { get => overheadUI;}
    public Team Type { get => type; set => type = value; }
    public CharacterSpine CharacterSpine { get => characterSpine; set => characterSpine = value; }
    public UnitStatsUI UnitStatUI { get => unitStatUI; set => unitStatUI = value; }

    //Event
    public Action<HpModifierData> onModifyHp;
    public Action<float> onModifyAp;
    public Action<Dictionary<int, Gem>> onModifyGems;
    public Action<BattleCharacter> onDead;
    public Action onGiveUp;

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
        //overheadUI.Active(active);
        float sign = Mathf.Sign(graphic.transform.localScale.x);
        if (active)
        {
            Debug.Log("Focus true");
            //graphic.transform.localScale = new Vector3(graphic.transform.localScale.x + (sign*0.1f), graphic.transform.localScale.y +  0.1f, 1f);
            overheadUI.Parent.transform.localScale = 
                new Vector3(overheadUI.Parent.transform.localScale.x + (0.2f),
                overheadUI.Parent.transform.localScale.y + (0.2f), 1f);
        }
        else
        {
            Debug.Log("Focus false");
            //graphic.transform.localScale = new Vector3(graphic.transform.localScale.x - (sign * 0.1f), graphic.transform.localScale.y -  0.1f, 1f);
            overheadUI.Parent.transform.localScale = 
                new Vector3(overheadUI.Parent.transform.localScale.x - (0.2f),
                overheadUI.Parent.transform.localScale.y - (0.2f), 1f);
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

    public IEnumerator ieModifyGems(Dictionary<int,Gem> gemSlots)
    {
        foreach(KeyValuePair<int,Gem> kvp in gemSlots)
            Data.Battle.gems[kvp.Key] = kvp.Value;

        onModifyGems?.Invoke(gemSlots);

        yield return unitStatUI.ieModifyGems(gemSlots);
        yield return overheadUI.DiamondUi.ieModifyGems(gemSlots);
    }

    public IEnumerator ieModifyHp(HpModifierData hpMod)
    {
        float from = Data.Battle.hp;
        float value = hpMod.value;
        if (hpMod.type == HpModifierData.Type.Damage) value *= -1;
        ModifyHp(value);

        yield return overheadUI.ieModifyHp(value);
        yield return unitStatUI.ieModifyHp(from, value);
        if (characterData.Battle.isDead) onDead?.Invoke(this);
        //yield return new WaitForSeconds(0.2f);
    }

    void ModifyHp(float value)
    {      
        if (characterData.Battle.hp + value <= 0)
        {
            characterData.Battle.hp = 0;
            characterData.Battle.isDead = true;
        }
        else if (characterData.Battle.hp + value >= characterData.Battle.maxHp)
        {
            characterData.Battle.hp = characterData.Battle.maxHp;
        }
        else
        {
            characterData.Battle.hp += value;
        }
    }

    void ModifyAp(float value)
    {
        unitStatUI.ModifyAp(value);
        if (characterData.Battle.ap + value <= 0)
        {
            characterData.Battle.ap = 0;
        }
        else if (characterData.Battle.ap + value >= characterData.Battle.maxAp)
        {
            characterData.Battle.ap = characterData.Battle.maxHp;
        }
        else
        {
            characterData.Battle.ap += value;
        }
    }

    public void Dead()
    {
        this.gameObject.SetActive(false);
    }
}
