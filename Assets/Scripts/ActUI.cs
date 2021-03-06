﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActUI : ListoUI
{
    [Header("Act UI")]
    [SerializeField] protected UnitSelection unitSelection;
    [SerializeField] protected UnitStatsUIController unitStatsUI;
    [SerializeField] protected ActSkillData[] skills;

    [SerializeField] BattleCharacter owner;
    [SerializeField] BattleController battleCtrl;
    [SerializeField] SkillInfoUI skillInfo;

    protected override void Start()
    {
        Active(false);
    }

    protected override IEnumerator ieExit()
    {
        //battleCtrl.invokeUI.Active(false);
        return base.ieExit();
    }

    public void Init(BattleCharacter owner, BattleController battleCtrl)
    {
        this.owner = owner;
        this.battleCtrl = battleCtrl;
        this.skills = this.owner.Data.Stats.actSkills;

        transform.position = new Vector3(this.owner.transform.position.x, this.owner.transform.position.y + 4.5f, this.owner.transform.position.z);

        ActSkillData data;
        ActButton btn;

        for (int i = 0; i < buttons.Count; i++)
        {
            //ActButton btn;
            int skillIDX = i + (button_per_page * page);

            if (skills.Length <= skillIDX)
            {
                //data = null;
                buttons[i].Hide();
                continue;
            }
            else
            {
                data = skills[skillIDX];
            }

            btn = buttons[i] as ActButton;
            Set(btn, owner, data);//Set button

            btn.value = skillIDX.ToString();

            if (owner.Data.Battle.ap < data.Ap)
            {
                btn.onClick = (b) =>
                {
                    var bx = b as ActButton;
                    bx.Shake();
                    Debug.Log("<color=red>Not Enough AP!</color>");
                };
            }
            else
            {
                btn.onClick = (b) =>
                {
                    var bx = b as ActButton;
                    var skill = this.owner.Data.Stats.actSkills[int.Parse(bx.value)];

                    bx.Focus(false);

                    ///////////////////Invoke UI///////////////////
                    //battleCtrl.invokeUI.Set(this.owner.Data.Stats.actSkills[int.Parse(bx.value)].Gems);
                    //battleCtrl.invokeUI.SetPosition(this.owner.transform.position);
                    //battleCtrl.invokeUI.Active(true);
                    ///////////////////////////////////////////////

                    unitSelection.onExit = () =>
                    {
                        skillInfo.Active(false);
                        Active(true);
                    };
                    unitSelection.onSubmit = (slots) =>
                    {
                        int idx;
                        skillInfo.Active(false);
                        //battleCtrl.invokeUI.Active(false);
                        if (int.TryParse(b.value, out idx))
                        {
                            battleCtrl.ApplyActSkill(this.owner, slots,idx);
                        }
                    };
                    unitSelection.onSelect = (slots, idx) =>
                    {
                        foreach (BattleCharacterSlot slot in slots)
                        {
                            battleCtrl.SetTargetCharacter(owner, slot.Character, idx);
                        };
                    };

                    Debug.Log("<color=red>" + this.owner.Data.Stats.actSkills[int.Parse(bx.value)].SkillName+ "</color>");
                    unitSelection.Active(this.owner,skill.TargetOption, this.owner.Data.Battle.ui_lastTarget);
                    Active(false);
                    skillInfo.Set(skill);
                    skillInfo.Active(true);
                };
            }
        }
    }

    public override void Active(bool active)
    {
        base.Active(active);

        if (active)
        {
            Select(owner.Data.Battle.ui_lastAct);
            foreach(ListoButton b in buttons)
            {
                ActButton btn = b as ActButton;
                btn.allowSubmit = true;
            }

            if (selected_index >= 0) buttons[selected_index].Select();
            //else start_selector.Select();
        }
        else
        {
            foreach (ListoButton b in buttons)
            {
                ActButton btn = b as ActButton;
                btn.allowSubmit = false;
            }
            page = 0;
        }
        main_canvas.enabled = active;
    }

    public override void PageShift(int mod)
    {
        int target = page;
        int maxP = Mathf.FloorToInt((float)skills.Length / (float)button_per_page);
        //Debug.Log("Max page " + maxP);

        if (target + mod < 0) target = maxP;
        else if (target + mod > maxP) target = 0;
        else target += mod;

        //Debug.Log("target " + target);

        int start = button_per_page * target;
        int c = skills.Length;
        ActButton btn;
        for (int i = 0; i < button_per_page; i++)
        {
            btn = buttons[i] as ActButton;
            if (i + start < c)
            {
                //btn.Set(skills[i + start]);
                Set(btn, this.owner, skills[i + start]);//Set button
                btn.value = (i + start).ToString();
                //buttons[i].Set(data);
            }
            else
            {
                btn.Hide();
            }
        }

        page = target;
    }

    public override void Page(int page)
    {
        int maxP = Mathf.FloorToInt((float)skills.Length / (float)button_per_page);
        this.page = page;
        ActButton btn;
        int start = button_per_page * page;
        for (int i = 0; i < button_per_page; i++)
        {
            btn = buttons[i] as ActButton;
            if (i + start < skills.Length)
            {
                Set(btn, this.owner, skills[i + start]);
                btn.value = (i + start).ToString();
            }
            else
            {
                buttons[i].Hide();
            }
        }
    }

    public override void Select(int index)
    {
        base.Select(index);
        owner.Data.Battle.ui_lastAct = index;
    }

    void Set(ActButton btn, BattleCharacter character, ActSkillData skillData)
    {
        btn.Name_txt.text = skillData.SkillName;
        //btn.Sp_txt.text = skillData.Ap.ToString();
        btn.SetGems(skillData.Gems);
        btn.SetAp(skillData.Ap);
        //for (int i = 0; i < btn.Gem_imgs.Length; i++)
        //{
        //    btn.Gem_imgs[i].gameObject.SetActive(false);
        //}
        btn.interactable = true;
        btn.Main_img.gameObject.SetActive(true);
        btn.EnoughActionPts(skillData.Ap <= character.Data.Battle.ap);
    }

}
