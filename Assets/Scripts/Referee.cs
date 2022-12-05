using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    public GameObject player1GameObject;
    public GameObject player2GameObject;

    private Fighter _player1Fighter;
    private Fighter _player2Fighter;

    private Rigidbody2D _player1RB;
    private Rigidbody2D _player2RB;

    public FightUIManager fightUI;

    public float fightWidth = 20;
    public float playerStartDistanceWidth = 7;

    [SerializeField]
    private float _roundDuration;
    private float _roundTimeRemaining;

    void Awake()
    {
        _player1Fighter = player1GameObject.GetComponent<Fighter>();
        _player2Fighter = player2GameObject.GetComponent<Fighter>();
        _player1RB = player1GameObject.GetComponent<Rigidbody2D>();
        _player2RB = player2GameObject.GetComponent<Rigidbody2D>();
        fightUI.player1 = _player1Fighter;
        fightUI.player2 = _player2Fighter;
        _player1Fighter.otherFighter = _player2Fighter;
        _player2Fighter.otherFighter = _player1Fighter;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetFightStartPositions();
        ResetRoundTime();
    }

    private void ResetRoundTime()
    {
        _roundTimeRemaining = _roundDuration;
    }

    void ResetFight()
    {
        _player1Fighter.Reset();
        _player2Fighter.Reset();
        Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ClampFightBounds();

        if (IsFighterDead(_player1Fighter) && IsFighterDead(_player2Fighter))
        {
            //Draw
            ResetFight();
        }
        else if (IsFighterDead(_player1Fighter))
        {
            //P2 Wins
            Win(_player2Fighter);
        }
        else if (IsFighterDead(_player2Fighter))
        {
            //P1 Wins
            Win(_player1Fighter);
        }
        else if (_roundTimeRemaining <= 0)
        {
            if (_player1Fighter.currentHP == _player2Fighter.currentHP)
            {
                //Draw
                ResetFight();
            }
            else if (_player1Fighter.currentHP < _player2Fighter.currentHP)
            {
                //P2 Wins
                Win(_player2Fighter);
            }
            else if (_player1Fighter.currentHP > _player2Fighter.currentHP)
            {
                //P1 Wins
                Win(_player1Fighter);
            }
        }

        DecrementRoundTime();

        fightUI.UpdateRoundTimeRemainingStatus(_roundTimeRemaining);
    }

    private void Win(Fighter fighter)
    {
        fighter.roundsWon++;
        if (fighter.GetComponent<FighterController>().playerSlot == PlayerSlot.Player1)
        {
            fightUI.player1RoundTickHolder.AddTick();
        }
        else
        {
            fightUI.player2RoundTickHolder.AddTick();
        }
        ResetFight();
    }

    bool IsFighterDead(Fighter fighter)
    {
        if (fighter.currentHP <= 0) return true;
        else return false;
    }

    void DecrementRoundTime()
    {
        _roundTimeRemaining -= Time.deltaTime;
    }

    void SetFightStartPositions()
    {
        //Set Player 1 and Player 2 in their respective starting locations.
        _player1RB.position = new Vector3(playerStartDistanceWidth / -2, 0);
        _player2RB.position = new Vector3(playerStartDistanceWidth / 2, 0);
    }

    //Ensure that the player never leaves the fight bounds.
    void ClampFightBounds()
    {
        _player1RB.position = new Vector3(Mathf.Clamp(_player1RB.position.x, fightWidth / -2, fightWidth / 2), _player1RB.position.y);
        _player2RB.position = new Vector3(Mathf.Clamp(_player2RB.position.x, fightWidth / -2, fightWidth / 2), _player2RB.position.y);
    }
}
