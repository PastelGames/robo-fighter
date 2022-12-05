using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (menuName = "PluggableSM/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        foreach (Action action in actions)
        {
            action.Act(controller);
        }
    }
    
    private void CheckTransitions(StateController controller)
    {
        foreach(Transition transition in transitions)
        {
            bool decisionSucceeded = transition.decision.Decide(controller);

            if (decisionSucceeded)
            {
                controller.TransitionToState(transition.trueState);
            }
            else controller.TransitionToState(transition.falseState);
        }
    }

    public void OnStateEnter(StateController controller)
    {
        foreach(Action action in actions)
        {
            action.StartAct(controller);
        }
    }

    public void OnStateExit(StateController controller)
    {
        foreach (Action action in actions)
        {
            action.EndAct(controller);
        }
    }
}
