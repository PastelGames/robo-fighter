using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    public GameObject player1GameObject;
    public GameObject player2GameObject;

    private Fighter player1Fighter;
    private Fighter player2Fighter;

    private Rigidbody player1RB;
    private Rigidbody player2RB;

    public FightUIManager fightUI;

    public float fightWidth = 20;
    public float playerStartWidth = 7;

    void Awake()
    {
        player1Fighter = player1GameObject.GetComponent<Fighter>();
        player2Fighter = player2GameObject.GetComponent<Fighter>();
        player1RB = player1GameObject.GetComponent<Rigidbody>();
        player2RB = player2GameObject.GetComponent<Rigidbody>();
        fightUI.player1 = player1Fighter;
        fightUI.player2 = player2Fighter;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetFightStartPositions();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ClampFightBounds();
    }

    void SetFightStartPositions()
    {
        //Set Player 1 and Player 2 in their respective starting locations.
        player1RB.position = new Vector3(0, 0, playerStartWidth / -2);
        player2RB.position = new Vector3(0, 0, playerStartWidth / 2);
    }

    //Ensure that the player never leaves the fight bounds.
    void ClampFightBounds()
    {
        player1RB.position = new Vector3(0, player1RB.position.y, Mathf.Clamp(player1RB.position.z, fightWidth / -2, fightWidth / 2));
        player2RB.position = new Vector3(0, player2RB.position.y, Mathf.Clamp(player2RB.position.z, fightWidth / -2, fightWidth / 2));
    }
}
