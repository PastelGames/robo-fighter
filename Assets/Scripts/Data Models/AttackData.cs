using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/AttackData", order = 1)]
public class AttackData : ScriptableObject
{
    public string attackName;
    public int hitstun;
    public int blockstun;
    public float damage;
    public AttackType attackType;
}
