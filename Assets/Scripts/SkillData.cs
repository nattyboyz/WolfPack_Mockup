using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "SkillData", order = 1)]
public class SkillData : ScriptableObject
{
    [SerializeField] private string skillName = "skill";
    [SerializeField] private int sp = 5;
    [SerializeField] private Gem[] gems = new Gem[4] { Gem.None, Gem.None, Gem.None, Gem.None };
    [SerializeField] private string lore = "this is a very powerful skill";
    [SerializeField] private string description = "Take gx2 for 2 turn";

    public string SkillName { get => skillName; set => skillName = value; }
    public int Sp { get => sp; set => sp = value; }
    public Gem[] Gems { get => gems; set => gems = value; }
    public string Lore { get => lore; set => lore = value; }
    public string Description { get => description; set => description = value; }
}
