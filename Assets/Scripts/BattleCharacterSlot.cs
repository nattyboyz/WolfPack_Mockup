using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterSlot : MonoBehaviour
{
    [SerializeField] BattleCharacter character;
    [SerializeField] GameObject highlight_efx;
    public BattleCharacter Character { get => character; set => character = value; }

    private void Start()
    {
        Highlight(false);
    }

    public void Highlight(bool active)
    {
        highlight_efx.SetActive(active);
    }

}
