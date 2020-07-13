using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Team { Player,CPU}

public class BattleCharacter : MonoBehaviour
{
    [SerializeField] Team type;
    [SerializeField] string id;

    [SerializeField] CharacterData characterData;
    [SerializeField] OverHeadUI overheadUI;


    public CharacterData CharacterData { get => characterData;}
    public OverHeadUI OverheadUI { get => overheadUI;}
    public Team Type { get => type; set => type = value; }

    public void Focus(bool active)
    {
        overheadUI.gameObject.SetActive(active);
    }
}
