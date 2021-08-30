using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fighter : MonoBehaviour
{
    public float _movementSpeed;

    public AttackData lightAttackData;
    public AttackData heavyAttackData;
    public AttackData specialAttackData;
    private AttackData currentAttackData;

    public int startingHP;
    private int _currentHP;

    public Hitbox hitbox;
    public Hurtbox hurtbox;

    private Animator anim;

    public bool canMove;
    public bool isBlocking;

    private FighterController fighterController;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hitbox.triggerEnterEvent.AddListener(SendHit);
        anim = GetComponentInChildren<Animator>();
        fighterController = GetComponent<FighterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHP = startingHP;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(fighterController.GetMoveValueHorizontal());
    }

    public void SendHit(Collider other)
    {
        if (other.TryGetComponent(out Hurtbox hurtbox))
        {
            if (fighterController.playerSlot != hurtbox.fighterController.playerSlot)
            {
                hurtbox.fighter.ReceiveHit(currentAttackData);
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
                anim.GetBehaviour<BlockstunAnimState>().stunDurationInFrames = attackData.blockstun;
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

        //TODO take damage.
    }

    private void HurtPlayer(AttackData attackData)
    {
        anim.SetTrigger("Hurt");
        anim.GetBehaviour<HurtAnimState>().stunDurationInFrames = attackData.hitstun;
    }

    public void LightAttack(InputAction.CallbackContext obj)
    {
        anim.SetTrigger("Light Attack");
        currentAttackData = lightAttackData;
    }

    public void HeavyAttack(InputAction.CallbackContext obj)
    {
        anim.SetTrigger("Heavy Attack");
        currentAttackData = heavyAttackData;
    }

    public void SpecialAttack()
    {

    }

    public void Block(InputAction.CallbackContext obj)
    {
        anim.SetBool("Block", true);
    }

    public void Unblock(InputAction.CallbackContext obj)
    {
        anim.SetBool("Block", false);
    }

    public void Move(float movementInputValue)
    {

        if (!canMove) movementInputValue = 0;

        //Change the player's velocity.
        Vector3 newVelocity = new Vector3(0, rb.velocity.y, movementInputValue * _movementSpeed);
        rb.velocity = newVelocity;

        //Update the player's walk animation.
        anim.SetFloat("Move", movementInputValue);
        anim.SetBool("Moving", movementInputValue != 0);
    }
}
