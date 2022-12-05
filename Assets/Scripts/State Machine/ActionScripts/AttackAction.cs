using UnityEngine;
using Animancer;
using System.Collections.Generic;
using System;

[CreateAssetMenu (menuName = "PluggableSM/Actions/Attack")]
public class AttackAction : Action
{
    [SerializeField] private State _idleState;
    [SerializeField] private AttackData[] _attackData;

    public override void StartAct(StateController controller)
    {
        Fighter fighter = controller as Fighter;
        AnimancerComponent animancerComponent = controller.GetComponent<AnimancerComponent>();
        FighterController fighterController = controller.GetComponent<FighterController>();

        fighter.currentAttackData = fighter.attackQ.Dequeue();
        fighter.currentAttackStringLength++;

        AttackData currentAttackData = GetCurrentAtackData(fighter);

        if (currentAttackData.attackType == AttackType.SpecialAttack)
        {
            fighter.BeginSpecialAttackCooldown();
            fighter.PlaySpecialAttackParticles();
        }

        fighter.animationQ = new Queue<AnimationClip>();
        foreach (AnimationClip animationClip in currentAttackData.clips)
        {
            fighter.animationQ.Enqueue(animationClip);
        }

        StartNextAnimation(animancerComponent, fighterController, fighter);
    }

    private AttackData GetCurrentAtackData(Fighter fighter)
    {
        AttackData currentAttackData;
        if (fighter.currentAttackData.attackType == AttackType.SpecialAttack)
        {
            currentAttackData = _attackData[0];
        }
        else
        {
            currentAttackData = _attackData[fighter.currentAttackStringLength - 1];
        }

        return currentAttackData;
    }

    private void StartNextAnimation(AnimancerComponent animancerComponent, FighterController fighterController, Fighter fighter)
    {
        fighter.currentAnimation = fighter.animationQ.Dequeue();
        string attackAnimationKey = fighter.currentAnimation.name + fighterController.playerSlot.ToString();
        animancerComponent.States.GetOrCreate(attackAnimationKey, fighter.currentAnimation);
        AnimancerState attackAnimationState = animancerComponent.TryPlay(attackAnimationKey, 0, FadeMode.FromStart);
    }

    public override void Act(StateController controller)    
    {
        Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();
        Fighter fighter = controller as Fighter;
        AnimancerComponent animancerComponent = controller.GetComponent<AnimancerComponent>();
        FighterController fighterController = controller.GetComponent<FighterController>();

        string attackAnimationKey = fighter.currentAnimation.name + fighterController.playerSlot.ToString();
        AnimancerState attackAnimationState = animancerComponent.States.GetOrCreate(attackAnimationKey, fighter.currentAnimation);

        if (attackAnimationState.NormalizedTime >= 1)
        {
            if (fighter.animationQ.Count > 0)
            {
                StartNextAnimation(animancerComponent, fighterController, fighter);
            }
            else
            {
                fighter.TransitionToState(fighter.idleState);
            }
        }
        else
        {
            float multiplier = fighter.isFacingLeft ? -1 : 1;
            float velocityX;
            float velocityY;

            AttackData currentAttackData = GetCurrentAtackData(fighter);

            velocityX = currentAttackData.velocityX.Evaluate(attackAnimationState.NormalizedTime);
            velocityY = currentAttackData.velocityY.Evaluate(attackAnimationState.NormalizedTime);
            
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                rb.velocity = new Vector3(velocityX * multiplier, velocityY);
            }

            fighter.CancelCheck();
        }
    }

    public override void EndAct(StateController controller)
    {
        Fighter fighter = controller as Fighter;

        if (fighter.hitOtherPlayer)
        {
            if (!fighter.canceledAttack)
            {
                ClearAttackString(fighter);
            }
        }
        else
        {
            ClearAttackString(fighter);
        }

        if (fighter.currentAttackData.attackType == AttackType.SpecialAttack)
        {
            ClearAttackString(fighter);
            fighter.StopSpecialAttackParticles();
        }

        fighter.ExitActiveFrames();
        fighter.hitOtherPlayer = false;
    }

    private static void ClearAttackString(Fighter fighter)
    {
        fighter.ClearAttackQ();
        fighter.currentAttackStringLength = 0;
    }
}