using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvokationUI : MonoBehaviour
{
    [SerializeField] Canvas main_canvas;
    [SerializeField] RectTransform parent;
    [SerializeField] GemUI[] gemUIs;


    private void Awake()
    {
        Active(false);
    }

    public void Active(bool value)
    {
        main_canvas.enabled = value;
    }

    public void Set(Gem[] gems)
    {
        for(int i=0;i< gemUIs.Length; i++)
        {
            if (i < gems.Length)
            {
                gemUIs[i].Set(gems[i]);
                gemUIs[i].gameObject.SetActive(true);
            }
            else
            {
                gemUIs[i].gameObject.SetActive(false);

            }
        }
    }

    public void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }

}
