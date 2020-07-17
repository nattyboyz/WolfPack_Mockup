using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ListoUI : MonoBehaviour
{
    [SerializeField] protected bool isActive = false;
    [SerializeField] protected Canvas main_canvas;
    [SerializeField] protected Selectable start_selector;

    [SerializeField] ListoButton pattern;
    [Min(1)][SerializeField] protected int button_per_page = 4;
    [SerializeField] protected int selected_index = 0;
    [SerializeField] protected int page = 0;
    [SerializeField] protected int maxPage = 2;

    [SerializeField] protected List<ActButton> buttons = new List<ActButton>();
    [SerializeField] List<string> stringList;

    //[SerializeField] KeyCode[] next;
    //[SerializeField] KeyCode[] previous;
    //[SerializeField] KeyCode[] nextPage;
    //[SerializeField] KeyCode[] prevPage;

    protected virtual void Start()
    {

    }

    public virtual void Active(bool active)
    {
        isActive = active;
    }

    public virtual void Update()
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
    }

    public virtual void TargetShift(int mod)
    {
        int target = selected_index;
        if (selected_index < 0) target = selected_index = 0;

        ActButton btn = null;
        int time = 0;
        do
        {
            if (time > buttons.Count)
            {
                return;
            }
            if (target + mod >= buttons.Count) target = 0;
            else if (target + mod < 0) target = buttons.Count - 1;
            else target += mod;
            btn = buttons[target];
            time++;

        } while (btn == null || !btn.interactable);

        buttons[target].Select();
        selected_index = target;
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

    public virtual void Init(List<string> stringList)
    {
        this.stringList = stringList;
    }


}
