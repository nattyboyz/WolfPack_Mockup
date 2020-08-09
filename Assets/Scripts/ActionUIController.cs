using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;

public class ActionUIController : MonoBehaviour
{
    [SerializeField] Canvas main_canvas;
    [SerializeField] BattleController battleCtrl;
    //[SerializeField] Selectable start_selector;
    public Action<string> onSubmit;
    public Action onExit;


    [SerializeField] TextMeshProUGUI header_txt;
    [SerializeField] List<ActionButton> buttons = new List<ActionButton>();

    [SerializeField] int selected_index = 0;
    [SerializeField] bool isActive = false;
    [SerializeField] bool allowSelect = true;
    [SerializeField] float x_distance = 80;
    [SerializeField] int visibleRange = 3;

    [SerializeField] protected MainControl input;

    Sequence menuMove;

    private void Awake()
    {
        input = new MainControl();
    }

    private void Enable()
    {
        input.UI.Navigate.started += Navigate_started;
        input.UI.Navigate.canceled += Navigate_canceled;
        input.UI.Navigate.performed += Navigate_performed;
        input.UI.Submit.performed += Submit_performed;
        input.UI.Cancel.performed += Cancel_performed;

        input.UI.Navigate.Enable();
        input.UI.Submit.Enable();
    }

    private void Disable()
    {
        input.UI.Navigate.started -= Navigate_started;
        input.UI.Navigate.canceled -= Navigate_canceled; ;
        input.UI.Navigate.performed -= Navigate_performed;
        input.UI.Submit.performed -= Submit_performed;
        input.UI.Cancel.performed -= Cancel_performed;

        input.UI.Navigate.Disable();
        input.UI.Submit.Disable();
    }


    private void Navigate_canceled(InputAction.CallbackContext obj)
    {
        //Debug.Log("Cancel");
        navigationPress = false;
    }

    private void Navigate_started(InputAction.CallbackContext obj)
    {
        //Debug.Log("Start");
        navigationPress = true;
    }


    bool navigationPress = false;
    private void Update()
    {
        if (!isActive) return;

        if (allowSelect && navigationPress && navigate.x<0)
        {
            TargetShift2(-1);
        }
        else if (allowSelect && navigationPress && navigate.x > 0)
        {
            TargetShift2(1);
        }
        //input.uiInputModule.submit.action += () => { };

        //if (/*allowSelect &&*/ Input.GetKeyDown(KeyCode.A))
        //{
        //    TargetShift2(-1);
        //}
        //else if (/*allowSelect && */Input.GetKeyDown(KeyCode.D))
        //{
        //    TargetShift2(1);
        //}
        //else if (/*allowSelect &&*/ Input.GetKeyDown(KeyCode.Space))
        //{
        //    //Debug.Log(buttons[selected_index].value);
        //    onSubmit?.Invoke(buttons[selected_index].value);
        //}
        //else if (allowSelect && Input.GetKey(KeyCode.A))
        //{
        //    TargetShift2(-1);
        //}
        //else if (allowSelect && Input.GetKey(KeyCode.D))
        //{
        //    TargetShift2(1);
        //}
        //else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Escape) || (Input.GetMouseButtonDown(1)))
        //{
        //    onExit?.Invoke();
        //}
    }

    Vector2 navigate;

    private void Submit_performed(InputAction.CallbackContext obj)
    {
        //Debug.Log("Submit");
        onSubmit?.Invoke(buttons[selected_index].value);
        // throw new NotImplementedException();
    }

    private void Cancel_performed(InputAction.CallbackContext obj)
    {
        //Debug.Log("Cancel");
        onExit?.Invoke();
    }

    private void Navigate_performed(InputAction.CallbackContext obj)
    { 
        navigate = obj.ReadValue<Vector2>();
        //Debug.Log("Navigate " + navigate.x);
        if (navigate.x > 0)
        {
            TargetShift2(1);
        }
        else if(navigate.x < 0)
        {
            TargetShift2(-1);
        }
        //throw new NotImplementedException();
    }

    private void Start()
    {
        TargetShift2(0);
        Hide();
    }

    //void TargetShift(int mod)
    //{
    //    if (selected_index + mod >= buttons.Count) selected_index = 0;
    //    else if (selected_index + mod < 0) selected_index = buttons.Count - 1;
    //    else selected_index += mod;





    //    int half = Mathf.FloorToInt((buttons.Count-1) * 0.5f);
    //    int f = selected_index;
    //    int a = 0;


    //    int h = Mathf.FloorToInt(open * 0.5f);
    //    int floor = f-h;
    //    if (floor < 0)
    //    {
    //        floor = buttons.Count+ floor;
    //    }

    //    int ceil = f + h;
    //    if (ceil > buttons.Count)
    //    {
    //        ceil = ceil - buttons.Count;
    //    }

    //    Debug.Log(floor + ":" + ceil);

        

    //    for (int i =0; i< buttons.Count; i++)
    //    {
    //        int idx = (f + i);
    //        int sign = 1;

    //        if (idx >= buttons.Count) idx -= buttons.Count;
    //        if (i > half)
    //        {
    //            sign = -1;
    //           // a = buttons.Count - half - 1;
    //        }
    //        //else
    //        //{
    //        //    a = i;
    //        //}

    //        float pos = sign * x_distance * (i- (2*a));
    //        buttons[idx].transform.localPosition = new Vector3(pos, buttons[idx].transform.localPosition.y, buttons[idx].transform.localPosition.z);

    //        //if (i > half)
    //        //{
    //        //    a--;
    //        //}
    //        Debug.Log(i + " - " + a + " " + pos);
    //        if (i > half)
    //        {
    //            a++;
    //        }

    //        if (idx > ceil || idx < floor)
    //        {
    //            buttons[idx].gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            buttons[idx].gameObject.SetActive(true);
    //        }
    //        //a++;
    //    }
    //    // Select(selected_index);
    //    //Debug.Log("Select " + character.Data.Base.c_name);
    //}

    void TargetShift2(int mod)
    {
         int swapIdx;
        header_txt.text = "";
        allowSelect = false;
       if (mod > 0)
        {
            ActionButton b = buttons[0];
            buttons.RemoveAt(0);
            buttons.Add(b);
            swapIdx = buttons.Count-1;
        }
        else if(mod<0)
        {
            ActionButton b = buttons[buttons.Count-1];
            buttons.RemoveAt(buttons.Count - 1);
            buttons.Insert(0,b);
            swapIdx = 0;
        }
        else
        {
            swapIdx = -1;
        }

        int half = Mathf.FloorToInt((buttons.Count - 1) * 0.5f);
        selected_index = half;

        if(menuMove!=null && menuMove.IsPlaying())
        {
            menuMove.Kill();
        }

        menuMove = DOTween.Sequence();

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
                menuMove.Join(buttons[i].transform.DOLocalMoveX(pos, 0.2f));
                if(i>half+ Mathf.FloorToInt(visibleRange*0.5f) || i < half - Mathf.FloorToInt(visibleRange * 0.5f))
                {
                    menuMove.Join(buttons[i].image.DOFade(0,0.1f));
                    menuMove.Join(buttons[i].icon.DOFade(0, 0.1f));
                }
                else
                {
                    menuMove.Join(buttons[i].image.DOFade(1, 0.1f));
                    menuMove.Join(buttons[i].icon.DOFade(1, 0.1f));
                }
            }
        }

        menuMove.OnComplete(() => {
            header_txt.text = buttons[selected_index].value;
            allowSelect = true;
        });
        menuMove.Play();
    }

    public void Active(bool value)
    {
        isActive = value;
        main_canvas.enabled = value;
        if (value)
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }

    public void SetPosition(Vector3 pos)
    {
        main_canvas.transform.position = pos;
    }

    public void Hide()
    {
        main_canvas.enabled = false;
        Active(false);
        //Disable();
    }

    #region Button function

    //public void Btn_Attack()
    //{
    //    //battleCtrl.MoveTurnForward();
    //    //battleCtrl.ExecuteTurn();
    //    Hide();
    //    onAttackClick?.Invoke();
    //}

    //public void Btn_Act()
    //{
    //    //battleCtrl.MoveTurnForward();
    //    //battleCtrl.ExecuteTurn();
    //    Hide();
    //    onActClick?.Invoke();
    //}

    //public void Btn_Item()
    //{
    //    //battleCtrl.MoveTurnForward();
    //    //battleCtrl.ExecuteTurn();
    //    Hide();
    //    onItemClick?.Invoke();
    //}

    //public void Btn_Skip()
    //{
    //    //battleCtrl.MoveTurnForward();
    //    //battleCtrl.ExecuteTurn();
    //    Hide();
    //    onSkipClick?.Invoke();
    //}

    #endregion

}
