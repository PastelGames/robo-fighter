using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Decisions/Move")]
public class MoveDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        float moveValue = controller.GetComponent<FighterController>().GetMoveValueHorizontal();

        if (moveValue != 0) return true;
        else return false;
    }
}
 