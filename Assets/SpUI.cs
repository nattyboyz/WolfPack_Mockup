using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpUI : FillUI
{
    [SerializeField] Image spBar;

    protected virtual void Start()
    {
        spBar.fillAmount = GetPecent(current);
    }

    public override void SetData(float min, float max, float cur)
    {
        base.SetData(min, max, cur);
        spBar.fillAmount = GetPecent(current);
    }

    public override void Modify(float value)
    {
        base.Modify(value);
        spBar.fillAmount = GetPecent(current);

    }
}
