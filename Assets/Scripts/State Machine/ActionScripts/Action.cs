using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public abstract void StartAct(StateController controller);
    public abstract void Act(StateController controller);
    public abstract void EndAct(StateController controller);
}
