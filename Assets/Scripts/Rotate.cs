using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        iTween.RotateBy(gameObject, iTween.Hash("y", 1, "speed", rotateSpeed, "loopType", iTween.LoopType.loop, "easeType", iTween.EaseType.linear));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
