using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Animancer;

public class Fighter : StateController
{
    public Fighter otherFighter;

    public float movementSpeed;
    public float forwardMoveSpeedMultiplier;

    public AttackData lightAttackData;
    public AttackData heavyAttackData;
    public AttackData specialAttackData;
    public Queue<AttackData> attackQ;

    public int startingHP;
    public int currentHP;

    public TMP_Text takqtext; //DEBUG

    protected Hitbox hitbox;
    protected Hurtbox hurtbox;

    private FighterController fighterController;

    public int currentAttackStringLength = 0;

    public int maxAttackStringLength = 3;

    [HideInInspector] public bool newIsFacingLeft;
    [HideInInspector] public bool isFacingLeft;

    public HitData currentHitData;
    public AttackData currentAttackData;

    public AttackData currentHurtAttackData;

    public Queue<AnimationClip> animationQ;
    public AnimationClip currentAnimation;

    public State lightAttackState;
    public State heavyAttackState;
    public State specialAttackState;
    public State idleState;
    public State blockingState;
    public State blockHitReactState;
    public State hurtState;

    public bool hitOtherPlayer;

    public bool canceledAttack;

    public bool wasHurt;

    public bool wasHitWhileBlocking;

    public bool isHitStopped;

    public float specialAttackCooldownRemaining = 0;

    public float specialAttackCooldownDuration;

    public GameObject hurtParticles;
    public GameObject heavyHurtParticles;
    public GameObject hurtParticleOrigin;

    public GameObject specialAttackParticles;

    [HideInInspector]
    public int roundsWon = 0;

    private FightCamera _fightCamera;

    public int comboHitCount;

    [SerializeField] private AnimationClip _idleAnimationClip;

    protected override void Awake()
    {
        base.Awake();
        hitbox = GetComponentInChildren<Hitbox>(true);
        hurtbox = GetComponentInChildren<Hurtbox>(true);

        hitbox.triggerEnterEvent.AddListener(SendHit);
        fighterController = GetComponent<FighterController>();
        currentHP = startingHP;
        attackQ = new Queue<AttackData>();
        _fightCamera = FindObjectOfType<FightCamera>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        string takqstring = "Attack Q: \n";
        foreach (AttackData attackData in attackQ)
        {
            takqstring += attackData.name + "\n";
        }
        takqtext.text = takqstring;
        SetFacing();
        Face();
        DecrementCooldown();
    }

    public void BeginSpecialAttackCooldown()
    {
        specialAttackCooldownRemaining = specialAttackCooldownDuration;
    }

    public void PlaySpecialAttackParticles()
    {
        ParticleSystem[] specialAttackParticleSystems
                = specialAttackParticles.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in specialAttackParticleSystems)
        {
            ps.Play();
        }
    }
    public void StopSpecialAttackParticles()
    {
        ParticleSystem[] specialAttackParticleSystems
                = specialAttackParticles.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in specialAttackParticleSystems)
        {
            ps.Stop();
        }
    }

    private void DecrementCooldown()
    {
        if (specialAttackCooldownRemaining > 0)
        {
            specialAttackCooldownRemaining -= Time.deltaTime;
        }
    }

    public void SendHit(Collider2D other)
    {
        if (!hitOtherPlayer 
            && other.TryGetComponent(out Hurtbox otherPlayerHurtbox)
            && fighterController.playerSlot != otherPlayerHurtbox.fighterController.playerSlot)
        {
            hitOtherPlayer = true;
            otherPlayerHurtbox.fighter.ReceiveHit(currentHitData, currentAttackData);
            hitbox.gameObject.SetActive(false);
            StartCoroutine(HitStopCoroutine(currentHitData.hitStop));
        }
    }

    IEnumerator HitStopCoroutine(int hitStopFrames)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        AnimancerComponent animancerComponent = GetComponent<AnimancerComponent>();
        animancerComponent.Playable.Speed = 0;
        isHitStopped = true;
        while (hitStopFrames > 0)
        {
            hitStopFrames--;
            yield return new WaitForFixedUpdate();
        }
        isHitStopped = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        animancerComponent.Playable.Speed = 1;
    }

    IEnumerator HitStopCoroutine(int hitStopFrames, float pushBack)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        AnimancerComponent animancerComponent = GetComponent<AnimancerComponent>();
        animancerComponent.Playable.Speed = 0;
        isHitStopped = true;
        while (hitStopFrames > 0)
        {
            hitStopFrames--;
            yield return new WaitForFixedUpdate();
        }
        isHitStopped = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        animancerComponent.Playable.Speed = 1;
        Pushback(pushBack);
    }

    void ReceiveHit(HitData hitData, AttackData attackData)
    {
        currentHurtAttackData = attackData;
        if (attackData.attackType == AttackType.LightAttack
            || attackData.attackType == AttackType.SpecialAttack)
        {
            if (currentState == blockingState
                || currentState == blockHitReactState)
            {
                OnHitWhileBlocking();
                StartCoroutine(HitStopCoroutine(hitData.hitStop / 3, hitData.pushBack));

            }
            else
            {
                ShowHurtParticles();
                HurtPlayer(hitData);
                StartCoroutine(HitStopCoroutine(hitData.hitStop, hitData.pushBack));
            }
        }
        else if (attackData.attackType == AttackType.HeavyAttack)
        {
            ShowHeavyHurtParticles();
            StartCoroutine(_fightCamera.Shake(hitData.hitStop, .05f));
            HurtPlayer(hitData);
            StartCoroutine(HitStopCoroutine(hitData.hitStop, hitData.pushBack));
        }
    }

    private void Pushback(float pushBack)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 force;
        if (isFacingLeft)
        {
            force = new Vector2(pushBack, 0);
        }
        else
        {
            force = new Vector2(-pushBack, 0);
        }
        rb.AddForce(force);
    }

    private void OnHitWhileBlocking()
    {
        wasHitWhileBlocking = true;
    }

    public void ShowHeavyHurtParticles()
    {
        Instantiate(heavyHurtParticles, hurtParticleOrigin.transform);
    }

    public void ShowHurtParticles()
    {
        Instantiate(hurtParticles, hurtParticleOrigin.transform);
    }

    public void Reset()
    {
        StopAllCoroutines();
        currentHP = startingHP;
        specialAttackCooldownRemaining = 0;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        AnimancerComponent animancerComponent = GetComponent<AnimancerComponent>();
        animancerComponent.Play(_idleAnimationClip, 0);
        rb.bodyType = RigidbodyType2D.Dynamic;
        animancerComponent.Playable.Speed = 1;
        rb.velocity = Vector2.zero;
        TransitionToState(idleState);
        ClearAttackQ();
        ExitActiveFrames();
    }

    private void HurtPlayer(HitData hitData)
    {
        comboHitCount++;
        currentHP = Mathf.Clamp(currentHP - hitData.damage, 0, startingHP);
        wasHurt = true;
    }

    public void LightAttack(InputAction.CallbackContext obj)
    {
        AddToAttackQueue(lightAttackData);
    }

    public void HeavyAttack(InputAction.CallbackContext obj)
    {
        AddToAttackQueue(heavyAttackData);
    }

    public void ClearAttackQ()
    {
        attackQ.Clear();
    }

    public void CancelCheck()
    {
        if (AttackWaiting()
            && currentAttackData.attackType != AttackType.SpecialAttack
            && hitOtherPlayer
            && currentAttackStringLength < maxAttackStringLength 
            && !canceledAttack
            && !isHitStopped)
        {
            canceledAttack = true;
            
            if (attackQ.Peek().attackType == AttackType.LightAttack)
            {
                TransitionToState(lightAttackState);
            }
            else if (attackQ.Peek().attackType == AttackType.HeavyAttack)
            {
                TransitionToState(heavyAttackState);
            }
            else if (attackQ.Peek().attackType == AttackType.SpecialAttack)
            {
                TransitionToState(specialAttackState);
            }
        }
    }

    public void BeginActiveFrames(HitData hitData)
    {
        hitbox.gameObject.SetActive(true);
        currentHitData = hitData;
        canceledAttack = false;
        hitOtherPlayer = false;
    }

    public void ExitActiveFrames()
    {
        hitbox.gameObject.SetActive(false);
        currentHitData = null;
        hitOtherPlayer = false;
    }

    private void AddToAttackQueue(AttackData ad)
    {
        if (attackQ.Count < 2) attackQ.Enqueue(ad); 
    }

    public void SpecialAttack(InputAction.CallbackContext obj)
    {
        if (specialAttackCooldownRemaining <= 0)
        {
            AddToAttackQueue(specialAttackData);
        }
    }

    public bool AttackWaiting()
    {
        return attackQ.Count > 0;
    }

    void Face()
    {
        if (newIsFacingLeft != isFacingLeft)
        {
            if (newIsFacingLeft)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
        isFacingLeft = newIsFacingLeft;
    }

    void SetFacing()
    {
        if (transform.position.x < otherFighter.transform.position.x)
        {
            newIsFacingLeft = false;
        }
        else
        {
            newIsFacingLeft = true;
        }
    }
}
