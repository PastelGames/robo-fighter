using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{ 
    public GameObject player1;
    public GameObject player2;

    public Transform leftBound;
    public Transform rightBound;

    public static Transform s_leftBound;
    public static Transform s_rightBound;

    [SerializeField]
    private float playerStartOffset;

    // Start is called before the first frame update
    void Start()
    {
        s_leftBound = leftBound;
        s_rightBound = rightBound;

        //Position player 1 at starting location.
        float player1StartPosition = .5f - playerStartOffset;
        PositionPlayerAt(playerStartOffset, player1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PositionPlayerAt(float percentage, GameObject player)
    {
        if (player.TryGetComponent(out Fighter fighter))
        {
            percentage = Mathf.Clamp(percentage, 0, 1);
            fighter.percentageAlongFightArea = percentage;
            player.transform.position = Vector3.Lerp(s_leftBound.position, s_rightBound.position, percentage);
        }
        else
        {
            Debug.LogError("Position Player At did not find a fighter component attached to the player.");
        }
    }
}
