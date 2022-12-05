using UnityEngine;
using Animancer;

[CreateAssetMenu (menuName = "PluggableSM/Actions/Block")]
public class BlockAction : Action
{
    [SerializeField] AnimationClip animClip;
     
    public override void Act(StateController controller)
    {
        
    }

    public override void EndAct(StateController controller)
    {
        Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void StartAct(StateController controller)
    {
        AnimancerComponent animancer = controller.GetComponent<AnimancerComponent>();
        Rigidbody2D rb = controller.GetComponent<Rigidbody2D>();

        animancer.Play(animClip, .1f);
        rb.bodyType = RigidbodyType2D.Static;
    }
}