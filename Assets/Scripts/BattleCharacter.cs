using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Team { Player,CPU}

public class BattleCharacter : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] Team type;
    [SerializeField] string id;
    [SerializeField] CharacterData characterData;
    [Header("Graphic")]
    [SerializeField] GameObject graphic;
    [SerializeField] CharacterSpine characterSpine;
    [Header("UI")]
    [SerializeField] OverHeadUI overheadUI;


    public CharacterData Data { get => characterData;}
    public OverHeadUI OverheadUI { get => overheadUI;}
    public Team Type { get => type; set => type = value; }
    public CharacterSpine CharacterSpine { get => characterSpine; set => characterSpine = value; }

    private void Awake()
    {
        overheadUI.gameObject.SetActive(false);
    }

    public void Focus(bool active)
    {
        overheadUI.gameObject.SetActive(active);
        if (active) graphic.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        else graphic.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void GetActSkill(ActSkillData skillData) //Add onComplete
    {

        TakeGemDamage(Gem.Green);
        //for (int i = 0; i < skillData.Gems.Length; i++)
        //{
        //    for(int j =0;j < Data.Battle.gems.Length; j++)
        //    {
        //        //if (Data.Battle.gems[j] != skillData.Gems[i])
        //        //{
        //        //    Data.Battle.gems[j]
        //        //}
        //    }
        //}
       
    }

    public void TakeGemDamage(Gem gem)
    {
        Gem[] g = Data.Battle.gems;
        //Util.Shuffle(g);
        int start = UnityEngine.Random.Range(0, 4);

        for (int i = 0; i < g.Length; i++)
        {
            int idx = (start + i);
            if (idx > g.Length) idx -= g.Length;

            if (g[idx] != gem)
            {
                g[idx] = gem;
                overheadUI.SetGem(idx, gem);
                break;
            }
        }
    }
}
