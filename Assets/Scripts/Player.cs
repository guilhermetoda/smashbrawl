using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    //Input variables for the player
    [Header("Input")]
    [SerializeField] private int _playerIndex;
    [SerializeField] private KeyCode _jumpKey;

    // Attack variables
    [Header("Attack")]
    //Sound of swing the sword
    [SerializeField] private AudioClip _swordSound;
    // Layers that the player is allowed to attack
    [SerializeField] private LayerMask _attackingLayers;
    // Gameobject to control the "trigger volume" of the attack, using Overlap Box
    [SerializeField] private Transform _attackPos;
    // Range of X-Axis of the attack box (overlap box)
    [SerializeField] private float _attackRangeX;
    // Range of Y-Axis of the attack box (overlap box)
    [SerializeField] private float _attackRangeY;
    // Amount of damage of each attack
    [SerializeField] private int _damage = 10;

    //Audio source to play the swordSound
    private AudioSource _audioSource;

    private int collectedGems = 0;

    protected override void Update()
    {
        base.Update();

        // apply move input to parent class
        float horizontalInput = Input.GetAxis("Horizontal" + _playerIndex);
        ApplyMoveInput(horizontalInput);

        // jump
        if(Input.GetKeyDown(_jumpKey))
        {
            TryJump();
        }


        // Player Attacking Mechanics
        // If the player is not attacking and press the "attack' button
        if (!GetIsAttacking() && Input.GetButtonDown("Fire1" + _playerIndex))
        {
            StartAttack();
            SetIsDamageTaken(false);
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _swordSound;
            _audioSource.Play();
        }
        // If the player is attacking and the damage hasn't taken yet, this avoid to double,triple or more damage with just one swings
        if (GetIsAttacking() && !GetIsDamageTaken())
        {
            Collider2D[] damageColliders = Physics2D.OverlapBoxAll(_attackPos.position, new Vector2(_attackRangeX, _attackRangeY), _attackingLayers);
            for (int i=0; i<damageColliders.Length; i++)
            {

                //Check if the attacker is the player
                Player playerAttacked = damageColliders[i].GetComponent<Player>();
                if (playerAttacked != null)
                {
                    if (playerAttacked._playerIndex == _playerIndex)
                    {
                        // Won't self attack
                        continue;
                    }
                }

                Health otherHealth = damageColliders[i].GetComponent<Health>();
                //check if the gameObject has Health Component
                if (otherHealth != null)
                {
                    //damage the other health
                    otherHealth.Damage(_damage);
                }
            }
            // Set the damage to not take damage again on the next frame.
            SetIsDamageTaken(true);
        }

        if (GetIsHit())
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // Getting Pickup Itens
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gem"))
        {
            //Destroy(this);
            Destroy(collision.gameObject);
            collectedGems++;
            GameScore.SetPlayerScore(_playerIndex, collectedGems);
        }
    }

    public int GetPlayerIndex()
    {
        return _playerIndex;
    }

    // Overriding KillCharacter to set that the player is Dead
    public override void KillCharacter()
    {
        base.KillCharacter();
        GameScore.SetPlayerIsDead(_playerIndex);
    }

    // Debug the Attack Radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_attackPos.position, new Vector3(_attackRangeX, _attackRangeY, 1));
    }

}

