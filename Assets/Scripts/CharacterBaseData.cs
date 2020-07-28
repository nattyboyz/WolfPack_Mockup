using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseData",
    menuName = "BaseData",
    order = 1)]
public class CharacterBaseData : ScriptableObject
{
    [SerializeField] private string c_name;
    [SerializeField] private string description;

    public string C_name { get => c_name;}
    public string Description { get => description;}
}
