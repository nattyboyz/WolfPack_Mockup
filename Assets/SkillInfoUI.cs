using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class SkillInfoUI : MonoBehaviour
{
    [SerializeField] Canvas main_canvas;
    [SerializeField] RectTransform parent;

    [SerializeField] TextMeshProUGUI skillName_txt;
    [SerializeField] TextMeshProUGUI skillDescription_txt;
    [SerializeField] TextMeshProUGUI skillLor_txt;
    [SerializeField] TextMeshProUGUI apCost_txt;
    [SerializeField] Image icon_img;


    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip inClip;
    [SerializeField] AnimationClip outClip;


    public void Active(bool value)
    {
        StartCoroutine(ieActive(value));
    }

    public IEnumerator ieActive(bool value)
    {
        if (value)
        {
            if (animator != null && inClip != null)
            {
                //animator.ResetTrigger("out");
                animator.SetTrigger("in");
                //main_canvas.enabled = value;
                yield return new WaitForSeconds(inClip.length);
            }
            else
            {
                main_canvas.enabled = value;
            }
        }
        else
        {
            if (animator != null && outClip != null)
            {
                //animator.ResetTrigger("in");

                animator.SetTrigger("out");

                yield return new WaitForSeconds(outClip.length);
                //main_canvas.enabled = value;
            }
            else
            {
                main_canvas.enabled = value;
            }
        }
    }

    public void Set(ActSkillData actSkill)
    {
        skillName_txt.text = actSkill.SkillName;
        skillDescription_txt.text = actSkill.Description;
        skillLor_txt.text = actSkill.Lore;
        apCost_txt.text = actSkill.Ap.ToString();
    }

    public void Set(BattleSkillData battleSkill)
    {
        skillName_txt.text = battleSkill.SkillName;
        skillDescription_txt.text = battleSkill.Description;
        skillLor_txt.text = battleSkill.Lore;
        apCost_txt.text = battleSkill.Ap.ToString();
    }
}
