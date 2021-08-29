using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    public AttackData _lightAttackData;
    public AttackData _heavyAttackData;
    public AttackData _specialAttackData;
    public AttackData _currentAttackData;

    public int _startingHP;
    private int _currentHP;

    public Hitbox _hitbox;
    public Hurtbox _hurtbox;

    private Animator _anim;

    private FighterController _fighter;

    public AnimationClip animClip;

    private void Awake()
    {
        _hitbox.triggerEnterEvent.AddListener(Hit);
        _anim = GetComponentInChildren<Animator>();
        _fighter = GetComponent<FighterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHP = _startingHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Hit(Collider other)
    {
        if (other.TryGetComponent(out Hurtbox hurtbox))
        {
            if (_fighter._playerSlot != hurtbox.fighterController._playerSlot)
            {
                hurtbox.fighter.Hurt(_currentAttackData);
            }
        }
    }

    void Hurt(AttackData attackData)
    {
        _anim.SetTrigger("Hurt");
        _anim.GetBehaviour<HurtAnimState>()._stunDurationInFrames = attackData.hitstun;
    }
}
