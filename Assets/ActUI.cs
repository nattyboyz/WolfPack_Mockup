using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActUI : MonoBehaviour
{
    [SerializeField] UnitSelection unitSelection;
    [SerializeField] UnitStatsUIController unitStatsUI;

    [SerializeField] List<ActButton> buttons = new List<ActButton>();
    [SerializeField] Canvas main_canvas;
    [SerializeField] Selectable start_selector;
    int selected_index = 0;
    int page = 1;
    int maxPage = 2;

    void Start()
    {
        Active(false);

    }

    public void Init(BattleCharacter character, BattleController battleCtrl)
    {
        transform.position = new Vector3(character.transform.position.x, character.transform.position.y +4.5f, character.transform.position.z);
        SkillData[] skils = character.Data.Stats.skills;
        for(int i = 0; i < buttons.Count; i++)
        {
            SkillData data = null;
            if(skils.Length > i * page)
            {
                data = character.Data.Stats.skills[i];
            }

            if (data != null)
            {
                buttons[i].onClick = () =>
                {  
                    unitSelection.onCancel = () => {
                        Active(true);
                    };
                    unitSelection.onSubmit = (slots) => {
                        battleCtrl.ApplySkill(character, slots, data);
                    };
                    unitSelection.onSelect = (slots) => {
                        unitStatsUI.Show(slots[0].Character.Data, UnitStatsUIController.Side.Right);
                    };

                    unitSelection.ActiveUnitSelection(TargetMode.Single,3);
                    Active(false);
                    //Debug.Log("Click button");
                };
            }
            else
            {
                buttons[i].onClick = null;
            }

            buttons[i].Set(data);
        }
    }

    void SubmitTarget(List<BattleCharacterSlot> slots)
    {

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
            foreach(ActButton btn in buttons)
            {
                btn.allowSubmit = true;
            }

            if (selected_index >= 0) buttons[selected_index].Select();
            else start_selector.Select();
        }
        else
        {
            foreach (ActButton btn in buttons)
            {
                btn.allowSubmit = false;
            }
        }
        main_canvas.enabled = active;
    }
}
