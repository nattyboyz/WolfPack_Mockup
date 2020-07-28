using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HpUI : FillUI
{
    float modifyDuration = 0.4f;
    Tween t;
    public Tween HpTween { get => t; }

    //float modifyFreeze = 0.1f;

    public IEnumerator ieModify(float value)
    {
        if (current + value <= _default.min)
        {
            current = _default.min;
        }
        else if (current + value >= _default.max)
        {
            current = _default.max;
        }
        else
        {
            current += value;
        }

        //bar.fillAmount = GetPecent(current);
        if(t!=null && t.IsPlaying()){ t.Kill(); }
        t = DOTween.To(() => this.bar.fillAmount, x => this.bar.fillAmount = x, GetPecent(current), modifyDuration);

        yield return t.WaitForCompletion();
        //yield return new WaitForSeconds(modifyFreeze);
    }
}
