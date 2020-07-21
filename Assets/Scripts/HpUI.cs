using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : FillUI
{
    [SerializeField] Image hpBar;

    protected virtual void Start()
    {
        hpBar.fillAmount = GetPecent(current);
    }

    public override void Set(float min, float max, float cur)
    {
        base.Set(min, max, cur);
        hpBar.fillAmount = GetPecent(current);
    }

    public override void Modify(float value)
    {
        base.Modify(value);
        hpBar.fillAmount = GetPecent(current);

    }
}
