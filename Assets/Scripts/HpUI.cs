using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HpUI : FillUI
{
    float modifyDuration = 0.4f;
    Sequence t;
    public Sequence HpTween { get => t; }
    [SerializeField] TextMeshProUGUI hp_txt;
    [SerializeField] TextMeshProUGUI max_hp_txt;
    public float ModifyDuration { get => modifyDuration; set => modifyDuration = value; }

    //float modifyFreeze = 0.1f;

    public IEnumerator ieModify(float value)
    {
        float target;
        if (current + value <= _default.min)
        {
            target = _default.min;
        }
        else if (current + value >= _default.max)
        {
            target = _default.max;
        }
        else
        {
            target= current + value;
        }

        //bar.fillAmount = GetPecent(current);
        if(t!=null && t.IsPlaying()){ t.Kill(); }

        t = DOTween.Sequence();
        t.Join(DOTween.To(() => this.bar.fillAmount, 
            x => this.bar.fillAmount = x,
            GetPecent(target), ModifyDuration));

        if (hp_txt != null)
        {
            int temp = (int)current;
            t.Join(DOTween.To(() => temp,
            x => temp = x,
           (int)target, ModifyDuration));
            t.OnUpdate(()=> {
                hp_txt.text = temp.ToString();
                max_hp_txt.text = _default.max.ToString();
            });
        }

        t.Restart();
        current = target;

        yield return t.WaitForCompletion();
        //yield return new WaitForSeconds(modifyFreeze);
    }
}
