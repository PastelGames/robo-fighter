using UnityEngine;
using Animancer;

[CreateAssetMenu (menuName = "PluggableSM/Actions/HitStop")]
public class HitStopAction : Action
{
    public override void Act(StateController controller)
    {
    //    Fighter fighter = controller as Fighter;
    //    if (fighter.hitstopFramesRemaining > 0) fighter.hitstopFramesRemaining--;
    }

    public override void EndAct(StateController controller)
    {
        AnimancerComponent animancerComponent = controller.GetComponent<AnimancerComponent>();
        AnimancerState state = animancerComponent.States.Current;
        state.IsPlaying = true;
    }

    public override void StartAct(StateController controller)
    {
        AnimancerComponent animancerComponent = controller.GetComponent<AnimancerComponent>();
        AnimancerState state = animancerComponent.States.Current;
        state.IsPlaying = false;
    }
}