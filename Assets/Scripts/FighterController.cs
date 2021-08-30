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
    private InputAction _blockInputAction;
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
        _lightAttackInputAction.performed += _fighter.LightAttack;
        _lightAttackInputAction.Enable();

        _heavyAttackInputAction = _playerActionMap.actions[2];
        _heavyAttackInputAction.performed += _fighter.HeavyAttack;
        _heavyAttackInputAction.Enable();

        _blockInputAction = _playerActionMap.actions[3];
        _blockInputAction.performed += _fighter.Block;
        _blockInputAction.canceled += _fighter.Unblock;
        _blockInputAction.Enable();
    }

    private void OnDisable()
    {
        movementInputAction.Disable();
        _lightAttackInputAction.Disable();
        _heavyAttackInputAction.Disable();
        _blockInputAction.Disable();
    }

    public float GetMoveValueHorizontal()
    {
        return movementInputAction.ReadValue<Vector2>().x;
    }
}
