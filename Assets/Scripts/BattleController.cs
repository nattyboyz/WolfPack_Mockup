using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Plugins.StateMachine;

public class BattleController : MonoBehaviour
{
    BattleCharacter current_character;
    BattleCharacter target_character;

    [SerializeField] UnitSelection unitSelection;
    [SerializeField] UnitStatsUIController unitStatsUI;
    [SerializeField] ActionUIController actionUI;
    [SerializeField] ActUI actUI;
    [SerializeField] AttackUI attackUI;

    [SerializeField] List<BattleCharacter> allies;//0 1 2
    [SerializeField] List<BattleCharacter> enemies;//3 4 5

    [SerializeField] List<BattleCharacter> turns;

    StateMachine turnbaseState;
    public StateMachine TurnbaseState { get => turnbaseState; set => turnbaseState = value; }

    public void Init()
    {
        turnbaseState = new StateMachine();
        unitSelection.InitSelection();

        turnbaseState.AddState(new ExecuteEnemyTurn(this));
        turnbaseState.AddState(new ExecutePlayerTurn(this));

        foreach (BattleCharacter ch in allies)
        {
            ch.Init();
        }

        foreach (BattleCharacter ch in enemies)
        {
            ch.Init();
        }
    }

    public void SetCurrentCharacter(BattleCharacter character)
    {
        if (character.Type == Team.Player)
        {
            unitStatsUI.Show(character.Data, UnitStatsUIController.Side.Left);
            unitStatsUI.Clear(UnitStatsUIController.Side.Right);
        }
        else//CPU
        {
            unitStatsUI.Show(character.Data, UnitStatsUIController.Side.Right);
            unitStatsUI.Clear(UnitStatsUIController.Side.Left);
        }

        if (current_character!=null)current_character.Focus(false);
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
            if(character.Type == Team.Player)
                unitStatsUI.Clear(UnitStatsUIController.Side.Right);
            else
                unitStatsUI.Clear(UnitStatsUIController.Side.Left);
        }
        else
        {
            unitStatsUI.Show(character.Data, UnitStatsUIController.Side.Right);
            if(target_character!=null) target_character.Focus(false);
            target_character = character;

            if (current_character.Type == Team.Player)
                unitStatsUI.Show(character.Data, UnitStatsUIController.Side.Right);
            else
                unitStatsUI.Show(character.Data, UnitStatsUIController.Side.Left);
        }
    }

    IEnumerator Start()
    {
        Init();
        yield return SortTurn();
    }

    private void Update()
    {
        turnbaseState.Update(Time.deltaTime);
    }

    IEnumerator SortTurn()
    {
        //Set turn depend by speed
        turns = new List<BattleCharacter>(allies);
        turns.AddRange(enemies);
        yield return new WaitForEndOfFrame();
        IEnumerable<BattleCharacter> query = 
            turns.OrderByDescending(turns => turns.Data.Battle.speed);

       // Debug.Log(query)

        turns = query.ToList<BattleCharacter>();
        int i = 0;
        foreach (BattleCharacter c in query)
        {
            i++;
            Debug.Log(i + " " +c.Data.Base.c_name);
        }
    }

    int currentTurn = 0;
    public void ExecuteTurn()
    {
        //BattleCharacter character = turns[currentTurn];

        SetCurrentCharacter(turns[currentTurn]);
        EnterTurn(turns[currentTurn]);
        //unitStatsUI.Clear(UnitStatsUIController.Side.Right);
    }

    public void ShowActionUI(BattleCharacter character)
    {
        actionUI.onActClick = 
            actionUI.onItemClick = 
            actionUI.onSkipClick =
        () => {
            actUI.Init(character, this);
            actUI.Active(true);
        };

        actionUI.onAttackClick = () => {
            attackUI.Init(character, this);
            attackUI.Active(true);
        };




        attackUI.onExit = () =>
        {
            attackUI.Active(false);
            actionUI.Show(character.transform.position);
        };

        actUI.onExit = () =>
        {
            actUI.Active(false);
            actionUI.Show(character.transform.position);
        };


        actionUI.Show(character.transform.position);

    }

    void EnterTurn(BattleCharacter character)
    {

        if(character.Type == Team.Player)
        {
            //Enter allie turn

           // Debug.Log("Battle: Enter turn " + "[Player]" + character.Data.name);
            TurnbaseState.TransitionToState(typeof(ExecutePlayerTurn), character);
        }
        else
        {
           // Debug.Log("Battle: Enter turn "+"[CPU]"+ character.Data.name);
            TurnbaseState.TransitionToState(typeof(ExecuteEnemyTurn), character);
            //Enter cpu turn
        }
        //MoveTurnForward();
        //RunTurn();
    }

    public void MoveTurnForward()
    {
        currentTurn++;
        if (currentTurn >= turns.Count) currentTurn = 0;
    }

    public void ApplyActSkill(BattleCharacter owner,
        List<BattleCharacterSlot> targets,
        ActSkillData skill
        )
    {
        string s = "";
        foreach (BattleCharacterSlot slot in targets)
        {
            if (slot.Character != null)
            {
                s += slot.Character.Data.Base.c_name +", ";
            }
            slot.Character.GetActSkill(skill);
        }

        Debug.Log("[BattleCrtl]: ["+ owner.Data.Base.c_name +
            "] apply skill <b>[" + 
            skill.SkillName+ "]</b> to " + s);

        MoveTurnForward();
        ExecuteTurn();
    }


    public void ProcessActSkill(BattleCharacter owner,
        List<BattleCharacterSlot> targets,
        ActSkillData skill)
    {
        // unitStatsUI.



    }


    public void ApplyBattleSkill(BattleCharacter owner,
      List<BattleCharacterSlot> targets,
      BattleSkillData skill)
    {
        string s = "";
        foreach (BattleCharacterSlot slot in targets)
        {
            if (slot.Character != null)
            {
                s += slot.Character.Data.Base.c_name + ", ";
            }

        }

        Debug.Log("[BattleCrtl]: [" + owner.Data.Base.c_name +
            "] apply skill <b>[" +
            skill.SkillName + "]</b> to " + s);

        MoveTurnForward();
        ExecuteTurn();
    }

}


