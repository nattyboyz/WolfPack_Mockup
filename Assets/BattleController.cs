﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleController : MonoBehaviour
{
    BattleCharacter current_character;
    BattleCharacter target_character;

    [SerializeField] UnitStatsUIController unitStatsUI;

    //[SerializeField] UnitStatsUI leftUI;
    //[SerializeField] UnitStatsUI rightUI;

    [SerializeField] List<BattleCharacter> allies;//0 1 2
    [SerializeField] List<BattleCharacter> enemies;//3 4 5

    [SerializeField] List<BattleCharacter> turns;

    [SerializeField] Transform[] alliesSpawnPos;
    [SerializeField] Transform[] enemiesSpawnPos;


    public void SetCurrentCharacter(BattleCharacter character)
    {
        unitStatsUI.Show(character.CharacterData, UnitStatsUIController.Side.Left);

        if(current_character!=null)current_character.Focus(false);
        current_character = character;
        character.Focus(true);
        //character.OverheadUI.gameObject.SetActive(true);
    }

    public void SetTargetCharacter(BattleCharacter character)
    {
        if (character == current_character)
        {
            target_character.Focus(false);
            target_character = null;
            unitStatsUI.Clear(UnitStatsUIController.Side.Right);
        }
        else
        {
            unitStatsUI.Show(character.CharacterData, UnitStatsUIController.Side.Right);
            if(target_character!=null) target_character.Focus(false);
            target_character = character;
        }
    }

    void Start()
    {
        SortTurn();
    }

    void SortTurn()
    {
        //Set turn depend by speed
        turns = new List<BattleCharacter>(allies);
        turns.AddRange(enemies);
        IEnumerable<BattleCharacter> query = turns.OrderByDescending(turns => turns.CharacterData.BattleData.speed);
        turns = query.ToList<BattleCharacter>();
        int i = 0;
        foreach (BattleCharacter c in query)
        {
            i++;
            Debug.Log(i + " " +c.CharacterData.BaseData.c_name);
        }
    }

    int currentTurn = 0;

    public void ExecuteTurn()
    {
        SetCurrentCharacter(turns[currentTurn]);
        EnterTurn(turns[currentTurn]);
        unitStatsUI.Clear(UnitStatsUIController.Side.Right);
    }

    void EnterTurn(BattleCharacter character)
    {

        if(character.Type == Team.Player)
        {
            //Enter allie turn
            Debug.Log("Battle: Enter turn " + "[Player]" + character.CharacterData.name);
        }
        else
        {
            Debug.Log("Battle: Enter turn "+"[CPU]"+ character.CharacterData.name);
            //Enter cpu turn
        }
        MoveTurnForward();
        //RunTurn();
    }

    void MoveTurnForward()
    {
        currentTurn++;
        if (currentTurn >= turns.Count) currentTurn = 0;
    }

    public void LoadLevel()
    {
        //Set chacter positions

    }

    void Spawn()
    {
        for(int i =0;i< allies.Count; i++)
        {
            allies[i].transform.position = alliesSpawnPos[i].position;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.position = enemiesSpawnPos[i].position;
        }
    }

}
