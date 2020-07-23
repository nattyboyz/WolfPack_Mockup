using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ListoUI : MonoBehaviour
{
    [SerializeField] protected bool isActive = false;
    [SerializeField] protected Canvas main_canvas;
    [SerializeField] protected Selectable start_selector;

    [SerializeField] ListoButton pattern;
    [Min(1)][SerializeField] protected int button_per_page = 4;
    [SerializeField] protected int selected_index = 0;
    [SerializeField] protected int page = 0;
    //[SerializeField] protected int maxPage = 2;

    [SerializeField] protected List<ListoButton> buttons = new List<ListoButton>();
    [SerializeField] List<string> stringList;

    public Action onExit;
   
    protected virtual void Start()
    {

    }

    public virtual void Active(bool active)
    {
        isActive = active;
    }

    public virtual void LateUpdate()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            TargetShift(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            TargetShift(1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            PageShift(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            PageShift(1);
        }
        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Escape))
        {
            onExit?.Invoke();
        }
    }

    public IEnumerator ieExit()
    {
        yield return new WaitForSeconds(0.2f);
        onExit?.Invoke();
    }

    public virtual void TargetShift(int mod)
    {
        int idx = selected_index;
        if (selected_index < 0) idx = selected_index = 0;

        ListoButton btn = null;
        int time = 0;
        do
        {
            if (time > buttons.Count)
            {
                return;
            }
            if (idx + mod >= buttons.Count) idx = 0;
            else if (idx + mod < 0) idx = buttons.Count - 1;
            else idx += mod;
            btn = buttons[idx];
            time++;

        } while (btn == null || !btn.interactable);

        Select(idx);
        //Debug.Log("Select " + character.Data.Base.c_name);
    }

    public virtual void PageShift(int mod)
    {
        int target = page;
        int maxP = Mathf.FloorToInt((float)stringList.Count / (float)button_per_page);
        //Debug.Log("Max page " + maxP);

        if (target + mod < 0) target = maxP;
        else if (target + mod > maxP) target = 0;
        else target += mod;

        //Debug.Log("target " + target);

        int start = button_per_page * target;
        int c = stringList.Count;

        for (int i = 0; i < button_per_page; i++)
        {
            if (i + start < c)
            {
                buttons[i].Set(stringList[i + start]);
            }
            else
            {
                buttons[i].Hide();
            }
        }
        page = target;
    }

    public virtual void Page(int page)
    {
        int maxP = Mathf.FloorToInt((float)stringList.Count / (float)button_per_page);
        this.page = page;
        for (int i = 0; i < button_per_page; i++)
        {
            if (i + (button_per_page * page) < stringList.Count)
            {
                buttons[i].Set(stringList[i + (button_per_page * page)]);
            }
            else
            {
                buttons[i].Hide();
            }
        }
    }

    public virtual void Init(List<string> stringList)
    {
        this.stringList = stringList;
    }

    public virtual void Select(int index)
    {
        Debug.Log(index);
        if(index >= buttons.Count)
        {
            Debug.Log(index + "/" + buttons.Count);
            page = (int)index/(int)buttons.Count;
            index = (index % buttons.Count);
        }
        //Open if want to set 
        Debug.Log("Page " + page);
        if (page > 0) Page(page);

        Debug.Log("Try to select " + index + ":Page " + page) ;
        buttons[index].Select();
        selected_index = index;
    }



}
