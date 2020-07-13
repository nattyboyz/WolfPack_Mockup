using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PortraitDataDictionary : SerializableDictionary<string, Sprite> { }

[CreateAssetMenu(fileName = "PortraitData",
    menuName = "PortraitData",
    order = 1)]
public class PortraitData : ScriptableObject
{
    [SerializeField] PortraitDataDictionary sprites;
    public PortraitDataDictionary Sprites { get => sprites;}
}
