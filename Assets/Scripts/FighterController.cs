using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FighterController : MonoBehaviour
{
    private FighterInputActions _fighterInputActions;
    private InputAction movementInputAction;
    private InputAction _lightAttackInputAction;
    private InputAction _heavyAttackInputAction;
    private InputAction _specialAttackInputAction;
    private InputAction _jumpInputAction;
    [HideInInspector] public InputAction blockInputAction;
    private InputActionMap _playerActionMap;

    public PlayerSlot playerSlot;

    private Fighter _fighter;

    private void Awake()
    {
        _fighterInputActions = new FighterInputActions();
        _fighter = GetComponent<Fighter>();

        //Set the controls relative to which player they are.
        if (playerSlot == PlayerSlot.Player1)
        {
            _playerActionMap = _fighterInputActions.Player1;
        }
        else if (playerSlot == PlayerSlot.Player2)
        {
            _playerActionMap = _fighterInputActions.Player2;
        }
    }

    private void OnEnable()
    {
        movementInputAction = _playerActionMap.actions[0];
        movementInputAction.Enable();

        _lightAttackInputAction = _playerActionMap.actions[1];
        _lightAttackInputAction.started += _fighter.LightAttack;
        _lightAttackInputAction.Enable();

        _heavyAttackInputAction = _playerActionMap.actions[2];
        _heavyAttackInputAction.started += _fighter.HeavyAttack;
        _heavyAttackInputAction.Enable();

        blockInputAction = _playerActionMap.actions[3];
        blockInputAction.Enable();

        _specialAttackInputAction = _playerActionMap.actions[4];
        _specialAttackInputAction.started += _fighter.SpecialAttack;
        _specialAttackInputAction.Enable();

        _jumpInputAction = _playerActionMap.actions[5];
        _jumpInputAction.started += _fighter.Jump;
        _jumpInputAction.Enable();
    }

    private void OnDisable()
    {
        movementInputAction.Disable();
        _lightAttackInputAction.Disable();
        _heavyAttackInputAction.Disable();
        blockInputAction.Disable();
    }

    public float GetMoveValueHorizontal()
    {
        return movementInputAction.ReadValue<Vector2>().x;
    }
}
