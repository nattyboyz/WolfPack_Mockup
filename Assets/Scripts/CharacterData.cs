﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] CharacterBaseData baseData;
    UnitBattleData battleData;
    [SerializeField] PortraitData portraitData;
    [SerializeField] CharacterStats stats;


    public UnitBattleData BattleData { get => battleData; set => battleData = value; }
    public CharacterBaseData BaseData { get => baseData; set => baseData = value; }
    public PortraitData PortraitData { get => portraitData; set => portraitData = value; }

    public void Start()
    {
        BattleData = stats.Clone();
    }
}

[Serializable]
public class CharacterStats
{
    public float hp;
    public float maxHp;
    public float sp;
    public float maxSp;
    public int stack;
    public int level;
    public int attack;
    public int defense;
    public int speed;


    public UnitBattleData Clone()
    {
        UnitBattleData data = new UnitBattleData();
        data.hp = this.hp;
        data.maxHp = this.maxHp;
        data.sp = this.sp;
        data.maxSp = this.maxSp;
        data.gemStack = this.stack;
        data.level = this.level;
        data.attack = this.attack;
        data.defense = this.defense;
        data.speed = this.speed;
        data.gems = new GemType[4] {
            GemType.None,
            GemType.None,
            GemType.None,
            GemType.None };
        return data;

    }
}

[Serializable]
public class UnitBattleData
{
    public float hp;
    public float maxHp;
    public float sp;
    public float maxSp;

    public int level;
    public int attack;
    public int defense;
    public int speed;

    public GemType[] gems;
    public int gemStack = 0;
    public string emote = "idle";
}