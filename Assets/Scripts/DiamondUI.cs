using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DiamondUI : MonoBehaviour
{
    [SerializeField] protected bool isActive = false;
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] protected GemUI[] gems;
    [SerializeField] protected GemUI[] fake_gems;
    [SerializeField] Gem[] cacheData;

    public Action onSubmit;

    public void Active(bool value)
    {
        isActive = value;
    }

    public void SetGems(Gem[] gemType)
    {
       for(int i = 0; i < gemType.Length; i++)
        {
            gems[i].Set(gemType[i]);
        }
    }

    public void SetGem(int slot, Gem gemType)
    {
        gems[slot].Set(gemType);
    }

    public void SetAmount(int amount)
    {
        text.text = amount.ToString();
    }

    public void PreviewGems(int start, Gem[] gemsData)
    {
        int idx = 0;
        HidePreviewGems();

        for (int i = 0; i < gemsData.Length; i++)
        {
            if (start + i > gems.Length - 1) idx = (start + i) - gems.Length;
            else idx = start + i ;

            //gems[idx].Set(gemsData[i]);
            SetPreviewGem(idx, gemsData[i]);
            Debug.Log(gems[idx] + " : to " + gemsData[i]);
        }

    }

    public void SetPreviewGem(int index, Gem gemType)
    {
        fake_gems[index].transform.position = gems[index].transform.position;
        fake_gems[index].Set(gemType);
        fake_gems[index].gameObject.SetActive(true);
    }

    public void HidePreviewGems()
    {
        for(int i = 0; i < fake_gems.Length; i++)
        {
            fake_gems[i].gameObject.SetActive(false);
        }
    }

    public void PreviewUp()
    {
        if(cacheData.Length >2)//3Gems
        {
            PreviewGems(3, cacheData);
        }
        else if (cacheData.Length > 1)//2Gems
        {
            PreviewGems(0, cacheData);
        }
        else if (cacheData.Length > 0)//1Gem
        {
            PreviewGems(0, cacheData);
        }
    }

    public void PreviewDown()
    {
        if (cacheData.Length > 2)//3Gems
        {
            PreviewGems(1, cacheData);
        }
        else { 
            PreviewGems(0, cacheData);
        }
    }

    public void PreviewLeft()
    {
        if (cacheData.Length > 2)//3Gems
        {
            PreviewGems(2, cacheData);
        }
        else
        {
            PreviewGems(0, cacheData);
        }
    }

    public void PreviewRight()
    {
        if (cacheData.Length > 2)//3Gems
        {
            PreviewGems(0, cacheData);
        }
        else
        {
            PreviewGems(0, cacheData);
        }
    }

    public void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Up");
                PreviewUp();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Down");
                PreviewDown();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Left");
                PreviewLeft();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("Right");
                PreviewRight();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space");
                onSubmit?.Invoke();
                Active(false);
            }
        }
    }
    
}
