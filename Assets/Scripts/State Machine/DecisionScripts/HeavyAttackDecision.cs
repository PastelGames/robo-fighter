using UnityEngine;

[CreateAssetMenu (menuName = "PluggableSM/Decisions/HeavyAttack")]
public class HeavyAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        Fighter fighter = controller.GetComponent<Fighter>();

        if (fighter.AttackWaiting() && fighter.attackQ.Peek().attackType == AttackType.HeavyAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}