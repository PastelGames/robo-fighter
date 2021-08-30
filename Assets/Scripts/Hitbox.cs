using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hitbox : MonoBehaviour
{
    public UnityEvent<Collider> triggerEnterEvent;

    void OnTriggerEnter(Collider other)
    {
        triggerEnterEvent.Invoke(other);
    }
}
