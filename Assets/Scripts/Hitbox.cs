using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hitbox : MonoBehaviour
{
    public UnityEvent<Collider2D> triggerEnterEvent;

    void OnTriggerEnter2D(Collider2D other)
    {
        triggerEnterEvent.Invoke(other);
    }
}
