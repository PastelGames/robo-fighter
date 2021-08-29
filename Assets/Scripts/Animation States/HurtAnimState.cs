using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAnimState : StateMachineBehaviour
{
    FighterController _fc;
    Hurtbox _hurtbox;
    public int _stunDurationInFrames;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Turn off hitbox.
        _hurtbox = animator.GetComponentInParent<Fighter>()._hurtbox;
        _hurtbox.gameObject.SetActive(false);

        //Stop the player from moving.
        _fc = animator.GetComponentInParent<FighterController>();
        _fc.canMove = false;

        //Set the length of the hitstun to be desired length.
        animator.speed = 100f / (float) _stunDurationInFrames;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _fc.canMove = true;
        _hurtbox.gameObject.SetActive(true);
        animator.speed = 1;
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
