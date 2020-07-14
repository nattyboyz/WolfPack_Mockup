using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.StateMachine;

namespace PartyCrawl
{
    public class ExecuteCommand : State
    {
    //    BoardStateMachine boardState;
    //    List<UnitCommandHolder> attackCmds = new List<UnitCommandHolder>();
    //    List<UnitCommandHolder> healCmds = new List<UnitCommandHolder>();
    //    List<UnitCommandHolder> defenseCmds = new List<UnitCommandHolder>();
    //    List<UnitCommandHolder> buffCmds = new List<UnitCommandHolder>();
    //    List<UnitCommandHolder> debuffCmds = new List<UnitCommandHolder>();
    //    List<UnitCommandHolder> moveCmds = new List<UnitCommandHolder>();

    //    public ExecuteCommand(BoardStateMachine board)
    //    {
    //        this.boardState = board;
    //    }

    //    public override void OnEnter(object args)
    //    {
    //        Debug.Log("ExecuteCommand : OnEnter");
    //        List<UnitDataHolder> units = args as List<UnitDataHolder>;
    //        boardState.StartCoroutine(ProcessIntention(units));
    //    }

    //    IEnumerator ProcessIntention(List<UnitDataHolder> units)
    //    {
    //        Debug.Log("ExecuteCommand : Process intention");

    //        foreach (UnitDataHolder unit in units)
    //        {
    //            if (unit.isDead)
    //            {
    //                Debug.LogError("ExecuteCommand : unit was dead before enter");
    //                continue;
    //            }

    //            //if (unit.patterns.Count == 0) continue;

    //            List<UnitCommand> commands = unit.GetIntention();
    //            unit.NextTurn();

    //            if (unit.isStun)
    //            {
    //                yield return ResolveStun(unit, false);
    //                continue;
    //            }

    //            foreach (UnitCommand cmd in commands)
    //            {
    //                if (cmd.type == CommandType.Attack)
    //                {
    //                    attackCmds.Add(new UnitCommandHolder(unit, cmd));
    //                }
    //                else if (cmd.type == CommandType.Defense)
    //                {
    //                    defenseCmds.Add(new UnitCommandHolder(unit, cmd));
    //                }
    //                else if (cmd.type == CommandType.Heal)
    //                {
    //                    healCmds.Add(new UnitCommandHolder(unit, cmd));
    //                }
    //                else if (cmd.type == CommandType.Move)
    //                {
    //                    moveCmds.Add(new UnitCommandHolder(unit, cmd));
    //                }
    //            }
    //        }

    //        foreach(UnitCommandHolder cmd in attackCmds)
    //        {
    //            yield return DoAttack(cmd.owner, cmd.command);
    //        }

    //        foreach (UnitCommandHolder cmd in healCmds)
    //        {
    //            yield return DoHeal(cmd.owner, cmd.command);
    //        }

    //        foreach (UnitCommandHolder cmd in defenseCmds)
    //        {
    //            yield return DoDefense(cmd.owner, cmd.command);
    //        }

    //        foreach (UnitCommandHolder cmd in moveCmds)
    //        {
    //            yield return DoMove(cmd.owner, cmd.command.moveOption);
    //        }

    //        attackCmds.Clear();
    //        defenseCmds.Clear();
    //        healCmds.Clear();
    //        moveCmds.Clear();

    //        boardState.EnemyState.TransitionToState(typeof(ExecuteMove), units);
    //    }

    //    IEnumerator DoAttack(UnitDataHolder owner, UnitCommand command)
    //    {
    //        Grid target = LevelManager.player.data.token.Grid;
    //        if (command.targetOption.targetMode == TargetMode.Self)
    //        {
    //            target = owner.token.Grid;
    //        }
    //        owner.token.View.SetIntentions(false);
    //        yield return boardState.boardManager.Attack.ApplyDamage(
    //            owner, target, command.targetOption,
    //            command.intValue, command.boolValue);
    //    }

    //    IEnumerator DoDefense(UnitDataHolder owner, UnitCommand command)
    //    {
    //        Grid target = LevelManager.player.data.token.Grid;
    //        if (command.targetOption.targetMode == TargetMode.Self)
    //        {
    //             target = owner.token.Grid;
    //        }
    //        Debug.Log(owner.baseData.unitName + ": Defense with " + command.intValue);
    //        owner.token.View.SetIntentions(false);
    //        yield return boardState.boardManager.Defense.ApplyShield(owner, target, command.targetOption, command.intValue);
    //    }

    //    IEnumerator DoHeal(UnitDataHolder owner, UnitCommand command)
    //    {
    //        Grid target = LevelManager.player.data.token.Grid;
    //        if (command.targetOption.targetMode == TargetMode.Self)
    //        {
    //            target = owner.token.Grid;
    //        }
    //        Debug.Log(owner.baseData.unitName + ": Heal with " + command.intValue);
    //        owner.token.View.SetIntentions(false);
    //        yield return boardState.boardManager.Heal.ApplyHeal(owner, target, command.targetOption, command.intValue);
    //    }

    //    IEnumerator ResolveStun(UnitDataHolder owner, bool value)
    //    {
    //        owner.isStun = value;
    //        owner.token.View.UpdateIntention();
    //        yield return null;
    //    }

    //    IEnumerator DoMove(UnitDataHolder owner, MoveCommand move)
    //    {
    //        Debug.Log("MoveCommand : Process move cmd:");
    //        owner.token.View.SetIntentions(false);

    //        if (owner.isDead) yield break;
    //        if (owner.isStun) yield break;

    //        if (move.mode == MoveMode.Random)
    //        {
    //            Debug.Log("Move Random " + move.mp + " times");
    //            int move_points = move.mp;

    //            while (move_points > 0)
    //            {
    //                List<Grid> moveable;
    //                List<Grid> unmoveable;

    //                boardState.boardManager.Move.
    //                     GetMovableGrids(owner.token.Grid.dimension, 1, out moveable, out unmoveable);

    //                if (moveable.Count > 0)
    //                {
    //                    if (moveable.Count > 0) moveable = ProcessMovableTargets(move, moveable);
    //                    if (moveable.Count > 1) moveable.Shuffle();
    //                    if (moveable.Count > 0)
    //                        yield return boardState.boardManager.Move.ieDoMove(owner.token, moveable[0]);
    //                }
    //                move_points--;
    //                yield return new WaitForSeconds(0.2f);
    //            }
    //        }

    //        yield return null;

    //    }

    //    public override void OnExit()
    //    {
    //        Debug.Log("Exit [Enemy] <color=red>ExecuteCommand</color> state.");
    //    }

    //    public List<Grid> ProcessMovableTargets(MoveCommand command, List<Grid> moveable)
    //    {
    //        List<Grid> m = new List<Grid>();
    //        for (int i = 0; i < moveable.Count; i++)
    //        {
    //            if (!command.onlyEmpty)
    //            {
    //                if (moveable[i].token == null)
    //                {
    //                    m.Add(moveable[i]);
    //                    continue;
    //                }

    //            }

    //            if (!command.allowSwapPlayer)
    //            {
    //                if (moveable[i].token != LevelManager.player.data.token)
    //                {
    //                    m.Add(moveable[i]);
    //                    continue;
    //                }

    //            }

    //            m.Add(moveable[i]);
    //        }

    //        string s = "";
    //        foreach (Grid g in m)
    //        {
    //            s += g.dimension + "\n";
    //        }
    //        Debug.Log(s);

    //        return m;
    //    }

    }
}