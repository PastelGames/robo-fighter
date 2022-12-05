using UnityEngine;

[CreateAssetMenu (menuName = "PluggableSM/Decisions/Hurt")]
public class HurtDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        Fighter fighter = controller as Fighter;

        return fighter.wasHurt;
    }
}