using Plugins.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PartyCrawl
{
    public class ExecuteEnterTurn : State
    {
        //BoardStateMachine enemyStateMachine;

        //public ExecuteEnterTurn(BoardStateMachine board)
        //{
        //    this.enemyStateMachine = board;
        //}

        //public override void OnEnter(object args)
        //{
        //    Debug.Log("EnterTurn : OnEnter");
        //    List<UnitDataHolder> units = args as List<UnitDataHolder>;
        //    enemyStateMachine.StartCoroutine(ProcessStatusEffect(units));
        //}

        //IEnumerator ProcessStatusEffect(List<UnitDataHolder> units)
        //{
        //    Debug.Log("EnterTurn : Process status effect:");

        //    List<UnitDataHolder> burnUnits = new List<UnitDataHolder>();
        //    List<UnitDataHolder> poisonUnits = new List<UnitDataHolder>();

        //    foreach (UnitDataHolder unit in units)
        //    {
        //        if (unit.isStun || unit.isDead) continue;

        //        if (unit.burn > 0)
        //        {
        //            burnUnits.Add(unit);
        //        }

        //        if (unit.poison > 0)
        //        {
        //            poisonUnits.Add(unit);
        //        }

        //        yield return null;
        //    }

        //    yield return ieApplyBurn(burnUnits);
        //    yield return ieApplyPoison(poisonUnits);
        //    enemyStateMachine.EnemyState.TransitionToState(typeof(ExecuteCommand), units);
        //}

        //IEnumerator ieApplyBurn(List<UnitDataHolder> units)
        //{
        //    yield return ActiveBurnStack(units);
        //}

        //IEnumerator ieApplyPoison(List<UnitDataHolder> units)
        //{
        //    yield return ActivePoisionStack(units);
        //    //yield return null;
        //}

        ///// <summary>
        ///// Remove value stack and apply damage
        ///// </summary>
        ///// <param name="target"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public IEnumerator ActiveBurnStack(List<UnitDataHolder> targets)
        //{
        //    int damage = 1;

        //    List<UnitDataHolder> takeDamageTargets = new List<UnitDataHolder>();
        //    List<UnitDataHolder> deadTargets = new List<UnitDataHolder>();

        //    for (int i = 0; i < targets.Count; i++)
        //    {
        //        UnitDataHolder targetUnit = targets[i];
        //        UnitData unitData = targetUnit.baseData;
        //        Token token = targetUnit.token;
        //        Grid grid = targetUnit.token.Grid;

        //        if (targetUnit.burn <= 0)
        //        {
        //            yield break;
        //        }

        //        targetUnit.ApplyDamage(damage, false);

        //        if (targetUnit.isDead)
        //        {
        //            deadTargets.Add(targetUnit);
        //        }
        //        else
        //        {
        //            takeDamageTargets.Add(targetUnit);
        //        }

        //        targetUnit.burn--;

        //        token.GetHit();
        //        token.Burn();
        //    }

        //    //StartCoroutine(ApplyDead(deadTargets));

        //    foreach (UnitDataHolder unit in deadTargets)
        //    {
        //        enemyStateMachine.StartCoroutine(unit.token.Dead());
        //    }

        //    //yield return ApplyDead(deadTargets);

        //    if (targets.Count > 0) yield return new WaitForSeconds(0.5f);

        //    yield return null;
        //}

        ///// <summary>
        ///// Remove value stack and apply damage
        ///// </summary>
        ///// <param name="target"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public IEnumerator ActivePoisionStack(List<UnitDataHolder> targets)
        //{
        //    int damage = 1;

        //    List<UnitDataHolder> takeDamageTargets = new List<UnitDataHolder>();
        //    List<UnitDataHolder> deadTargets = new List<UnitDataHolder>();

        //    for (int i = 0; i < targets.Count; i++)
        //    {
        //        UnitDataHolder targetUnit = targets[i];
        //        UnitData unitData = targetUnit.baseData;
        //        Token token = targetUnit.token;
        //        Grid grid = targetUnit.token.Grid;

        //        if (targetUnit.poison <= 0)
        //        {
        //            yield break;
        //        }

        //        targetUnit.ApplyDamage(damage, true);
        //        if (targetUnit.isDead)
        //        {
        //            deadTargets.Add(targetUnit);
        //        }
        //        else
        //        {
        //            takeDamageTargets.Add(targetUnit);
        //        }

        //        targetUnit.poison--;
        //        //token.GetHit();
        //        //token.UpdatePoison();
        //        //token.Efx.Poison();
        //    }

        //    if (targets.Count > 0) yield return new WaitForSeconds(0.5f);

        //    yield return null;

        //}


    }
}