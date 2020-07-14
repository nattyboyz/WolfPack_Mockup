using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ActButton : SkillButton, IPointerEnterHandler, 
    IPointerExitHandler, IPointerDownHandler, ISelectHandler,
    IDeselectHandler, IPointerClickHandler
{
    [SerializeField] protected Image[] gem_imgs;

    [SerializeField] Color enterColor;
    [SerializeField] Color normalTextColor;
    public Action onClick;
    public Action<ActButton> onEnter;
    public Action<ActButton> onExit;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        name_txt.color = enterColor;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        name_txt.color = enterColor;
        onEnter?.Invoke(this);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        name_txt.color = normalTextColor;
        onExit?.Invoke(this);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        name_txt.color = enterColor;
        onEnter?.Invoke(this);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        name_txt.color = normalTextColor;
        onExit?.Invoke(this);
    }

    public void Set(SkillData skillData)
    {
        name_txt.text = skillData.name;
        sp_txt.text = skillData.sp.ToString();
        for(int i = 0; i < gem_imgs.Length; i++)
        {
            if (skillData.gems.Length > i && skillData.gems[i] != Gem.None)
            {
                gem_imgs[i].gameObject.SetActive(true);
            }
            else
            {
                gem_imgs[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
}

public class SkillData
{
    public string name = "skill";
    public int sp = 5;
    public Gem[] gems = new Gem[4] {Gem.None,Gem.None,Gem.None,Gem.None };
    public string lore = "this is a very powerful skill";
    public string description = "Take gx2 for 2 turn";
}
