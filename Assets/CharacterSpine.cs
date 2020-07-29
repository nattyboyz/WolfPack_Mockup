using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;

public class CharacterSpine : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] GameObject parent;
    [SerializeField] SkeletonAnimation spine;
    [SerializeField] GameObject spine_parent;
    [SerializeField] AnimationClip attack_clip;

    private void Start()
    {
        if (spine != null)
        {
            Spine.TrackEntry track = spine.state.SetAnimation(0, "idle_test", true);
            track.TrackTime = UnityEngine.Random.Range(0, 1.5f);
        }
    }

    public void Attack()
    {
        if(animator!=null && attack_clip!=null)
            StartCoroutine(ieAttack());
    }

    public IEnumerator ieAttack()
    {
        if (animator != null && attack_clip != null)
        {
            animator.SetTrigger("attack");
            yield return WaitTillFinishClip(attack_clip);
        }
        else
        {

            yield break;
        }
    }

    public IEnumerator ieGetHit()
    {
        if (animator != null /*&& attack_clip != null*/)
        {
            animator.SetTrigger("gethit");
            yield return null;
        }
        else
        {

            yield break;
        }
    }

    IEnumerator WaitTillFinishClip(AnimationClip clip)
    {
        yield return new WaitForSeconds(clip.length);
    }
}
