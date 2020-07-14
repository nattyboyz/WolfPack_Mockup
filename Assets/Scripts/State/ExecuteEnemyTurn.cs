using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.StateMachine;

public class ExecuteEnemyTurn : State
{
    BattleController battleCtrl;
    BattleCharacter character;

    public ExecuteEnemyTurn(BattleController b)
    {
        this.battleCtrl = b;
    }

    public override void OnEnter(object args)
    {
        this.character = args as BattleCharacter;
        Debug.Log("Enemy State: Enter turn " + "[CPU]" + this.character.CharacterData.name);
        battleCtrl.StartCoroutine(ieEnter());
    }

    IEnumerator ieEnter()
    {
        yield return new WaitForSeconds(1);
        //battleCtrl.ShowActionUI(character);
        battleCtrl.MoveTurnForward();
        battleCtrl.ExecuteTurn();
    }

    public override void OnExit()
    {
        Debug.Log("Enemy State: Exit turn " + "[Player]" + this.character.CharacterData.name);
        //base.OnExit();
    }
}
