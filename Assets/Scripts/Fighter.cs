using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fighter : MonoBehaviour
{
    public AttackData lightAttackData;
    public AttackData heavyAttackData;
    public AttackData specialAttackData;

    public float movementSpeed;

    private FighterInputActions fighterInputActions;
    private InputAction movement;

    [Range(0, 1)]
    public float percentageAlongFightArea;

    private void Awake()
    {
        fighterInputActions = new FighterInputActions();
    }

    private void OnEnable()
    {
        movement = fighterInputActions.Player1.Move;
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(movement);
    }

    void TakeDamage(AttackData ad)
    {

    }

    void DealDamage(AttackData ad)
    {

    }

    void Move(InputAction movement)
    {
        //Change the players position.
        float offset = movement.ReadValue<Vector2>().x * movementSpeed;
        float newPercentage = offset + percentageAlongFightArea;
        PositionManager.PositionPlayerAt(newPercentage, this.gameObject);

        //Update the player's walk animation.
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
