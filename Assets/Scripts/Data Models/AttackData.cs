using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/AttackData", order = 1)]
public class AttackData : ScriptableObject
{
    public AttackType attackType;
    public List<AnimationClip> clips;
    public AnimationCurve velocityX;
    public AnimationCurve velocityY;
}