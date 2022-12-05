using UnityEngine;
using Animancer;

[CreateAssetMenu (menuName = "PluggableSM/Actions/BlockHitReact")]
public class BlockHitReactAction : Action
{
    [SerializeField] private AnimationClip _blockHitReactAnimationClip;
    [SerializeField] private State _blockState;
    public override void Act(StateController controller)
    {
        
    }

    public override void EndAct(StateController controller)
    {

    }

    public override void StartAct(StateController controller)
    {
        AnimancerState state = controller.GetComponent<AnimancerComponent>().Play(_blockHitReactAnimationClip);
        state.Time = 0;
        Fighter fighter = controller as Fighter;
        fighter.wasHitWhileBlocking = false;
        state.Events.OnEnd = () =>
        {
            controller.TransitionToState(_blockState);
        };
    }
}