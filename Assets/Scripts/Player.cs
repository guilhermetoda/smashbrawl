using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Input")]
    [SerializeField] private int _playerIndex;
    [SerializeField] private KeyCode _jumpKey;

    [Header("Attack")]
    [SerializeField] private AudioClip _swordSound;

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
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _swordSound;
            _audioSource.Play();
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
            Debug.Log("Gem");
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
        Debug.Log("DEAD");
    }
}
