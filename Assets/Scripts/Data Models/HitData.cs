using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HitData", menuName = "ScriptableObjects/HitData", order = 1)]
public class HitData : ScriptableObject
{
    public string attackName;
    public float pushBack;
    public int hitStop;
    public int blockstun;
    public int damage;
}
