using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitStatsUI : MonoBehaviour
{
    [SerializeField] HpUI hpUi;
    [SerializeField] SpUI spUi;
    [SerializeField] DiamondUI diamondUI;
    [SerializeField] TextMeshProUGUI name_txt;
    [SerializeField] TextMeshProUGUI hp_txt;
    [SerializeField] TextMeshProUGUI sp_txt;
    [SerializeField] Image portrait_img;
    [SerializeField] Image fraction_flag_img;

    public void SetData(CharacterData data)
    {
        hpUi.Init(0, data.Battle.maxHp, data.Battle.hp);
        spUi.Init(0, data.Battle.maxSp, data.Battle.sp);
        name_txt.text = data.Base.c_name;
        diamondUI.SetGems(data.Battle.gems);
        portrait_img.sprite = data.Portrait.Sprites[data.Battle.emote];
    }

    public void SetData(BattleCharacter character)
    {
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
        
    }


    public void SetSp(float sp)
    {

    }


}
