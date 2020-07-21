using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FillUI : MonoBehaviour
{
    public MinMax _default;
    public float current;
    //public Action onFinish = null;

    public virtual void Set(float min, float max, float cur)
    {
        _default.min = min;
        _default.max = max;
        current = cur;
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
    }

    public float GetPecent(float value)
    {
        return  value/_default.max;
    }

}

[Serializable]
public struct MinMax
{
    public float min;
    public float max;
}