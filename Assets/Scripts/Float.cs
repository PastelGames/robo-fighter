using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public float magnitude;

    // Start is called before the first frame update
    void Start()
    {
        iTween.MoveBy(gameObject, iTween.Hash("y", magnitude, "loopType", "pingPong", "easeType", iTween.EaseType.easeInOutSine));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
