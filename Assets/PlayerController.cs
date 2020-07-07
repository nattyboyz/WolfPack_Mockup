using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum PlayerState { None,Idle,Run}
public class PlayerController : MonoBehaviour
{
    public float speedX = 10;
    public SkeletonAnimation skel;
    public PlayerState moveState = PlayerState.None;
    public bool isFlip = false;
    public bool isLock = false;
    public bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttack && Input.GetMouseButtonDown(0))
        {          
            skel.AnimationState.SetAnimation(1, "Slash02", false);
            isAttack = true;
            StartCoroutine(ieLockAnimation(.3f));  
        }

        if (!isLock)
        {
            float hor = Input.GetAxis("Horizontal");
            if (hor > 0.1f || hor < -0.1f)
            {
                float x = (hor * Time.deltaTime * speedX);
                transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
                if (moveState != PlayerState.Run)
                {
                    skel.AnimationState.SetAnimation(0, "Run cycle01", true);
                    moveState = PlayerState.Run;
                }
            }
            else
            {
                if (moveState != PlayerState.Idle)
                {
                    skel.AnimationState.SetAnimation(0, "Idle02", true);
                    moveState = PlayerState.Idle;
                }
            }

            if (hor < 0)
            {
                if (isFlip)
                {
                    isFlip = !isFlip;
                    skel.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else if (hor > 0)
            {
                if (!isFlip)
                {
                    isFlip = !isFlip;
                    skel.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
    }

    IEnumerator ieLockAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack = false;
    }
}
