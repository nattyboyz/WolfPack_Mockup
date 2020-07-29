using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActSkillData", menuName = "ActSkillData", order = 1)]
public class ActSkillData : ScriptableObject
{
    [SerializeField] private string skillName = "skill";
    [SerializeField] private int ap = 5;
    [SerializeField] private Gem[] gems = new Gem[4] { Gem.None, Gem.None, Gem.None, Gem.None };
    [SerializeField] private string lore = "this is a very powerful skill";
    [SerializeField] private string description = "Take gx2 for 2 turn";
    [SerializeField] TargetOption targetOption;

    public string SkillName { get => skillName; set => skillName = value; }
    public int Ap { get => ap; set => ap = value; }
    public Gem[] Gems { get => gems; set => gems = value; }
    public string Lore { get => lore; set => lore = value; }
    public string Description { get => description; set => description = value; }
    public TargetOption TargetOption { get => targetOption; set => targetOption = value; }
}
