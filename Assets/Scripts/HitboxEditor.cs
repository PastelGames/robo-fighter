using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxEditor : MonoBehaviour
{
    [SerializeField] private Collider2D _hitboxCollider2D;
    [SerializeField] private Collider2D _hurtboxCollider2D;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_hitboxCollider2D.bounds.center, _hitboxCollider2D.bounds.size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_hurtboxCollider2D.bounds.center, _hurtboxCollider2D.bounds.size);
    }
}
