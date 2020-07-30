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

    //public InvokationUI invokeUI;
    [SerializeField] SkillInfoUI skillInfo;

    [SerializeField] List<BattleCharacter> allies;//0 1 2
    [SerializeField] List<BattleCharacter> enemies;//3 4 5

    [SerializeField] List<BattleCharacter> turns;
    //bool is_runnning = false; 

    StateMachine turnbaseState;
    public StateMachine TurnbaseState { get => turnbaseState; set => turnbaseState = value; }

    public void Init()
    {
        turnbaseState = new StateMachine();
        //unitSelection.InitSelection();

        turnbaseState.AddState(new ExecuteEnemyTurn(this));
        turnbaseState.AddState(new ExecutePlayerTurn(this));

        foreach (BattleCharacter ch in allies)
        {
            ch.Init();
            ch.onDead = OnCharacterDead;
            //ch.onGiveUp = OnCharacterGiveUp;
        }

        foreach (BattleCharacter ch in enemies)
        {
            ch.Init();
            ch.onDead = OnCharacterDead;
            //ch.onGiveUp = OnCharacterGiveUp;
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

        //if (current_character!=null)current_character.Focus(false);
        current_character = character;
        //character.Focus(true);
        //character.OverheadUI.gameObject.SetActive(true);
    }

    public void SetTargetCharacter(BattleCharacter character)
    {
        if (character == current_character)
        {
            //target_character.Focus(false);
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
            Debug.Log(i + " " +c.Data.Base.C_name);
        }
    }

    int currentTurn = 0;
    public void ExecuteTurn()
    {
        //BattleCharacter character = turns[currentTurn];
        //if (is_runnning) return;
        //is_runnning = true;
        SetCurrentCharacter(turns[currentTurn]);
        EnterTurn(turns[currentTurn]);
        //unitStatsUI.Clear(UnitStatsUIController.Side.Right);
    }

    public void ShowActionUI(BattleCharacter character)
    {
        actionUI.onSubmit = (code) =>
        {
            if (code == "Attack")
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
            SkipTurn();
            return;
        }

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
    }

    void SkipTurn()
    {
        MoveTurnForward();
        ExecuteTurn();
    }

    public void MoveTurnForward()
    {
        currentTurn++;
        if (currentTurn >= turns.Count) currentTurn = 0;
    }

    BattleCharacter owner;
    List<BattleCharacterSlot> targets;
    ActSkillData skill;

    public void ApplyActSkill(BattleCharacter owner,
        List<BattleCharacterSlot> targets,
        ActSkillData skill
        )
    {
        string s = "";
        this.owner = owner;
        this.targets = targets;
        this.skill = skill;

        foreach (BattleCharacterSlot slot in targets)
        {
            if (slot.Character != null)
            {
                s += slot.Character.Data.Base.C_name +", ";
            }
            //slot.Character.OverheadUI.Active(true);
            slot.Character.OverheadUI.ChooseGemSlot(skill.Gems, OnSubmitGemSlot, OnCancelGemSelection);
        }

        Debug.Log("[BattleCrtl]: ["+ owner.Data.Base.C_name +
            "] apply skill <b>[" + 
            skill.SkillName+ "]</b> to " + s);
    }

    void OnCancelGemSelection()
    {
        //foreach (BattleCharacterSlot slot in targets)
        //{
        //    slot.Character.OverheadUI.Active(false);
        //}
        unitSelection.SelectPrevious();
        skillInfo.Active(true);
        //invokeUI.Active(true);
    }

    void OnSubmitGemSlot(Dictionary<int, Gem> gemSlots)
    {
        StartCoroutine(ieExecuteActSkill(owner,targets,skill,gemSlots));
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
                yield return slot.Character.ieTakeGemDamage(gemSlots);
            }
            else
            {
                StartCoroutine(slot.Character.ieTakeGemDamage(gemSlots));
            }
            count++;
        }
        yield return new WaitForSeconds(0.2f);
        MoveTurnForward();
        ExecuteTurn();
        //foreach (BattleCharacterSlot slot in targets)
        //{
        //    slot.Character.OverheadUI.Active(false);
        //}
        foreach (BattleCharacter chara in turns)
        {
            chara.OverheadUI.Active(false);
        }

    }

    public void ApplyBattleSkill(BattleCharacter owner,
      List<BattleCharacterSlot> targets,
      BattleSkillData skill)
    {
        // string s = "";
        //foreach (BattleCharacterSlot slot in targets)
        //{
        //    slot.Character.OverheadUI.Active(true);
        //}
        StartCoroutine(ieExecuteBattleSkill(owner, targets, skill));
        //Debug.Log("[BattleCrtl]: [" + owner.Data.Base.c_name +
        //    "] apply battle skill <b>[" +
        //    skill.SkillName + "]</b> to " + s);
    }

    IEnumerator ieExecuteBattleSkill(BattleCharacter owner,
       List<BattleCharacterSlot> targets,
       BattleSkillData skill)
    {
     
        yield return new WaitForSeconds(0.2f);//This is delay..
        //Pre attack: duration depend on attack animation...
        yield return iePreAttack(owner, targets, skill);
        //Process attack
        Dictionary<BattleCharacter, BattleOutputData> processData;
        Dictionary<BattleCharacter, Dictionary<PostEffectType, int>> postData;
        ProcessAttack(owner, targets, skill,out processData, out postData);

        int count = 0;
        foreach(KeyValuePair<BattleCharacter,BattleOutputData> kvp in processData)
        {
            StartCoroutine(kvp.Key.CharacterSpine.ieGetHit());
            StartCoroutine(kvp.Key.ieTakeDamage(kvp.Value));
            if (count == processData.Count - 1)//Consider just the last tween
            {
                yield return kvp.Key.OverheadUI.HpUi.HpTween.WaitForCompletion();
            }
            count++;
        }
        yield return new WaitForSeconds(0.2f);
        //Post attack
        yield return iePostAttack(owner, postData);
        //foreach (BattleCharacterSlot slot in targets)
        //{
        //    slot.Character.OverheadUI.Active(false);
        //}
        foreach (BattleCharacter chara in turns)
        {
            chara.OverheadUI.Active(false);
        }
        MoveTurnForward();
        ExecuteTurn();
    }

    IEnumerator iePreAttack(BattleCharacter owner,
      List<BattleCharacterSlot> targets,
      BattleSkillData skill)
    {
        StartCoroutine(owner.CharacterSpine.ieAttack());
        yield return null;
    }

   void ProcessAttack(BattleCharacter owner,
          List<BattleCharacterSlot> targets,
          BattleSkillData skill, 
          out Dictionary<BattleCharacter, BattleOutputData> processData,
          out Dictionary<BattleCharacter, Dictionary<PostEffectType, int>> postData)
    {
        processData = new Dictionary<BattleCharacter, BattleOutputData>();
        postData = new Dictionary<BattleCharacter, Dictionary<PostEffectType, int>>();

        foreach (BattleCharacterSlot slot in targets)
        {
            BattleCharacter target = slot.Character;
            BattleOutputData data = new BattleOutputData();
            data.value = Calculate(owner, target, skill);
            data.type = BattleOutputData.Type.Damage;
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

    float Calculate(
        BattleCharacter owner, 
        BattleCharacter target,
        BattleSkillData skill)
    {
        //KeyValuePair<BattleCharacter, BattleOutputData> data = 
        //    new KeyValuePair<BattleCharacter, BattleOutputData>();
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

public class BattleOutputData
{
    public enum Type { None, Damage, Heal }
    public Type type;
    public float value;
    public Dictionary<string, int> viewEffects = new Dictionary<string, int>();
}

public enum PostEffectType { None, Dead, Heal }



