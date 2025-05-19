// using System;
// using UnityEngine;
// using UnityEngine.UI;
// public class InteractionButtonUI : MonoBehaviour
// {
//     public InteractionUI baseInteraction;
//     public Image LineImage;
//     public Image IconImage;
//     public Text ButtonName;
//     Button button;
//
//     Action_State m_Action;
//     private void Awake()
//     {
//         button = GetComponent<Button>();
//     }
//     public void Initalize(Action_State state)
//     {
//         m_Action = state;
//         if(m_Action == Action_State.None)
//         {
//             GetComponent<Image>().color = new Color(0, 0, 0, GetComponent<Image>().color.a);
//             return;
//         }
//         IconImage.gameObject.SetActive(true);
//         IconImage.sprite = ActionHolder.GetAtlas(state.ToString());
//
//         button.onClick.RemoveAllListeners();
//         button.onClick.AddListener(() => baseInteraction.DeactiveObject());
//         button.onClick.AddListener(() => ActionHolder.Actions[state]());
//     }
// }

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButtonUI : MonoBehaviour
{
    public InteractionUI baseInteraction;
    public Image lineImage;
    public Image iconImage;
    public TMP_Text buttonName;
    private Button button;

    public ActionState actionState;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Initialize(ActionState state)
    {
        actionState = state;
        if (state.Equals(ActionState.None))
        {
            GetComponent<Image>().color = new Color(0, 0, 0, GetComponent<Image>().color.a);
            return;
        }

        iconImage.gameObject.SetActive(true);
        iconImage.sprite = ActionHolder.GetAtlas(state.ToString());
        
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => baseInteraction.DeactiveObject());
        button.onClick.AddListener(() => ActionHolder.actions[state]());
    }
}