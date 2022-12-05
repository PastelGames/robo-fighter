using UnityEngine;
using Animancer;

[CreateAssetMenu (menuName = "PluggableSM/Decisions/Idle")]
public class IdleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        AnimancerComponent animancer = controller.GetComponent<AnimancerComponent>();

        if (animancer.States.TryGet(controller, out AnimancerState state))
        {
            if (state.NormalizedTime >= 1)
            {
                return true;
            }
            else return false;
        }
        else
        {
            return false;
        }
    }
}