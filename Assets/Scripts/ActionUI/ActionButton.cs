using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ActionButton : Selectable, IPointerEnterHandler,
    IPointerExitHandler, ISelectHandler,
    IDeselectHandler, IPointerClickHandler, ISubmitHandler
{
    public bool allowSubmit = true;
    public Action<string> onClick;
    // Action<ActionButton> onEnter;
    //public Action<ActionButton> onExit;
    public string value;
    public Image icon;

    public void Focus(bool value)
    {

    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!allowSubmit || !interactable) return;
        base.OnPointerEnter(eventData);
        Focus(true);
        //onEnter?.Invoke(this);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!allowSubmit || !interactable) return;
        base.OnPointerExit(eventData);
        Focus(false);
        //onExit?.Invoke(this);
    }


    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnSubmit(BaseEventData eventData)
    {

    }

    public virtual void Hide()
    {
        interactable = false;
    }

}
