using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

[CreateAssetMenu (menuName = "PluggableSM/Actions/Idle")]
public class IdleAction : Action
{
    [SerializeField] private AnimationClip _idleAnimationClip;
    public override void Act(StateController controller)
    {

    }

    public override void EndAct(StateController controller)
    {

    }

    public override void StartAct(StateController controller)
    {
        controller.GetComponent<AnimancerComponent>().Play(_idleAnimationClip, 0.25f);
        Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();
        Fighter fighter = controller as Fighter;
        fighter.comboHitCount = 0;
        rb.velocity = Vector2.zero;
    }
}