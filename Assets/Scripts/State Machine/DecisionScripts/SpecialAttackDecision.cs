using UnityEngine;

[CreateAssetMenu (menuName = "PluggableSM/Decisions/SpecialAttack")]
public class SpecialAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        Fighter fighter = controller.GetComponent<Fighter>();

        if (fighter.AttackWaiting() 
            && fighter.attackQ.Peek().attackType == AttackType.SpecialAttack
            && fighter.specialAttackCooldownRemaining <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}