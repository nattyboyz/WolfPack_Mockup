using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(DiamondUI))]
public class DiamondPreview : MonoBehaviour
{
    [SerializeField] DiamondUI diamondUI;
    [SerializeField] protected bool isActive = false;
    [SerializeField] protected GemUI[] fake_gems;
    [SerializeField] Gem[] gemData;
    [SerializeField] Dictionary<int,Gem> gemSlots = new Dictionary<int, Gem>();
    public Action<Dictionary<int, Gem>> onSubmit;
    public Action onExit;

    [SerializeField] UnityEvent activeEvent;
    [SerializeField] UnityEvent deactivateEvent;

    int focus = 0;

    [SerializeField] Image glowBack;
    [SerializeField] Image handleLeft;
    [SerializeField] Image handleRight;
    [SerializeField] GameObject swinger;

    [SerializeField] protected MainControl input;
    protected Vector2 navigate;

    protected virtual void Awake()
    {
        input = new MainControl();
    }

    protected virtual void Start()
    {
        Active(false);
    }

    protected virtual void Enable()
    {
        input.UI.Navigate.started += Navigate_started;
        input.UI.Navigate.canceled += Navigate_canceled;
        input.UI.Navigate.performed += Navigate_performed;
        input.UI.Submit.performed += Submit_performed;
        input.UI.Cancel.performed += Cancel_performed;

        input.UI.Navigate.Enable();
        input.UI.Submit.Enable();
        input.UI.Cancel.Enable();
    }

    protected virtual void Disable()
    {
        input.UI.Navigate.started -= Navigate_started;
        input.UI.Navigate.canceled -= Navigate_canceled; ;
        input.UI.Navigate.performed -= Navigate_performed;
        input.UI.Submit.performed -= Submit_performed;
        input.UI.Cancel.performed -= Cancel_performed;

        input.UI.Navigate.Disable();
        input.UI.Submit.Disable();
        input.UI.Cancel.Disable();
    }

    protected virtual void Navigate_started(InputAction.CallbackContext obj)
    {

    }

    protected virtual void Navigate_canceled(InputAction.CallbackContext obj)
    {

    }

    protected virtual void Navigate_performed(InputAction.CallbackContext obj)
    {
        navigate = obj.ReadValue<Vector2>();
        if (navigate.x > 0 || navigate.y < 0)
        {
            Debug.Log("Right");
            focus++;
            if (focus >= diamondUI.Gems.Length) focus = 0;
            //PreviewGems(focus, gemData);
            UpdateGemSlot(focus, gemData);
            Vector3 rot = swinger.transform.localEulerAngles;
            swinger.transform.DOLocalRotate(new Vector3(rot.x, rot.y, rot.z - 90), 0.2f);
            Bounce(handleRight);
        }
        else
        {
            Debug.Log("Left");
            focus--;
            if (focus < 0) focus = diamondUI.Gems.Length - 1;
            //PreviewGems(focus, gemData);

            UpdateGemSlot(focus, gemData);

            Vector3 rot = swinger.transform.localEulerAngles;
            swinger.transform.DOLocalRotate(new Vector3(rot.x, rot.y, rot.z + 90),0.2f);
            Bounce(handleLeft);
        }
    }

    protected virtual void Submit_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Space");
        onSubmit?.Invoke(gemSlots);
        HidePreviewGems();
        Active(false);
    }

    protected virtual void Cancel_performed(InputAction.CallbackContext obj)
    {
        HidePreviewGems();
        Active(false);
        onExit?.Invoke();
    }

    public void Active(bool value)
    {
        isActive = value;
        if (value)
        {
            diamondUI.gameObject.transform.DOScale(new Vector3(1.4f, 1.4f, 1),0.13f);
            activeEvent.Invoke();
            Enable();
        }
        else
        {
            diamondUI.gameObject.transform.localScale = new Vector3(1f, 1f, 1);
            deactivateEvent.Invoke();
            Disable();
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
        //if (isActive)
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        Debug.Log("Left");
        //        focus--;
        //        if (focus < 0) focus = diamondUI.Gems.Length - 1;
        //        PreviewGems(focus, gemData);
        //        Bounce(handleLeft);
        //    }
        //    else if (Input.GetKeyDown(KeyCode.D))
        //    {
        //        Debug.Log("Right");
        //        focus++;
        //        if (focus >= diamondUI.Gems.Length) focus = 0;
        //        PreviewGems(focus, gemData);
        //        Bounce(handleRight);
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        Debug.Log("Space");
        //        onSubmit?.Invoke(gemSlots);
        //        HidePreviewGems();
        //        Active(false);
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Escape) || (Input.GetMouseButtonDown(1)))
        //    {
        //        Debug.Log("Exit");
        //        StartCoroutine(ieExit());
        //    }
        //}
    }

    //public IEnumerator ieExit()
    //{
    //    HidePreviewGems();
    //    Active(false);
    //    yield return new WaitForSeconds(0.2f);
    //    onExit?.Invoke();
    //}

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

    void UpdateGemSlot(int start, Gem[] gemsData)
    {
        int idx = 0;
        //HidePreviewGems();
        gemSlots.Clear();


        for (int i = 0; i < gemsData.Length; i++)
        {
            if (start + i >= diamondUI.Gems.Length) idx = (start + i) - diamondUI.Gems.Length;
            else idx = start + i;
            //SetPreviewGem(idx, gemsData[i]);
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
