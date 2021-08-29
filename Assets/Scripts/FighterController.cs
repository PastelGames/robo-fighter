using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FighterController : MonoBehaviour
{
    public float _movementSpeed;

    public bool canMove;

    private FighterInputActions _fighterInputActions;
    private InputAction _movement;
    private InputAction _lightAttackInputAction;
    private InputAction _heavyAttackInputAction;
    private InputActionMap _playerActionMap;

    private Animator _anim;

    private Rigidbody _rb;

    public PlayerSlot _playerSlot;

    private Fighter _fighter;

    private void Awake()
    {
        _fighterInputActions = new FighterInputActions();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
        _fighter = GetComponent<Fighter>();

        //Set the controls relative to which player they are.
        if (_playerSlot == PlayerSlot.Player1)
        {
            _playerActionMap = _fighterInputActions.Player1;
        }
        else if (_playerSlot == PlayerSlot.Player2)
        {
            _playerActionMap = _fighterInputActions.Player2;
        }
    }

    private void OnEnable()
    {
        _movement = _playerActionMap.actions[0];
        _movement.Enable();

        _lightAttackInputAction = _playerActionMap.actions[1];
        _lightAttackInputAction.performed += LightAttack;
        _lightAttackInputAction.Enable();

        _heavyAttackInputAction = _playerActionMap.actions[2];
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

    void Move()
    {
        float movementInputValue;

        if (canMove) movementInputValue = _movement.ReadValue<Vector2>().x;
        else movementInputValue = 0;
        
        //Change the player's velocity.
        Vector3 newVelocity = new Vector3(0, _rb.velocity.y, movementInputValue * _movementSpeed);
        _rb.velocity = newVelocity;

        //Update the player's walk animation.
        _anim.SetFloat("Move", movementInputValue);
        _anim.SetBool("Moving", movementInputValue != 0);
    }

    void LightAttack(InputAction.CallbackContext obj)
    {
        _anim.SetTrigger("Light Attack");
        _fighter._currentAttackData = _fighter._lightAttackData;
    }

    void HeavyAttack(InputAction.CallbackContext obj)
    {
        _anim.SetTrigger("Heavy Attack");
    }

    void SpecialAttack()
    {

    }
}
