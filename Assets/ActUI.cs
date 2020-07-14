using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActUI : MonoBehaviour
{
    [SerializeField] List<ActButton> buttons = new List<ActButton>();
    [SerializeField] Canvas main_canvas;

    void Start()
    {
        Active(false);
    }

    public void Init(BattleCharacter character, BattleController battleCtrl)
    {
        transform.position = new Vector3(character.transform.position.x, character.transform.position.y +4.5f, character.transform.position.z);

        for(int i =0;i< buttons.Count; i++)
        {
            buttons[i].onClick = () =>
            {
                battleCtrl.MoveTurnForward();
                battleCtrl.ExecuteTurn();
                Active(false);
                Debug.Log("Click button");
            };

            buttons[i].onEnter = OnSelectButton;
            buttons[i].onExit = OnDeselectButton;
        }
    }

    public void OnSelectButton(ActButton button)
    {
        button.transform.localPosition = new Vector3(27f,
       button.transform.localPosition.y +10f,
      button.transform.localPosition.z);
    }

    public void OnDeselectButton(ActButton button)
    {
        button.transform.localPosition = new Vector3(8.5f,
                    button.transform.localPosition.y -10f,
                    button.transform.localPosition.z);
    }



    public void Active(bool active)
    {
        main_canvas.enabled = active;
    }
}
