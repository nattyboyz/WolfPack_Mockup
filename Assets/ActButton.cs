using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class ActButton : ListoButton
{
    [SerializeField] protected TextMeshProUGUI sp_txt;
    [SerializeField] protected Image main_img;
    [SerializeField] protected Image[] gem_imgs;

    [SerializeField] Color enterColor;
    [SerializeField] Color normalTextColor;

    public void Focus(bool value)
    {
        if (value)
        {
            name_txt.color = enterColor;
            main_img.rectTransform.anchoredPosition = new Vector2(17,0);
        }
        else
        {
            name_txt.color = normalTextColor;
            main_img.rectTransform.anchoredPosition = new Vector2(0, 0);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!allowSubmit || !interactable) return;
        base.OnPointerEnter(eventData);
        Focus(true);
         onEnter?.Invoke(this);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!allowSubmit || !interactable) return;
        base.OnPointerExit(eventData);
        Focus(false);
        onExit?.Invoke(this);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        if (!allowSubmit || !interactable) return;
        base.OnSelect(eventData);
        Focus(true);
        onEnter?.Invoke(this);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        Focus(false);
        onExit?.Invoke(this);
    }

    public void Set(SkillData skillData)
    {
        if (skillData == null)
        {
            Hide();
        }
        else
        {
            name_txt.text = skillData.SkillName;
            sp_txt.text = skillData.Sp.ToString();
            for (int i = 0; i < gem_imgs.Length; i++)
            {
                if (skillData.Gems.Length > i && skillData.Gems[i] != Gem.None)
                {
                    gem_imgs[i].gameObject.SetActive(true);
                }
                else
                {
                    gem_imgs[i].gameObject.SetActive(false);
                }
            }
            interactable = true;
            main_img.gameObject.SetActive(true);
        }

    }

    public void Set(string detail)
    {
        name_txt.text = detail;
        interactable = true;

    }

    public override void Hide()
    {
        base.Hide();
        name_txt.text = "";
        sp_txt.text = "";
        interactable = false;
        for (int i = 0; i < gem_imgs.Length; i++)
        {
            gem_imgs[i].gameObject.SetActive(false);
        }
        main_img.gameObject.SetActive(false);
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!allowSubmit || !interactable) return;
        onClick?.Invoke(value);
        Focus(false);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        if (!allowSubmit || !interactable) return;
        onClick?.Invoke(value);
        Focus(false);
    }
}
