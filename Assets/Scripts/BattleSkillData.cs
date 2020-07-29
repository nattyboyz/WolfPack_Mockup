using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleSkillData", menuName = "BattleSkillData", order = 1)]
public class BattleSkillData : ScriptableObject
{
    [SerializeField] private string skillName = "skill";
    [SerializeField] private int ap = 1;
    [SerializeField] private MinMax damage;
    [SerializeField] private string lore = "this is a very powerful skill";
    [SerializeField] private string description = "Take gx2 for 2 turn";
    [SerializeField] TargetOption targetOption;

    public string SkillName { get => skillName; set => skillName = value; }
    public int Ap { get => ap; set => ap = value; }
    public string Lore { get => lore; set => lore = value; }
    public string Description { get => description; set => description = value; }
    public MinMax Damage { get => damage; set => damage = value; }
    public TargetOption TargetOption { get => targetOption; set => targetOption = value; }
}
