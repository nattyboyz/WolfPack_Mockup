﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActUI : ListoUI
{
    [Header("Act UI")]
    [SerializeField] protected UnitSelection unitSelection;
    [SerializeField] protected UnitStatsUIController unitStatsUI;
    [SerializeField] protected ActSkillData[] skills;

    protected override void Start()
    {
        Active(false);
    }

    public void Init(BattleCharacter character, BattleController battleCtrl)
    {
        transform.position = new Vector3(character.transform.position.x, character.transform.position.y +4.5f, character.transform.position.z);
        skills = character.Data.Stats.actSkills;
        ActButton btn;
        ActSkillData data;

        for (int i = 0; i < buttons.Count; i++)
        {
            if (skills.Length <= i)
            {
                data = null;
            }
            else
            {
                data = skills[i];
            }

            btn = buttons[i] as ActButton;
            Set(btn, data);//Set button
            btn.value = i.ToString();
            btn.onClick = (value) =>
                {
                    unitSelection.onCancel = () =>
                    {
                        Active(true);
                    };
                    unitSelection.onSubmit = (slots) =>
                    {
                        int idx;
                        if (int.TryParse(value, out idx))
                        {
                            battleCtrl.ApplyActSkill(character,
                                slots,
                                character.Data.Stats.actSkills[idx]);
                        }
                    };
                    unitSelection.onSelect = (slots) =>
                    {
                        unitStatsUI.Show(slots[0].Character.Data, UnitStatsUIController.Side.Right);
                    };

                    unitSelection.ActiveUnitSelection(TargetMode.Single, 3);
                    Active(false);
                    //Debug.Log("Click button");
                };

        }
    }

    //public void OnSelectButton(ActButton button)
    //{
    //    button.transform.localPosition = new Vector3(27f,
    //   button.transform.localPosition.y +10f,
    //  button.transform.localPosition.z);
    //}

    //public void OnDeselectButton(ActButton button)
    //{
    //    button.transform.localPosition = new Vector3(8.5f,
    //                button.transform.localPosition.y -10f,
    //                button.transform.localPosition.z);
    //}

    public override void Active(bool active)
    {
        base.Active(active);
        if (active)
        {
            foreach(ListoButton b in buttons)
            {
                ActButton btn = b as ActButton;
                btn.allowSubmit = true;
            }

            if (selected_index >= 0) buttons[selected_index].Select();
            else start_selector.Select();
        }
        else
        {
            foreach (ListoButton b in buttons)
            {
                ActButton btn = b as ActButton;
                btn.allowSubmit = false;
            }
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
                Set(btn, skills[i + start]);//Set button
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

    public void Set(ActButton btn, ActSkillData skillData)
    {
        if (skillData == null)
        {
            btn.Hide();
        }
        else
        {
            btn.SetGems(skillData.Gems);
            btn.Name_txt.text = skillData.SkillName;
            btn.Sp_txt.text = skillData.Sp.ToString();
            btn.interactable = true;
            btn.Main_img.gameObject.SetActive(true);
        }

    }
}