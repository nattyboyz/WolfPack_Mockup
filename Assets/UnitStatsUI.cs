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
        hpUi.SetData(0, data.BattleData.maxHp, data.BattleData.hp);
        spUi.SetData(0, data.BattleData.maxSp, data.BattleData.sp);
        name_txt.text = data.BaseData.c_name;
        diamondUI.SetGem(data.BattleData.gems);
        portrait_img.sprite = data.PortraitData.Sprites[data.BattleData.emote];
    }

    public void SetData(BattleCharacter character)
    {
        SetData(character.CharacterData);
    }

}
