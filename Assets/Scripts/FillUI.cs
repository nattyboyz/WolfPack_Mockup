using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FillUI : MonoBehaviour
{
    [SerializeField] protected Image bar;
    public MinMax _default;
    public float current;

    protected virtual void Start()
    {
        bar.fillAmount = GetPecent(current);
    }

    public virtual void Init(float min, float max, float cur)
    {
        _default.min = min;
        _default.max = max;
        current = cur;
        bar.fillAmount = GetPecent(current);
    }

    public virtual void Modify(float value)
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

        bar.fillAmount = GetPecent(current);
    }

    public virtual float GetPecent(float value)
    {
        return value / _default.max;
    }

    public virtual void SetMax(float value)
    {
        _default.max = value;
    }

    public virtual void SetMin(float value)
    {
        _default.min = value;
    }

    public virtual void SetValue(float value)
    {
        Modify(value - current);
    }
}

[Serializable]
public struct MinMax
{
    public float min;
    public float max;
}