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
    [SerializeField] private TextMeshProUGUI sp_txt;
    [SerializeField] private Image main_img;
    [SerializeField] private Image[] gem_imgs;

    [SerializeField] protected Color enterColor;
    [SerializeField] protected Color normalTextColor;

    public TextMeshProUGUI Sp_txt { get => sp_txt; set => sp_txt = value; }
    public Image Main_img { get => main_img; set => main_img = value; }
    public Image[] Gem_imgs { get => gem_imgs; set => gem_imgs = value; }

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
        Select();
    }

    public override void Select()
    {
        if (!allowSubmit || !interactable) return;
        base.Select();
        Focus(true);
        onEnter?.Invoke(this);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        Focus(false);
        onExit?.Invoke(this);
    }

    public void SetGems(Gem[] gems)
    {

        for (int i = 0; i < gem_imgs.Length; i++)
        {
            if (gems.Length > i && gems[i] != Gem.None)
            {
                gem_imgs[i].gameObject.SetActive(true);
                gem_imgs[i].color = GemUI.GemColor(gems[i]);
            }
            else
            {
                gem_imgs[i].gameObject.SetActive(false);
            }
        }

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
