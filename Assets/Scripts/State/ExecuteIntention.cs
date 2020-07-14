using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.StateMachine;

namespace PartyCrawl
{
    public class ExecuteIntention : State
    {
        //BoardStateMachine stateMachine;
        //BoardMovement boardMove { get { return stateMachine.boardManager.Move; } }

        //public ExecuteIntention(BoardStateMachine board)
        //{
        //    this.stateMachine = board;
        //}


        //public override void OnEnter(object args)
        //{
        //    Debug.Log("Intention : OnEnter");
        //    List<UnitDataHolder> units = args as List<UnitDataHolder>;
        //    stateMachine.StartCoroutine(ProcessUpdateIntention(units));
        //}


        //IEnumerator ProcessUpdateIntention(List<UnitDataHolder> units)
        //{
        //    foreach (UnitDataHolder unit in units)
        //    {
        //        Debug.Log("Update intention of " + unit.baseData.unitName);
        //        stateMachine.boardManager.CheckIntention(unit);
               
        //        yield return null;
        //    }
        //    stateMachine.EnterPlayerTurn();
        //}

        ////IEnumerator CheckRange(UnitDataHolder unit)
        ////{

        ////    if(stateMachine.boardManager.Grid.IsInRange(
        ////        unit.token.Grid.dimension,
        ////      LevelManager.player.data.token.Grid.dimension,
        ////      LocationType.Melee))
        ////    {
        ////        unit.intention_index = 0;
        ////    }
        ////    else
        ////    {
        ////        unit.intention_index = 1;
        ////    }

        ////    yield return null;
        ////}

    }
}
