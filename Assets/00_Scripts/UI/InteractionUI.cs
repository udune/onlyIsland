// using System;
// using UnityEngine;
//
// public enum Interaction_State{
//     Player,
// }
// public class InteractionUI : MonoBehaviour
// {
//     [SerializeField] private float yPosFloat;
//     Animator animator;
//     PlayerController playerController;
//     Interaction_State m_State;
//     public InteractionButtonUI[] interactionButtons;
//     private void Awake()
//     {
//         animator = GetComponent<Animator>();
//     }
//
//     private void Update()
//     {
//         if(playerController != null)
//         {
//             Vector3 targetPosition = playerController.transform.position + new Vector3(0.0f, yPosFloat, 0.0f);
//             transform.position = Camera.main.WorldToScreenPoint(targetPosition);
//         }
//     }
//
//     public void Initalize(PlayerController controller, Interaction_State state)
//     {
//         playerController = controller;
//         m_State = state;
//         var actions = InteractionActions(state);
//         for(int i = 0; i < interactionButtons.Length; i++)
//         {
//             //interactionButtons[i].Initalize(actions[i]);
//         }
//         animator.Play("Hexagon_Open");
//     }
//
//     public void DeactiveObject()
//     {
//         if (gameObject.activeSelf == false) return;
//         animator.Play("Hexagon_Hide");
//     }
//
//     public void Deactive() => gameObject.SetActive(false);
//
//     private Action_State[] InteractionActions(Interaction_State state)
//     {
//         Action_State[] actions = new Action_State[6];
//         switch(state)
//         {
//             case Interaction_State.Player:
//                 actions[0] = Action_State.InviteParty;
//                 actions[1] = Action_State.Trade;
//                 actions[2] = Action_State.InviteGuild;
//                 break;
//         }
//         return actions;
//     }
//   
// }

using System;
using UnityEngine;

public enum InteractionState
{
    Player
}

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private float yPos;
    private Animator animator;
    PlayerController controller;
    InteractionState interactionState;
    public InteractionButtonUI[] interactionButtons;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (controller != null)
        {
            Vector3 targetPosition = controller.transform.position + new Vector3(0, yPos, 0);
            transform.position = Camera.main.WorldToScreenPoint(targetPosition);
        }
    }

    public void Initialize(PlayerController controller, InteractionState state)
    {
        this.controller = controller;
        interactionState = state;
        var actions = InteractionActions(state);
        for (int i = 0; i < interactionButtons.Length; i++)
        {
            interactionButtons[i].Initialize(actions[i]);
        }
        animator?.Play("Hexagon_Open");
    }

    public void DeactiveObject()
    {
        animator?.Play("Hexagon_Hide");
    }
    
    public void Deactive() => gameObject.SetActive(false);

    private ActionState[] InteractionActions(InteractionState state)
    {
        ActionState[] actions = new ActionState[6];
        
        switch (state)
        {
            case InteractionState.Player:
                actions[0] = ActionState.InviteParty;
                actions[1] = ActionState.Trade;
                actions[2] = ActionState.InviteGuild;
                break;
        }

        return actions;
    }
}