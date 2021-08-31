using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockstunAnimState : StateMachineBehaviour
{
    Fighter fighter;
    Hurtbox hurtbox;
    public int stunDurationInFrames;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fighter = animator.GetComponentInParent<Fighter>();
        fighter.canMove = false;
        fighter.isBlocking = true;

        //Turn off the player's hurtbox.
        fighter.hurtbox.gameObject.SetActive(false);

        animator.speed = 100f / (float) stunDurationInFrames;
        //animator.speed = animator.GetCurrentAnimatorClipInfo(0).Length / (float) stunDurationInFrames;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fighter.hurtbox.gameObject.SetActive(true);
        animator.speed = 1;
        fighter.isBlocking = false;
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
