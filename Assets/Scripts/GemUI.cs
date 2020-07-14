﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GemType { None, Red, Purple, Blue, Green }

public class GemUI : MonoBehaviour
{
    [SerializeField] Image image;
    public static Color32 none = new Color32(29,29,29,255);
    public static Color32 red = new Color32(255, 64, 131, 255);
    public static Color32 purple = new Color32(176, 81, 255, 255);
    public static Color32 blue = new Color32(73, 255, 230, 255);
    public static Color32 green = new Color32(167, 255, 73, 255);

    public void Set(GemType type)
    {
        Color c;
        if(type == GemType.Red)
        {
            c = red;
        }
        else if (type == GemType.Purple)
        {
            c = purple;
        }
        else if (type == GemType.Blue)
        {
            c = blue;
        }
        else if (type == GemType.Green)
        {
            c = green;
        }
        else
        {
            c = none;
        }

        image.color = c;
    }
    
}