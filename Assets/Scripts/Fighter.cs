using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fighter : MonoBehaviour
{
    public AttackData _lightAttackData;
    public AttackData _heavyAttackData;
    public AttackData _specialAttackData;

    public float _movementSpeed;

    private FighterInputActions _fighterInputActions;
    private InputAction _movement;

    public Animator _anim;

    public Rigidbody rb;

    private void Awake()
    {
        _fighterInputActions = new FighterInputActions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _movement = _fighterInputActions.Player1.Move;
        _movement.Enable();
    }

    private void OnDisable()
    {
        _movement.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(_movement);
    }

    void TakeDamage(AttackData ad)
    {

    }

    void DealDamage(AttackData ad)
    {

    }

    void Move(InputAction movement)
    {
        float movementInputValue = movement.ReadValue<Vector2>().x;

        //Change the player's velocity.
        Vector3 newVelocity = new Vector3(0, rb.velocity.y, movementInputValue * _movementSpeed);
        rb.velocity = newVelocity;

        //Update the player's walk animation.
        _anim.SetFloat("Move", movementInputValue);
        _anim.SetBool("Moving", movementInputValue != 0);
    }

    void LightAttack()
    {

    }

    void HeavyAttack()
    {

    }

    void SpecialAttack()
    {

    }
}
