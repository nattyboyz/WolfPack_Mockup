using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class ActionUIController : MonoBehaviour
{
    [SerializeField] Canvas main_canvas;
    [SerializeField] BattleController battleCtrl;
    [SerializeField] Selectable start_selector;
    public Action onAttackClick;
    public Action onActClick;
    public Action onItemClick;
    public Action onSkipClick;

    public Action onExit;

    [SerializeField] List<Button> buttons = new List<Button>();

    [SerializeField] int selected_index = 0;
    [SerializeField] bool isActive = false;
    [SerializeField] float x_distance = 80;
    [SerializeField] int open = 3;

    private void Update()
    {

        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            TargetShift2(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            TargetShift2(1);
        }
        //else if (Input.GetKeyDown(KeyCode.A))
        //{
        //    PageShift(-1);
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    PageShift(1);
        //}
        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Escape) || (Input.GetMouseButtonDown(1)))
        {
            onExit?.Invoke();
        }
    }

    private void Start()
    {
        Hide();
    }

    void TargetShift(int mod)
    {
        if (selected_index + mod >= buttons.Count) selected_index = 0;
        else if (selected_index + mod < 0) selected_index = buttons.Count - 1;
        else selected_index += mod;





        int half = Mathf.FloorToInt((buttons.Count-1) * 0.5f);
        int f = selected_index;
        int a = 0;


        int h = Mathf.FloorToInt(open * 0.5f);
        int floor = f-h;
        if (floor < 0)
        {
            floor = buttons.Count+ floor;
        }

        int ceil = f + h;
        if (ceil > buttons.Count)
        {
            ceil = ceil - buttons.Count;
        }

        Debug.Log(floor + ":" + ceil);

        

        for (int i =0; i< buttons.Count; i++)
        {
            int idx = (f + i);
            int sign = 1;

            if (idx >= buttons.Count) idx -= buttons.Count;
            if (i > half)
            {
                sign = -1;
               // a = buttons.Count - half - 1;
            }
            //else
            //{
            //    a = i;
            //}

            float pos = sign * x_distance * (i- (2*a));
            buttons[idx].transform.localPosition = new Vector3(pos, buttons[idx].transform.localPosition.y, buttons[idx].transform.localPosition.z);

            //if (i > half)
            //{
            //    a--;
            //}
            Debug.Log(i + " - " + a + " " + pos);
            if (i > half)
            {
                a++;
            }

            if (idx > ceil || idx < floor)
            {
                buttons[idx].gameObject.SetActive(false);
            }
            else
            {
                buttons[idx].gameObject.SetActive(true);
            }
            //a++;
        }
        // Select(selected_index);
        //Debug.Log("Select " + character.Data.Base.c_name);
    }

    void TargetShift2(int mod)
    {
        int swapIdx;
       if (mod > 0)
        {
            Button b = buttons[0];
            buttons.RemoveAt(0);
            buttons.Add(b);
            swapIdx = buttons.Count-1;
        }
        else
        {
            Button b = buttons[buttons.Count-1];
            buttons.RemoveAt(buttons.Count - 1);
            buttons.Insert(0,b);
            swapIdx = 0;
        }

        int half = Mathf.FloorToInt((buttons.Count - 1) * 0.5f);
  

        for (int i = 0; i < buttons.Count; i++)
        {
            if(i == half)
            {
                buttons[i].transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                buttons[i].transform.localScale = new Vector3(0.7f, 0.7f, 1);
            }
            float pos = -(x_distance * half) + i*x_distance;
            // buttons[i].transform.localPosition = new Vector3(pos, buttons[i].transform.localPosition.y, buttons[i].transform.localPosition.z);
            if (i == swapIdx)
            {
                buttons[i].transform.localPosition = new Vector3(pos, buttons[i].transform.localPosition.y, buttons[i].transform.localPosition.z);
            }
            else
            {
                buttons[i].transform.DOLocalMoveX(pos, 0.1f);
            }
        }
    }


    public void Active(bool value)
    {
        isActive = value;
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
