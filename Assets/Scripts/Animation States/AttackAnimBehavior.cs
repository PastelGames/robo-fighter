using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimBehavior : StateMachineBehaviour
{

    Fighter fighter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Make the player unable to move.
        fighter = animator.GetComponentInParent<Fighter>();
        fighter.canMove = false;
        fighter.isAttacking = true;
        fighter.hitOtherPlayer = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Grant the player the ability to move again. 
        fighter.canMove = true;
        fighter.isAttacking = false;

        //If the player was not hit, it was a whiff and you must clear the attack queue.
        if (!fighter.hitOtherPlayer)
        {
            fighter.attackQ.Clear();
        }

        //Turn off the hitbox incase the player gets hit.
        fighter.hitbox.gameObject.SetActive(false);
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
