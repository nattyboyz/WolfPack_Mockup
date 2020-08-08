using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Plugins.StateMachine;
using DG.Tweening;

public class BattleController : MonoBehaviour
{
    BattleCharacter current_character;
    BattleCharacter target_character;

    [SerializeField] UnitSelection unitSelection;
    [SerializeField] UnitStatsUIController unitStatsUI;
    [SerializeField] ActionUIController actionUI;
    [SerializeField] ActUI actUI;
    [SerializeField] AttackUI attackUI;

    [SerializeField] SkillInfoUI skillInfo;

    [SerializeField] List<BattleCharacter> allies;//0 1 2
    [SerializeField] List<BattleCharacter> enemies;//3 4 5
    [SerializeField] List<BattleCharacter> turns;

    StateMachine turnbaseState;
    public StateMachine TurnbaseState { get => turnbaseState; set => turnbaseState = value; }

    public void Init()
    {
        turnbaseState = new StateMachine();

        turnbaseState.AddState(new ExecuteEnemyTurn(this));
        turnbaseState.AddState(new ExecutePlayerTurn(this));

        foreach (BattleCharacter ch in allies)
        {
            ch.Init();
            ch.onDead = OnCharacterDead;
        }

        foreach (BattleCharacter ch in enemies)
        {
            ch.Init();
            ch.onDead = OnCharacterDead;
        }
    }

    public void OnCharacterDead(BattleCharacter b)
    {
        Debug.Log(b.Data.Base.C_name + " is dead");
    }

    public void OnCharacterGiveUp()
    {
        Debug.Log("OnCharacterGiveUp");
    }

    public void SetCurrentCharacter(BattleCharacter character)
    {
        Debug.Log("Set current character " + character.Data.Base.C_name);
        if (character.Type == Team.Player)
        {
            unitStatsUI.Show(character, UnitStatsUIController.Side.Left);
            unitStatsUI.Hide(UnitStatsUIController.Side.Right);
        }
        else//CPU
        {
            unitStatsUI.Show(character, UnitStatsUIController.Side.Right);
            unitStatsUI.Hide(UnitStatsUIController.Side.Left);
        }
        current_character = character;
    }

    public void SetTargetCharacter(BattleCharacter owner, BattleCharacter target,int index)
    {
        owner.Data.Battle.ui_lastTarget = index;

        if (target == owner)
        {
            if (target.Type == Team.Player)
                unitStatsUI.Hide(UnitStatsUIController.Side.Right);
            else
                unitStatsUI.Hide(UnitStatsUIController.Side.Left);
        }
        else
        {
            if (owner.Type == Team.Player)
                unitStatsUI.Show(target, UnitStatsUIController.Side.Right);
            else
                unitStatsUI.Show(target, UnitStatsUIController.Side.Left);
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
            Debug.Log(i + " " +c.Data.Base.C_name);
        }
    }

    int currentTurn = 0;

    public void ExecuteTurn()
    {
        EnterTurn(turns[currentTurn]);
    }

    public void ShowActionUI(BattleCharacter character)
    {
        actionUI.onSubmit = (code) =>
        {
            if (code == "Combat")
            {
                attackUI.Init(character, this);
                attackUI.Active(true);
                actionUI.Active(false);
                foreach(BattleCharacter chara in turns)
                {
                    if(!chara.Data.Battle.isDead)
                        chara.OverheadUI.Active(true);
                    else chara.OverheadUI.Active(false);
                }

            }
            else if (code == "Act")
            {
                actUI.Init(character, this);
                actUI.Active(true);
                actionUI.Active(false);
                foreach (BattleCharacter chara in turns)
                {
                    if (!chara.Data.Battle.isDead)
                        chara.OverheadUI.Active(true);
                    else chara.OverheadUI.Active(false);
                }
            }
            else if (code == "Item")
            {
                //actUI.Init(character, this);
                //actUI.Active(true);
                //actionUI.Active(false);
            }
            else if (code == "Skip")
            {
                //actUI.Init(character, this);
                //actUI.Active(true);
                //actionUI.Active(false);
            }
        };
    
        attackUI.onExit = () =>
        {
            attackUI.Active(false);
       
            actionUI.SetPosition(character.transform.position);
            actionUI.Active(true);

            foreach (BattleCharacter chara in turns)
            {
                chara.OverheadUI.Active(false);
            }
        };

        actUI.onExit = () =>
        {
            actUI.Active(false);
         
            actionUI.SetPosition(character.transform.position);
            actionUI.Active(true);

            foreach (BattleCharacter chara in turns)
            {
                chara.OverheadUI.Active(false);
            }
        };

        actionUI.SetPosition(character.transform.position);
        actionUI.Active(true);


    }

    void EnterTurn(BattleCharacter character)
    {
        if(character.Data.Battle.isDead)
        {
            EndTurn();
            return;
        }

        SetCurrentCharacter(turns[currentTurn]);

        if (character.Type == Team.Player)
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
    }

    void EndTurn()
    {
        MoveTurnForward();
        ExecuteTurn();
    }

    public void MoveTurnForward()
    {
        currentTurn++;
        if (currentTurn >= turns.Count) currentTurn = 0;
    }

    List<BattleCharacterSlot> targets;
    ActSkillData skill;

    public void ApplyActSkill(BattleCharacter owner,
        List<BattleCharacterSlot> targets,
        int index
        )
    {
        this.targets = targets;
        owner.Data.Battle.ui_lastAct = index;
        this.skill = owner.Data.Stats.actSkills[index];

        foreach (BattleCharacterSlot slot in targets)
        {
            slot.Character.OverheadUI.ChooseGemSlot(skill.Gems, OnSubmitGemSlot, OnCancelGemSelection);
        }
    }

    void OnCancelGemSelection()
    {
        unitSelection.SelectPrevious();
        skillInfo.Active(true);
    }

    void OnSubmitGemSlot(Dictionary<int, Gem> gemSlots)
    {
        StartCoroutine(ieExecuteActSkill(current_character,targets,skill,gemSlots));
    }

    IEnumerator ieExecuteActSkill(BattleCharacter owner,
        List<BattleCharacterSlot> targets,
        ActSkillData skill, Dictionary<int,Gem> gemSlots)
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(owner.CharacterSpine.ieAttack());

        int count = 0;
        foreach (BattleCharacterSlot slot in targets)
        {
            StartCoroutine(slot.Character.CharacterSpine.ieGetHit());
            //StartCoroutine(slot.Character.ieTakeGemDamage(gemSlots));
            if (count == targets.Count - 1)//Consider just the last tween
            {
                yield return slot.Character.ieModifyGems(gemSlots);
            }
            else
            {
                StartCoroutine(slot.Character.ieModifyGems(gemSlots));
            }
            count++;
        }
        yield return new WaitForSeconds(0.2f);

        foreach (BattleCharacter chara in turns)
        {
            chara.OverheadUI.Active(false);
        }

        MoveTurnForward();
        ExecuteTurn();



       // unitStatsUI.Hide(UnitStatsUIController.Side.Right);
    }

    public void ApplyBattleSkill(BattleCharacter owner,
      List<BattleCharacterSlot> targets,
      int index)
    {
        owner.Data.Battle.ui_lastAttack = index;
        BattleSkillData skill = owner.Data.Stats.battleSkills[index];
        StartCoroutine(ieExecuteBattleSkill(owner, targets, skill));
    }

    IEnumerator ieExecuteBattleSkill(BattleCharacter owner,
       List<BattleCharacterSlot> targets,
       BattleSkillData skill)
    {
        yield return new WaitForSeconds(0.2f);//This is delay..
        //Pre attack: duration depend on attack animation...
        yield return iePreAttack(owner, targets, skill);
        //Process attack
        Dictionary<BattleCharacter, HpModifierData> processData;
        Dictionary<BattleCharacter, Dictionary<PostEffectType, int>> postData;
        ProcessAttack(owner, targets, skill,out processData, out postData);

        int count = 0;
        foreach(KeyValuePair<BattleCharacter,HpModifierData> kvp in processData)
        {
            StartCoroutine(kvp.Key.CharacterSpine.ieGetHit());
            StartCoroutine(kvp.Key.ieModifyHp(kvp.Value));
            if (count == processData.Count - 1)//Consider just the last tween
            {
                yield return kvp.Key.OverheadUI.HpUi.HpTween.WaitForCompletion();
            }
            count++;
        }
        yield return new WaitForSeconds(0.2f);
        //Post attack
        yield return iePostAttack(owner, postData);

        foreach (BattleCharacter chara in turns)
        {
            chara.OverheadUI.Active(false);
        }
        MoveTurnForward();
        ExecuteTurn();

       // unitStatsUI.Hide(UnitStatsUIController.Side.Right);
    }

    IEnumerator iePreAttack(BattleCharacter owner, List<BattleCharacterSlot> targets, BattleSkillData skill)
    {
        StartCoroutine(owner.CharacterSpine.ieAttack());
        yield return null;
    }

   void ProcessAttack(BattleCharacter owner,
          List<BattleCharacterSlot> targets,
          BattleSkillData skill, 
          out Dictionary<BattleCharacter, HpModifierData> processData,
          out Dictionary<BattleCharacter, Dictionary<PostEffectType, int>> postData)
    {
        processData = new Dictionary<BattleCharacter, HpModifierData>();
        postData = new Dictionary<BattleCharacter, Dictionary<PostEffectType, int>>();

        foreach (BattleCharacterSlot slot in targets)
        {
            BattleCharacter target = slot.Character;
            HpModifierData data = new HpModifierData();
            data.value = Calculate(owner, target, skill);
            data.type = HpModifierData.Type.Damage;
            processData.Add(target, data);

            //Add pose effect
            Dictionary<PostEffectType, int> postEffects = new Dictionary<PostEffectType, int>();
    
            if (target.Data.Battle.hp - data.value <= 0)
            {
                postEffects.Add(PostEffectType.Dead, 0);
                Debug.Log("[Process Atk] <color=red>Will dead soon</color>");
            }

            if(postEffects.Count>0) postData.Add(target, postEffects);
        }
        //yield return owner.CharacterSpine.ieAttack();
       
    }

    float Calculate(BattleCharacter owner, BattleCharacter target, BattleSkillData skill)
    {
        return skill.Damage.max;
    }

    IEnumerator iePostAttack(BattleCharacter owner, Dictionary<BattleCharacter, Dictionary<PostEffectType, int>> postData)
    {
        foreach (KeyValuePair<BattleCharacter, Dictionary<PostEffectType, int>> kvp in postData)
        {
            foreach (KeyValuePair<PostEffectType, int> effect in kvp.Value)
            {
                Debug.Log("[POST EFFECT]: Set <b>[" + kvp.Key.Data.Base.C_name +
                    "]</b> Effect " + effect.Key.ToString() +
                    " Value " + effect.Value.ToString());
                if(effect.Key == PostEffectType.Dead)
                {
                    kvp.Key.Dead();
                }
            }
        }
        yield return new WaitForSeconds(0.2f);
    }
}

public class HpModifierData
{
    public enum Type { None, Damage, Heal }
    public Type type;
    public float value;
    public Dictionary<string, int> viewEffects = new Dictionary<string, int>();
}

public enum PostEffectType { None, Dead, Heal }



