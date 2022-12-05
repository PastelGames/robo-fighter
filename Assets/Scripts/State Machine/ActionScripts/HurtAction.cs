using UnityEngine;
using Animancer;

[CreateAssetMenu (menuName = "PluggableSM/Actions/Hurt")]
public class HurtAction : Action
{
    [SerializeField] protected AnimationClip _lightHurtAnimClip;
    [SerializeField] protected AnimationClip _heavyHurtAnimClip;

    public override void StartAct(StateController controller)
    {
        Fighter fighter = controller as Fighter;
        AnimancerComponent animancer = controller.GetComponent<AnimancerComponent>();

        fighter.wasHurt = false;
        AttackType recievedAttackType = fighter.currentHurtAttackData.attackType;

        AnimancerState state;
        if (recievedAttackType == AttackType.HeavyAttack)
        {
            state = animancer.Play(_heavyHurtAnimClip);
        }
        else 
        {
            state = animancer.Play(_lightHurtAnimClip);
        }

        state.Time = 0;
        state.Events.OnEnd += () =>
        {
            controller.TransitionToState(fighter.idleState);
        };
    }

    public override void Act(StateController controller)
    {
        
    }

    public override void EndAct(StateController controller)
    {
        Fighter fighter = controller as Fighter;
        fighter.ClearAttackQ();
    }

}