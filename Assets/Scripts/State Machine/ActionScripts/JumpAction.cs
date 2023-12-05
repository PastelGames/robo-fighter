using UnityEngine;
using Animancer;

[CreateAssetMenu (menuName = "PluggableSM/Actions/Jump")]
public class JumpAction : Action
{
    [SerializeField] private float _jumpInitialUpVelocity = 5;
    [SerializeField] private AnimationClip _jumpAnimationClip;
    private float _horizontalVelocity;
    private Rigidbody2D _rb;
    private Fighter _fighter;

    public override void Act(StateController controller)
    {
        _rb.velocity = new Vector2(_horizontalVelocity, _rb.velocity.y);
        _fighter = controller as Fighter;
        if (_fighter.isGrounded) controller.TransitionToState(controller.previousState);
    }

    public override void EndAct(StateController controller)
    {
        _fighter.SetFacing();
    }

    public override void StartAct(StateController controller)
    {
        _rb = controller.GetComponent<Rigidbody2D>();
        FighterController fighterController = controller.GetComponent<FighterController>();
        if (fighterController.GetMoveValueHorizontal() == 0)
        {
            _horizontalVelocity = 0;
        }
        else
        {
            _horizontalVelocity = _rb.velocity.x;
        }
        _rb.velocity = new Vector2(_horizontalVelocity, _jumpInitialUpVelocity);
        AnimancerComponent ac = controller.GetComponent<AnimancerComponent>();
        ac.Play(_jumpAnimationClip).Time = 0;
        _fighter = controller as Fighter;
        _fighter.isGrounded = false;
    }
}