using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ListoButton : Selectable, IPointerEnterHandler,
    IPointerExitHandler, ISelectHandler,
    IDeselectHandler, IPointerClickHandler, ISubmitHandler
{
    [SerializeField] protected TextMeshProUGUI name_txt;

    public bool allowSubmit = true;
    public Action<string> onClick;
    public Action<ActButton> onEnter;
    public Action<ActButton> onExit;
    public string value;

    public virtual void OnPointerClick(PointerEventData eventData) { }

    public virtual void OnSubmit(BaseEventData eventData) { }

    public virtual void Hide()
    {
        interactable = false;
        name_txt.text = "";
    }

}
