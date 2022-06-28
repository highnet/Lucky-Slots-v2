using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwaitBet : StateMachineBehaviour
{

    private BetAwaiter betAwaiter;
    private GameState gameState;
    private UI ui;

    private void Awake()
    {
        betAwaiter = GameObject.FindGameObjectWithTag("Bet Awaiter").GetComponent<BetAwaiter>();
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();

    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ui.MakeChangeBetButtonInteractable(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (betAwaiter.GetSpinInput())
        {
            gameState.SetTrigger("Awaited Bet");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ui.MakeChangeBetButtonInteractable(false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
