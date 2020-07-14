using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUIController : MonoBehaviour
{
    [SerializeField] Canvas main_canvas;
    [SerializeField] BattleController battleCtrl;


    private void Start()
    {
        Hide();
    }

    public void Show(Vector3 pos)
    {
        main_canvas.enabled = true;
        main_canvas.transform.position = pos;
    }

    public void Hide()
    {
        main_canvas.enabled = false;
        //main_canvas.transform.position = pos;
    }

    #region Button function

    public void Btn_Attack()
    {
        battleCtrl.MoveTurnForward();
        battleCtrl.ExecuteTurn();
        Hide();
    }

    public void Btn_Act()
    {
        battleCtrl.MoveTurnForward();
        battleCtrl.ExecuteTurn();
        Hide();
    }

    public void Btn_Item()
    {
        battleCtrl.MoveTurnForward();
        battleCtrl.ExecuteTurn();
        Hide();
    }

    public void Btn_Skip()
    {
        battleCtrl.MoveTurnForward();
        battleCtrl.ExecuteTurn();
        Hide();
    }

    #endregion

}
