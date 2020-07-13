using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseData",
    menuName = "BaseData",
    order = 1)]
public class CharacterBaseData : ScriptableObject
{
    public string c_name;
    public string description;
}
