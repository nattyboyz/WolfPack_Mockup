using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.StateMachine;

public class ExecutePlayerTurn : State
{
    BattleController battleCtrl;
    BattleCharacter character;

    public ExecutePlayerTurn(BattleController b)
    {
        this.battleCtrl = b;
    }

    //BattleCharacter
    public override void OnEnter(object args)
    {
        this.character = args as BattleCharacter;
        Debug.Log("Player State: Enter turn " + "[Player]" + this.character.CharacterData.name);
        battleCtrl.StartCoroutine(ieEnter());

        //battleCtrl.TurnbaseState.TransitionToState(typeof(ExecuteCommand), units);
    }

    IEnumerator ieEnter()
    {
        yield return new WaitForSeconds(0.2f);
        //battleCtrl.MoveTurnForward();
        //battleCtrl.ExecuteTurn();
        battleCtrl.ShowActionUI(character);
    }

    public override void OnExit()
    {
        Debug.Log("Player State: Exit turn " + "[Player]" + this.character.CharacterData.name);
        //base.OnExit();
    }
}
