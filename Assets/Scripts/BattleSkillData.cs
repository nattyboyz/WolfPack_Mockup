using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleSkillData", menuName = "BattleSkillData", order = 1)]
public class BattleSkillData : ScriptableObject
{
    [SerializeField] private string skillName = "skill";
    [SerializeField] private int sp = 1;
    [SerializeField] private MinMax damage;
    [SerializeField] private string lore = "this is a very powerful skill";
    [SerializeField] private string description = "Take gx2 for 2 turn";

    public string SkillName { get => skillName; set => skillName = value; }
    public int Sp { get => sp; set => sp = value; }
    public string Lore { get => lore; set => lore = value; }
    public string Description { get => description; set => description = value; }
    public MinMax Damage { get => damage; set => damage = value; }
}
