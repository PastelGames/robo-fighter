using UnityEngine;

[CreateAssetMenu (menuName = "PluggableSM/Decisions/HitStop")]
public class HitStopDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        Fighter fighter = controller as Fighter;
        //if (fighter.hitstopFramesRemaining > 0) return true;
        //else return false;
        return true;
    }
}