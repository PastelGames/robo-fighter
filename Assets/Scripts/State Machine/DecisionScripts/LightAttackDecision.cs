using UnityEngine;
using Animancer;

[CreateAssetMenu (menuName = "PluggableSM/Decisions/LightAttack")]
public class LightAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        Fighter fighter = controller as Fighter;

        if (fighter.AttackWaiting() 
            && fighter.attackQ.Peek().attackType == AttackType.LightAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}