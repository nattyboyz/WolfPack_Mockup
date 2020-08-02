using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UnitStatsUI : MonoBehaviour
{
    [SerializeField] HpUI hpUi;
    [SerializeField] SpUI spUi;
    [SerializeField] DiamondUI diamondUI;
    [SerializeField] TextMeshProUGUI name_txt;
    [SerializeField] TextMeshProUGUI hp_txt;
    [SerializeField] TextMeshProUGUI ap_txt;
    [SerializeField] Image portrait_img;
    [SerializeField] Image fraction_flag_img;
    Tweener hpTween;
    

    CharacterData data;

    void SetData(CharacterData data)
    {
        this.data = data;
        hpUi.Init(0, data.Battle.maxHp, data.Battle.hp);
        spUi.Init(0, data.Battle.maxAp, data.Battle.ap);

        hp_txt.text = data.Battle.hp.ToString() + "/" + data.Battle.maxHp.ToString();
        ap_txt.text = data.Battle.ap.ToString() + "/" + data.Battle.maxAp.ToString();

        name_txt.text = data.Base.C_name;
        diamondUI.SetGems(data.Battle.gems);
        portrait_img.sprite = data.Portrait.Sprites[data.Battle.emote];
    }

    public void Set(BattleCharacter character)
    {
        character.UnitStatUI = this;
        SetData(character.Data);
    }
    
    public void SetGem(int slot, Gem gem)
    {

    }

    public void SetGems(int[] slots, Gem gems)
    {

    }

    public void SetHp(float hp)
    {
        hpUi.Init(0, data.Battle.maxHp, data.Battle.hp);
        hp_txt.text = data.Battle.hp.ToString() + "/" + data.Battle.maxHp.ToString();
    }

    public void SetSp(float sp)
    {
        spUi.Init(0, data.Battle.maxAp, data.Battle.ap);
        ap_txt.text = data.Battle.ap.ToString() + "/" + data.Battle.maxAp.ToString();
    }

    public IEnumerator ieModifyGems(Dictionary<int, Gem> gemSlots)
    {
        StartCoroutine (diamondUI.ieModifyGems(gemSlots));
        yield return null;
    }

    public IEnumerator ieModifyHp(float from, float value)
    {
        //int cur_hp = (int)from;
        //int target_hp;

        //hpUi.Init(0, data.Battle.maxHp, data.Battle.hp);
        //hp_txt.text = data.Battle.hp.ToString() + "/" + data.Battle.maxHp.ToString();
        //hpTween = DOTween.To(() => cur_hp, x => cur_hp = x, cur_hp+(int)value, hpUi.ModifyDuration*0.5f).
        //    OnUpdate(()=> {
        //        hp_txt.text = cur_hp.ToString() + "/" + data.Battle.maxHp.ToString();
        //    });
        StartCoroutine(hpUi.ieModify(value));
        yield return null;
    }

    public void ModifyAp(float value)
    {
        spUi.Init(0, data.Battle.maxAp, data.Battle.ap);
        ap_txt.text = data.Battle.ap.ToString() + "/" + data.Battle.maxAp.ToString();
    }
}
