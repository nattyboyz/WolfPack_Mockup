using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestCameraMovement : MonoBehaviour
{
    //[SerializeField] Camera LeftCam;
    //[SerializeField] Camera RightCam;
    [SerializeField] Camera mainCam;
    [SerializeField] Camera mainUICam;

    [SerializeField] Camera leftCam;
    [SerializeField] Camera rightCam;

    [SerializeField] Camera[] LeftCam;
    [SerializeField] Camera[] RightCam;

    Sequence leftCamSeq;
    Sequence rightCamSeq;
    Sequence mainCamSeq;

    float startFOV = 0;
    Vector3 startPosition;
    Vector3 startRotation;



    bool isLeft = false;
    bool isRight = false;


    private void Start()
    {
        startFOV = mainCam.fieldOfView;
        startPosition = mainCam.transform.position;
        startRotation = mainCam.transform.rotation.eulerAngles;
    }

    public void Zoom()
    {

    }

    public void ZoomTarget(BattleCharacter character)
    {

    }

    public void Target(BattleCharacter character)
    {

    }

    public void CloseUp(BattleCharacter character)
    {

    }


    public void TransitionIn()
    {
        PanInFromLeft();
    }

    public void TransitionOut()
    {

    }

    public void SetToStartPos()
    {

    }

    void PanInFromLeft()
    {

    }



    public void FocusP1()
    {
        //LeftCam.transform.position = new Vector3(-5.832f, 9.522f, -34.12f);
        //LeftCam.fieldOfView = 12;
        Camera c = LeftCam[0];
        if (leftCam != null && leftCam != c) leftCam.gameObject.SetActive(false);
        c.gameObject.SetActive(true);
        leftCam = c;

        isLeft = true;
        if (isRight) mainCam.gameObject.SetActive(false);

    }

    public void FocusP2()
    {
        //LeftCam.transform.position = new Vector3(-3.5f, 11.5f, -34.12f);
        //LeftCam.fieldOfView = 12;
        Camera c = LeftCam[1];
        if (leftCam!=null && leftCam != c) leftCam.gameObject.SetActive(false);
       c.gameObject.SetActive(true);
        leftCam = c;
        ///  LeftCam

        isLeft = true;
        if (isRight) mainCam.gameObject.SetActive(false);
    }

    public void FocusP3()
    {
        //LeftCam.transform.position = new Vector3(-3.5f, 11.5f, -34.12f);
        //LeftCam.fieldOfView = 12;
        Camera c = LeftCam[2];
        if (leftCam != null && leftCam != c) leftCam.gameObject.SetActive(false);
        c.gameObject.SetActive(true);
        leftCam = c;

        isLeft = true;
        if (isRight) mainCam.gameObject.SetActive(false);
    }

    public void FocusP4()
    {

        Camera c = RightCam[0];
        if (rightCam != null && rightCam != c) rightCam.gameObject.SetActive(false);
        c.gameObject.SetActive(true);
        rightCam = c;

        isRight = true;
        if (isLeft) mainCam.gameObject.SetActive(false);
    }

    public void FocusP5()
    {
        Camera c = RightCam[1];
        if (rightCam != null&&rightCam != c) rightCam.gameObject.SetActive(false);
        c.gameObject.SetActive(true);
        rightCam = c;

        isRight = true;
        if (isLeft) mainCam.gameObject.SetActive(false);
    }

    public void FocusP6()
    {
        Camera c = RightCam[2];
        if (rightCam != null&&rightCam != c) rightCam.gameObject.SetActive(false);
        c.gameObject.SetActive(true);
        rightCam = c;

        isRight = true;
        if (isLeft) mainCam.gameObject.SetActive(false);
    }


}
