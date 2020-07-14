using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.StateMachine;

namespace PartyCrawl
{
    public class ExecuteMove : State
    {
        //BoardStateMachine stateMachine;
        //BoardMovement boardMove { get { return stateMachine.boardManager.Move; } }

        //public ExecuteMove(BoardStateMachine board)
        //{
        //    this.stateMachine = board;
        //}

        //public override void OnEnter(object args)
        //{
        //    Debug.Log("MoveCommand : OnEnter");
        //    List<UnitDataHolder> units = args as List<UnitDataHolder>;
        //    stateMachine.StartCoroutine(ProcessMoveCommand(units));
        //}

        //IEnumerator ProcessMoveCommand(List<UnitDataHolder> units)
        //{
        //    Debug.Log("MoveCommand : Process move cmd:");
        //    foreach (UnitDataHolder unit in units)
        //    {
        //        if (unit.isDead) continue;
        //        if (unit.isStun) continue;

        //        for (int i = 0; i < unit.moveCommands.Count; i++)
        //        {
        //            MoveCommand move = unit.moveCommands[i];
        //            if (move.mode == MoveMode.Random)
        //            {
        //                Debug.Log("Move Random " + move.mp + " times");
        //                int move_points = move.mp;

        //                while (move_points > 0)
        //                {
        //                    List<Grid> moveable;
        //                    List<Grid> unmoveable;

        //                    boardMove.GetMovableGrids(unit.token.Grid.dimension, 1, out moveable, out unmoveable);

        //                    if (moveable.Count > 0)
        //                    {
        //                        if (moveable.Count > 0) moveable = ProcessMovableTargets(move, moveable);
        //                        if (moveable.Count > 1) moveable.Shuffle();
        //                        if (moveable.Count > 0)
        //                            yield return boardMove.ieDoMove(unit.token, moveable[0]);
        //                    }
        //                    move_points--;
        //                    yield return new WaitForSeconds(0.2f);
        //                }
        //            }
        //        }
        //        yield return null;
        //    }

        //    stateMachine.EnemyState.TransitionToState(typeof(ExecuteIntention), units);
        //}

        //List<Grid> ProcessMovableTargets(MoveCommand command, List<Grid> moveable)
        //{
        //    List<Grid> m = new List<Grid>();
        //    for (int i = 0; i < moveable.Count; i++)
        //    {
        //        if (!command.onlyEmpty)
        //        {
        //            if (moveable[i].token == null)
        //            {
        //                m.Add(moveable[i]);
        //                continue;
        //            }
                              
        //        }

        //        if (!command.allowSwapPlayer)
        //        {
        //            if (moveable[i].token != LevelManager.player.data.token)
        //            {
        //                m.Add(moveable[i]);
        //                continue;
        //            }
    
        //        }

        //        m.Add(moveable[i]);
        //    }

        //    string s = "";
        //    foreach(Grid g in m)
        //    {
        //        s += g.dimension + "\n";
        //    }
        //    Debug.Log(s);

        //    return m;
        //}

    }
}