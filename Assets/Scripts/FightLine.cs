using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightLine : MonoBehaviour
{
    public Transform fightLeftBound;
    public Transform fightRightBound;

    public Color color;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawLine(fightLeftBound.position, fightRightBound.position);
    }
}
