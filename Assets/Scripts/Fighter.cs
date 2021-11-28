using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fighter : MonoBehaviour
{
    public Fighter otherFighter;

    public float _movementSpeed;

    public AttackData lightAttackData;
    public AttackData heavyAttackData;
    public AttackData specialAttackData;
    public Queue<AttackData> attackQ;

    public float startingHP;
    public float currentHP;

    public Hitbox hitbox;
    public Hurtbox hurtbox;

    public bool hitOtherPlayer;

    private Animator anim;

    public bool canMove;
    public bool isBlocking;
    public bool isAttacking;

    private FighterController fighterController;

    private Rigidbody rb;

    public int currentAttackStringLength;

    private const int MAX_ATTACK_STRING_LENGTH = 3;

    bool isFacingLeft;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hitbox.triggerEnterEvent.AddListener(SendHit);
        anim = GetComponent<Animator>();
        fighterController = GetComponent<FighterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHP = startingHP;
        attackQ = new Queue<AttackData>(2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(fighterController.GetMoveValueHorizontal());
    }

    private void Update()
    {
        StartAttackString();
        SetFacing();
        Face();
    }

    public void Halt()
    {
        canMove = false;
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    public void SendHit(Collider other)
    {
        if (other.TryGetComponent(out Hurtbox hurtbox))
        {
            if (fighterController.playerSlot != hurtbox.fighterController.playerSlot)
            {
                hitOtherPlayer = true;

                hurtbox.fighter.ReceiveHit(attackQ.Dequeue());

                //Cancel
                if (AttackWaiting() && currentAttackStringLength < MAX_ATTACK_STRING_LENGTH)
                {
                    Attack();
                    currentAttackStringLength++;
                }
            }
        }
    }

    void ReceiveHit(AttackData attackData)
    { 
        if (attackData.attackType == AttackType.LightAttack)
        {
            if (isBlocking)
            {
                anim.SetTrigger("HitWhileBlocking");
                CustomStateLength.animDurationInFrames = attackData.blockstun;
            }
            else
            {
                HurtPlayer(attackData);
            }
        }
        else if (attackData.attackType == AttackType.HeavyAttack)
        {
            HurtPlayer(attackData);
        }
    }

    private void HurtPlayer(AttackData attackData)
    {
        anim.SetTrigger("Hurt");
        CustomStateLength.animDurationInFrames = attackData.hitstun;
        currentHP -= attackData.damage;
    }

    public void LightAttack(InputAction.CallbackContext obj)
    {
        AddToAttackQueue(lightAttackData);
    }

    public void HeavyAttack(InputAction.CallbackContext obj)
    {
        AddToAttackQueue(heavyAttackData);
    }

    private void AddToAttackQueue(AttackData ad)
    {
        if (attackQ.Count < 2) attackQ.Enqueue(ad); 
    }

    public void SpecialAttack()
    {

    }

    void StartAttackString()
    {
        if (!isAttacking && AttackWaiting())
        {
            currentAttackStringLength = 1;
            Attack();
        }
    }

    private bool AttackWaiting()
    {
        return attackQ.Count > 0;
    }

    private void Attack()
    {
        switch (attackQ.Peek().attackType)
        {
            case AttackType.HeavyAttack:
                anim.SetTrigger("Heavy Attack");
                break;
            case AttackType.LightAttack:
                anim.SetTrigger("Light Attack");
                break;
        }
    }

    public void Block(InputAction.CallbackContext obj)
    {
        anim.SetBool("Block", true);
    }

    public void Unblock(InputAction.CallbackContext obj)
    {
        anim.SetBool("Block", false);
    }

    void Face()
    {
        if (isFacingLeft)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    void SetFacing()
    {
        if (transform.position.z < otherFighter.transform.position.z)
        {
            isFacingLeft = false;
        }
        else
        {
            isFacingLeft = true;
        }
    }

    public void Move(float movementInputValue)
    {
        if (canMove)
        {
            //Change the player's velocity.
            rb.velocity = new Vector3(0, rb.velocity.y, movementInputValue * _movementSpeed);
        }

        //Update the player's walk animation.
        if (isFacingLeft)
        {
            anim.SetFloat("Move", movementInputValue * -1);
        }
        else
        {
            anim.SetFloat("Move", movementInputValue);
        }
        anim.SetBool("Moving", movementInputValue != 0);
    }

    public void SetVelocityZ(float z)
    {
        rb.velocity = new Vector3(0, rb.velocity.y, isFacingLeft ? -z : z);
    }

    public void SetVelocityY(float y)
    {
        rb.velocity = new Vector3(0, y, rb.velocity.z);
    }
}
