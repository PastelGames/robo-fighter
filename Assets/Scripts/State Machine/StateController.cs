using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State currentState;
    public State previousState;

    protected virtual void Awake()
    {
        currentState.OnStateEnter(this);
    }

    protected virtual void FixedUpdate()
    {
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState == currentState)
        {
            currentState.OnStateExit(this);
            previousState = currentState;
            currentState.OnStateEnter(this);
        }
        else if (nextState != null)
        {
            currentState.OnStateExit(this);
            previousState = currentState;
            currentState = nextState;
            currentState.OnStateEnter(this);
        }
    }
}