using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

[CreateAssetMenu(menuName = "PluggableSM/Actions/Move")]
public class MoveAction : Action
{
    [SerializeField] private AnimationClip _movingForwardAnimClip;
    [SerializeField] private AnimationClip _movingBackwardAnimClip;
    public override void Act(StateController controller)
    {
        float movementValue = controller.GetComponent<FighterController>().GetMoveValueHorizontal();
        Fighter fighter = controller as Fighter;


        if (fighter.isFacingLeft)
        {
            if (movementValue > 0)
            {
                MoveBackward(controller);
            }
            else if (movementValue < 0)
            {
                MoveForward(controller);
            }
        }
        else
        {
            if (movementValue > 0)
            {
                MoveForward(controller);
            }
            else if (movementValue < 0)
            {
                MoveBackward(controller);
            }
        }
    }

    private void MoveBackward(StateController controller)
    {
        float movementValue = controller.GetComponent<FighterController>().GetMoveValueHorizontal();
        Fighter fighter = controller as Fighter;
        Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();
        AnimancerComponent animancerComponent = controller.GetComponent<AnimancerComponent>();

        animancerComponent.Play(_movingBackwardAnimClip, 0.25f);
        rb.velocity = new Vector3(movementValue * fighter.movementSpeed, rb.velocity.y);
    }
     
    private void MoveForward(StateController controller)
    {
        float movementValue = controller.GetComponent<FighterController>().GetMoveValueHorizontal();
        Fighter fighter = controller as Fighter;
        Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();
        AnimancerComponent animancerComponent = controller.GetComponent<AnimancerComponent>();

        animancerComponent.Play(_movingForwardAnimClip, 0.25f);
        rb.velocity = new Vector3(movementValue * fighter.movementSpeed * fighter.forwardMoveSpeedMultiplier, rb.velocity.y);
    }

    public override void EndAct(StateController controller)
    {

    }

    public override void StartAct(StateController controller)
    {
        Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
