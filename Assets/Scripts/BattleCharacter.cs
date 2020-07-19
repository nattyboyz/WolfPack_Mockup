using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Team { Player,CPU}

public class BattleCharacter : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] Team type;
    [SerializeField] string id;
    [SerializeField] CharacterData characterData;
    [Header("Graphic")]
    [SerializeField] GameObject graphic;
    [SerializeField] CharacterSpine characterSpine;
    [Header("UI")]
    [SerializeField] OverHeadUI overheadUI;


    public CharacterData Data { get => characterData;}
    public OverHeadUI OverheadUI { get => overheadUI;}
    public Team Type { get => type; set => type = value; }
    public CharacterSpine CharacterSpine { get => characterSpine; set => characterSpine = value; }

    private void Awake()
    {
        overheadUI.gameObject.SetActive(false);
    }

    public void Focus(bool active)
    {
        overheadUI.gameObject.SetActive(active);
        if (active) graphic.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        else graphic.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
