using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTickHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject _roundTick;

    public void AddTick()
    {
        Instantiate(_roundTick, transform);
    }
}
