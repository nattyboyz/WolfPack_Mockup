using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] CharacterBaseData baseData;
    UnitBattleData battleData;
    [SerializeField] PortraitData portraitData;
    [SerializeField] CharacterStats stats;


    public UnitBattleData Battle { get => battleData; set => battleData = value; }
    public CharacterBaseData Base { get => baseData; set => baseData = value; }
    public PortraitData Portrait { get => portraitData; set => portraitData = value; }
    public CharacterStats Stats { get => stats; set => stats = value; }

    public void Start()
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

    public SkillData[] skills;

    public UnitBattleData BattleData()
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
        data.isDead = this.isDead;

        return data;

    }

}

public class SkillSet
{
    List<SkillData> skills = new List<SkillData>();
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
    public bool isDead;

    public int target_slot = 0;

    public void ModifyHp()
    {
      
    }

    public void ModifySp()
    {

    }
}