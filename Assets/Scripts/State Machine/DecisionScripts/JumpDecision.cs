using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu (menuName = "PluggableSM/Decisions/Jump")]
public class JumpDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        FighterController fighterController = controller.GetComponent<FighterController>();

        if (fighterController.jumpInputAction.phase == InputActionPhase.Started)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}