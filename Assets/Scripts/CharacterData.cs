using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] CharacterBaseData baseData;
    [SerializeField] UnitBattleData battleData;
    [SerializeField] PortraitData portraitData;
    [SerializeField] CharacterStats stats;

    public UnitBattleData Battle { get => battleData; set => battleData = value; }
    public CharacterBaseData Base { get => baseData; set => baseData = value; }
    public PortraitData Portrait { get => portraitData; set => portraitData = value; }
    public CharacterStats Stats { get => stats; set => stats = value; }

    public void InitBattleData()
    {
        Battle = stats.BattleData();
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
    public bool isDead = false;

    public ActSkillData[] actSkills;
    public BattleSkillData[] battleSkills;

    public UnitBattleData BattleData()
    {
        UnitBattleData data = new UnitBattleData();
        data.hp = this.hp;
        data.maxHp = this.maxHp;
        data.ap = this.sp;
        data.maxAp = this.maxSp;
        data.gemStack = this.stack;
        data.level = this.level;
        data.attack = this.attack;
        data.defense = this.defense;
        data.speed = this.speed;
        data.gems = new Gem[4] {
            Gem.None,
            Gem.None,
            Gem.None,
            Gem.None };
        data.isDead = this.isDead;

        return data;
    }

}

public class SkillSet
{
    List<ActSkillData> skills = new List<ActSkillData>();
}

[Serializable]
public class UnitBattleData
{
    public float hp;
    public float maxHp;
    public float ap;
    public float maxAp;

    public int level;
    public int attack;
    public int defense;
    public int speed;

    public Gem[] gems;
    public int gemStack = 0;
    public string emote = "idle";
    public bool isDead = false;

    //UI cache data 
    public int ui_lastAction = 0;
    public int ui_lastTarget = 0;
    public int ui_lastAct = 0;
    public int ui_lastAttack = 0;

}