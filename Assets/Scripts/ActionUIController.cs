using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ActionUIController : MonoBehaviour
{
    [SerializeField] Canvas main_canvas;
    [SerializeField] BattleController battleCtrl;
    [SerializeField] Selectable start_selector;
    public Action onAttackClick;
    public Action onActClick;
    public Action onItemClick;
    public Action onSkipClick;
 
    private void Start()
    {
        Hide();
    }

    public void Show(Vector3 pos)
    {
        Debug.Log("Show");
        main_canvas.transform.position = pos;
        main_canvas.enabled = true;
        start_selector.Select();
    }

    public void Hide()
    {
        main_canvas.enabled = false;
        //main_canvas.transform.position = pos;
    }

    #region Button function

    public void Btn_Attack()
    {
        //battleCtrl.MoveTurnForward();
        //battleCtrl.ExecuteTurn();
        Hide();
        onAttackClick?.Invoke();
    }

    public void Btn_Act()
    {
        //battleCtrl.MoveTurnForward();
        //battleCtrl.ExecuteTurn();
        Hide();
        onActClick?.Invoke();
    }

    public void Btn_Item()
    {
        //battleCtrl.MoveTurnForward();
        //battleCtrl.ExecuteTurn();
        Hide();
        onItemClick?.Invoke();
    }

    public void Btn_Skip()
    {
        //battleCtrl.MoveTurnForward();
        //battleCtrl.ExecuteTurn();
        Hide();
        onSkipClick?.Invoke();
    }

    #endregion

}
