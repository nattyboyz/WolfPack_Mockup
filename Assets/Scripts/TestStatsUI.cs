using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestStatsUI : MonoBehaviour
{
    public enum TargetMode{Left, Right}
    TargetMode mode;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    [SerializeField] GameObject[] leftStatsUI;
    [SerializeField] GameObject[] rightStatsUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeMode();
        }
    }

    void ChangeMode()
    {
        if(mode== TargetMode.Left)
        {
            mode = TargetMode.Right;
        }
        else
        {
            mode = TargetMode.Left;
        }
        Debug.Log("Change mode to " + mode);
    }

    public void ShowBotUI(int index)
    {
        GameObject g;
        if (index < 3)//Left
        {
            g = leftStatsUI[index];
            if (left != null && left != g) left.SetActive(false); 
      
            left =g;
        }
        else//Right
        {
            //rightStatsUI[index-3].SetActive(true);
            g = rightStatsUI[index - 3];
            if (right != null && right != g) right.SetActive(false);
            right = g;
        }
        g.SetActive(true);
    }


    public void Show(CharacterDetail detail, TargetMode side)
    {

    }


    public void Hide()
    {
        if(mode == TargetMode.Left)
        {
            if (left != null) left.SetActive(false);
            left = null;
        }
        else
        {
            if (right != null) right.SetActive(false);
            right = null;
        }
    }


   // public void Show
}

public enum Gem { None,Red,Purple,Blue,Green}

public class CharacterDetail
{
    public string name;
    public Sprite portrait;
    public int hp;
    public int maxHp;
    public int sp;
    public int maxSp;
    public Gem[] gems;
    public int gemCount;
    public int faction;
}

