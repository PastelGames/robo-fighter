using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/AttackData", order = 1)]
public class AttackData : ScriptableObject
{
    public string attackName;
    public float hitstun;
    public float blockstun;
    public float Damage;
    public AttackType attackType;
}
