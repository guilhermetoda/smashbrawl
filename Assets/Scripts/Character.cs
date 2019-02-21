using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveVelocity = 5f;   // how fast the character moves
    [SerializeField] private float _acceleration = 10f;  // how fast the character accelerates
    [SerializeField] private float _jumpVelocity = 7.5f; // how fast the character jumps
    [SerializeField] private float _airControl = 0.1f;   // how much the character can move in the air

    [Header("Grounding")]
    [SerializeField] private float _groundCheckDistance = 0.25f; // how far down we look for the ground
    [SerializeField] private float _groundCheckOffset = 0.25f;   // how far to the side we look for the ground
    [SerializeField] private LayerMask _groundMask;              // which layers are considered ground

    //Attacking Parameters
    [Header("Attacking")]
    [SerializeField] private float _attackingCooldown = 0.3f;

    [Header("Hit")]
    [SerializeField] private float _hitCooldown = 1f;

    private Rigidbody2D _rigidbody;    // the attached rigidbody
    private Animator _animator;        // the attached animator
    private SpriteRenderer _renderer;  // the attached sprite renderer

    private float _moveInput;          // horizontal move input
    private bool _isGrounded;          // is the character on the ground?
    private bool _isTryingToMove;      // is the character trying to move?

    private bool _isAttacking;         // is the character attacking?
    private bool _isDead; // boolean to check if the character is dying (used in animation)
    private bool _isHit; // is the character being hit?
    private bool _isDamageTaken; // Check if the damage is already taken in the enemy

    private float _hitTimer; // Time of getting hit
    private float _attackingTimer; // Duration of the attack

    private void Awake()
    {
        // get attached components
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        // When the Character Awakes setting the attack trigger volume to false to avoid damage without attack
        //_attackTriggerVolume.enabled = false;

        _isDead = false;
        _isHit = false;
        _isAttacking = false;
    }

    // set the movement input value
    protected void ApplyMoveInput(float input)
    {
        _moveInput = input;
        _isTryingToMove = Mathf.Abs(_moveInput) >= 0.05f;
    }

    // attempt a jump
    protected void TryJump()
    {
        if(_isGrounded)
        {
            // set vertical velocity to _jumpVelocity
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpVelocity);
        }
    }

    protected virtual void Update()
    {
        _isGrounded = GetGroundedLeft() || GetGroundedRight();

        // set animator values
        _animator.SetBool("Grounded", _isGrounded);
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x) / _moveVelocity);

        //Set Attacking Value
        _animator.SetBool("Attacking", _isAttacking);
        //_animator.SetBool("Hit", _isHit);


        //Hit Countdown
        if (_isHit)
        {
            if (_hitTimer <= 0)
            {
                _isHit = false;
            }
            else
            {
                _hitTimer -= Time.deltaTime;
            }
        }

        // If the player is still attacking
        if (_isAttacking)
        {
            // The attack stops when the attackingTimer reaches 0
            if (_attackingTimer <= 0f)
            {
                StopAttack();
            }
            else
            {
                // decreasing the attacking timer at every frame
                _attackingTimer -= Time.deltaTime;
            }
        }

        // flip the sprite if character is grounded and has move input
        if (_isGrounded && _isTryingToMove)
        {
            _renderer.flipX = _moveInput <= 0f;
            //_attackTriggerVolume.transform.localPosition = new Vector2(-_attackTriggerVolume.transform.localPosition.x, _attackTriggerVolume.transform.localPosition.y);
        }
    }

    private void FixedUpdate()
    {
        if(_isGrounded)
        {
            // accelerate based on movement input
            AccelerateToVelocity(_moveInput);
        }
        else if(_isTryingToMove)
        {
            // accelerate using air control
            AccelerateToVelocity(_moveInput, _airControl);
        }
    }

    private void AccelerateToVelocity(float input, float control = 1f)
    {
        // get target velocity
        float targetVelocity = input * _moveVelocity;
        // get velocity difference
        float velocityDifference = targetVelocity - _rigidbody.velocity.x;
        // calculate acceleration force
        float force = velocityDifference * _acceleration * control;
        // add acceleration
        _rigidbody.AddForce(new Vector2(force, 0f));
    }

    // checks for the ground
    private bool GroundCheck(float offset)
    {
        // origin of the raycast
        Vector2 origin = (Vector2)transform.position + Vector2.right * offset;
        // perform raycast
        if(Physics2D.Raycast(origin, Vector2.down, _groundCheckDistance, _groundMask))
        {
            // debug raycast hit
            Debug.DrawRay(origin, Vector2.down * _groundCheckDistance, Color.green);
            // return success
            return true;
        }

        // raycast failed
        // debug raycast miss
        Debug.DrawRay(origin, Vector2.down * _groundCheckDistance, Color.red);
        // return failure
        return false;
    }

    protected bool GetGroundedLeft()
    {
        return GroundCheck(-_groundCheckOffset);
    }

    protected bool GetGroundedRight()
    {
        return GroundCheck(_groundCheckOffset);
    }

    protected void StartAttack()
    {
        _isAttacking = true;
        _attackingTimer = _attackingCooldown;
        
    }

    protected void StopAttack()
    {
        _isAttacking = false;
        
    }

    // Function to get if the character is attacking
    protected bool GetIsAttacking()
    {
        return _isAttacking;
    }

    public void SetHit()
    {
        _isHit = true;
        _hitTimer = _hitCooldown;
    }

    // Character has died
    public virtual void KillCharacter()
    {
        _isDead = true;
        _animator.SetBool("Dead", _isDead);
    }

    // Get and sets for isDead and iSDamagetaken, this function are used on the Player class
    protected bool GetIsDead()
    {
        return _isDead;
    }

    protected bool GetIsHit()
    {
        return _isHit;
    }


    protected bool GetIsDamageTaken()
    {
        return _isDamageTaken;
    }

    protected void SetIsDamageTaken(bool isDamageTaken)
    {
        _isDamageTaken = isDamageTaken;
    }

}
