using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FighterController : MonoBehaviour
{
    public AttackData _lightAttackData;
    public AttackData _heavyAttackData;
    public AttackData _specialAttackData;

    public float _movementSpeed;

    public bool canMove;

    private FighterInputActions _fighterInputActions;
    private InputAction _movement;
    private InputAction _lightAttackInputAction;
    private InputAction _heavyAttackInputAction;

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

        _lightAttackInputAction = _fighterInputActions.Player1.LightAttack;
        _lightAttackInputAction.performed += LightAttack;
        _lightAttackInputAction.Enable();

        _heavyAttackInputAction = _fighterInputActions.Player1.HeavyAttack;
        _heavyAttackInputAction.performed += HeavyAttack;
        _heavyAttackInputAction.Enable();
    }

    private void OnDisable()
    {
        _movement.Disable();
        _lightAttackInputAction.Disable();
        _heavyAttackInputAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void TakeDamage(AttackData ad)
    {

    }

    void DealDamage(AttackData ad)
    {

    }

    void Move()
    {
        float movementInputValue;

        if (canMove) movementInputValue = _movement.ReadValue<Vector2>().x;
        else movementInputValue = 0;
        
        //Change the player's velocity.
        Vector3 newVelocity = new Vector3(0, rb.velocity.y, movementInputValue * _movementSpeed);
        rb.velocity = newVelocity;

        //Update the player's walk animation.
        _anim.SetFloat("Move", movementInputValue);
        _anim.SetBool("Moving", movementInputValue != 0);
    }

    void LightAttack(InputAction.CallbackContext obj)
    {
        _anim.SetTrigger("Light Attack");
    }

    void HeavyAttack(InputAction.CallbackContext obj)
    {
        _anim.SetTrigger("Heavy Attack");
    }

    void SpecialAttack()
    {

    }
}
