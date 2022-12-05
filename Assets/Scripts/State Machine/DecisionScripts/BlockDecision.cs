using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu (menuName = "PluggableSM/Decisions/Block")]
public class BlockDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        FighterController fighterController = controller.GetComponent<FighterController>();

        if (fighterController.blockInputAction.phase == InputActionPhase.Started
            || fighterController.blockInputAction.phase == InputActionPhase.Performed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}