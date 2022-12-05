using UnityEngine;

[CreateAssetMenu (menuName = "PluggableSM/Decisions/BlockHitReact")]
public class BlockHitReactDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        Fighter fighter = controller as Fighter;
        return fighter.wasHitWhileBlocking;
    }
}