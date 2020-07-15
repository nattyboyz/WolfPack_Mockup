using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActUI : MonoBehaviour
{

    [SerializeField] List<ActButton> buttons = new List<ActButton>();
    [SerializeField] Canvas main_canvas;
    [SerializeField] Selectable start_selector;
    int page = 1;
    int maxPage = 2;

    void Start()
    {
        Active(false);
    }

    public void Init(BattleCharacter character, BattleController battleCtrl)
    {
        transform.position = new Vector3(character.transform.position.x, character.transform.position.y +4.5f, character.transform.position.z);
        SkillData[] skils = character.CharacterData.Stats.skills;
        for(int i = 0; i < buttons.Count; i++)
        {
            SkillData data = null;
            if(skils.Length > i * page)
            {
                data = character.CharacterData.Stats.skills[i];
            }

            if (data != null)
            {
                buttons[i].onClick = () =>
                {
                    battleCtrl.MoveTurnForward();
                    battleCtrl.ExecuteTurn();
                    Active(false);
                    Debug.Log("Click button");
                };
            }
            else
            {
                buttons[i].onClick = null;
            }

            buttons[i].Set(data);
        }
    }

    //public void OnSelectButton(ActButton button)
    //{
    //    button.transform.localPosition = new Vector3(27f,
    //   button.transform.localPosition.y +10f,
    //  button.transform.localPosition.z);
    //}

    //public void OnDeselectButton(ActButton button)
    //{
    //    button.transform.localPosition = new Vector3(8.5f,
    //                button.transform.localPosition.y -10f,
    //                button.transform.localPosition.z);
    //}



    public void Active(bool active)
    {
        if (active)
        {
            start_selector.Select();
        }
        main_canvas.enabled = active;
    }
}
