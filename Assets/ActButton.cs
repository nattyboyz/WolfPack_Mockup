using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

public class ActButton : SkillButton, IPointerEnterHandler, 
    IPointerExitHandler, ISelectHandler,
    IDeselectHandler, IPointerClickHandler, ISubmitHandler
{
    [SerializeField] protected Image main_img;
    [SerializeField] protected Image[] gem_imgs;

    [SerializeField] Color enterColor;
    [SerializeField] Color normalTextColor;

    public bool allowSubmit = true;
    public Action onClick;
    public Action<ActButton> onEnter;
    public Action<ActButton> onExit;

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


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!allowSubmit) return;
        onClick?.Invoke();
        Focus(false);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        Focus(true);
         onEnter?.Invoke(this);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        Focus(false);
        onExit?.Invoke(this);
    }

    public override void OnSelect(BaseEventData eventData)
    {
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
            name_txt.text = "";
            sp_txt.text = "";
            interactable = false;
            for (int i = 0; i < gem_imgs.Length; i++)
            {
                gem_imgs[i].gameObject.SetActive(false);
            }
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
        }

    }

    public void OnSubmit(BaseEventData eventData)
    {
        if(!allowSubmit ) return;
        onClick?.Invoke();
        Focus(false);
    }
}
