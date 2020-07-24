using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(DiamondUI))]
public class DiamondPreview : MonoBehaviour
{
    [SerializeField] DiamondUI diamondUI;
    [SerializeField] protected bool isActive = false;
    [SerializeField] protected GemUI[] fake_gems;
    [SerializeField] Gem[] gemData;
    [SerializeField] Dictionary<int,Gem> gemSlots = new Dictionary<int, Gem>();
    public Action<Dictionary<int, Gem>> onSubmit;
    public Action onCancel;

    [SerializeField] UnityEvent activeEvent;
    [SerializeField] UnityEvent deactivateEvent;

    int focus = 0;

    [SerializeField] Image glowBack;
    [SerializeField] Image handleLeft;
    [SerializeField] Image handleRight;

    private void Start()
    {
        Active(false);
    }

    public void Active(bool value)
    {
        isActive = value;
        if (value)
        {
            diamondUI.gameObject.transform.DOScale(new Vector3(1.4f, 1.4f, 1),0.13f);
            //diamondUI.gameObject.transform.localScale = new Vector3(1.4f, 1.4f, 1);
            activeEvent.Invoke();
        }
        else
        {
            diamondUI.gameObject.transform.localScale = new Vector3(1f, 1f, 1);
            deactivateEvent.Invoke();
        }
        glowBack.gameObject.SetActive(value);
        handleLeft.gameObject.SetActive(value);
        handleRight.gameObject.SetActive(value);
    }

    public void StartChoose(Gem[] gemData, int startIndex = 0)
    {
        this.gemData = gemData;
        focus = startIndex;
        PreviewGems(0, gemData);
    }

    public static void Bounce(Image img)
    {
        Sequence s = DOTween.Sequence();
   
        s.Append(img.rectTransform.DOScale(0.8f, 0.1f));
        s.Append(img.rectTransform.DOScale(1, 0.1f));
        s.Restart();
    }

    public void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Left");
                focus--;
                if (focus < 0) focus = diamondUI.Gems.Length - 1;
                PreviewGems(focus, gemData);
                Bounce(handleLeft);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("Right");
                focus++;
                if (focus >= diamondUI.Gems.Length) focus = 0;
                PreviewGems(focus, gemData);
                Bounce(handleRight);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space");
                onSubmit?.Invoke(gemSlots);
                HidePreviewGems();
                Active(false);
            }
            else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Escape) || (Input.GetMouseButtonDown(1)))
            {
                Debug.Log("Back");
                onCancel?.Invoke();
                HidePreviewGems();
                Active(false);
            }
        }
    }

    public void HidePreviewGems()
    {
        for (int i = 0; i < fake_gems.Length; i++)
        {
            fake_gems[i].gameObject.SetActive(false);
        }
    }

     void PreviewGems(int start, Gem[] gemsData)
    {
        int idx = 0;
        HidePreviewGems();
        gemSlots.Clear();


        for (int i = 0; i < gemsData.Length; i++)
        {
            if (start + i >= diamondUI.Gems.Length) idx = (start + i) - diamondUI.Gems.Length;
            else idx = start + i;
            SetPreviewGem(idx, gemsData[i]);
            gemSlots.Add(idx, gemsData[i]);
            //Debug.Log(idx + " : to " + gemsData[i]);
        }
    }

     void SetPreviewGem(int index, Gem gemType)
    {
        fake_gems[index].transform.position = diamondUI.Gems[index].transform.position;
        fake_gems[index].Set(gemType);
        fake_gems[index].gameObject.SetActive(true);
    }

    // void PreviewUp()
    //{
    //    if (cacheData.Length > 2)//3Gems
    //    {
    //        PreviewGems(3, cacheData);
    //    }
    //    else if (cacheData.Length > 1)//2Gems
    //    {
    //        PreviewGems(0, cacheData);
    //    }
    //    else if (cacheData.Length > 0)//1Gem
    //    {
    //        PreviewGems(0, cacheData);
    //    }
    //}

    // void PreviewDown()
    //{
    //    if (cacheData.Length > 1)//3Gems
    //    {
    //        PreviewGems(2, cacheData);
    //    }
    //    else
    //    {
    //        PreviewGems(0, cacheData);
    //    }
    //}

    // void PreviewLeft()
    //{
    //    if (cacheData.Length > 1)//3Gems
    //    {
    //        PreviewGems(3, cacheData);
    //    }
    //    else
    //    {
    //        PreviewGems(0, cacheData);
    //    }
    //}

    // void PreviewRight()
    //{
    //    if (cacheData.Length > 1)//3Gems
    //    {
    //        PreviewGems(1, cacheData);
    //    }
    //    else
    //    {
    //        PreviewGems(0, cacheData);
    //    }
    //}

}
