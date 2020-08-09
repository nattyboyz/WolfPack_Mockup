using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StripeBarUI : MonoBehaviour
{
    [SerializeField] Image cube_prefab;
    [SerializeField] Transform parent;
    [SerializeField] List<Image> cs;

    [SerializeField] Color enable_color;
    [SerializeField] Color disable_color;

    public MinMax _default;
    public float current;

    public virtual void Init(float min, float max, float cur)
    {
        _default.min = min;
        _default.max = max;
        current = cur;
        Create();
        //bar.fillAmount = GetPecent(current);
    }

    public void Create()
    {
        int c = (int)_default.max - cs.Count;
        if (c <= 0)
        {
            return;
        }
        for(int i = 0; i < c; i++)
        {
            Image img = Instantiate(cube_prefab, parent);
            //img.transform.SetParent(parent);
            cs.Add(img);
        }
        
    }
}
