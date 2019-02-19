using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character 
{
	// is the enemy moving right?
	private bool _moveRight = true;

    // overriding Update in Character
    protected override void Update()
	{
		// call Character.Update
		base.Update();

        if (!GetIsDead())
        {

            // check if enemy has walked off platform to the left
            if (!GetGroundedLeft())
            {
                // move right
                _moveRight = true;
            }
            else if (!GetGroundedRight()) // walked off platform to the right
            {
                // move left
                _moveRight = false;
            }

            // moveInput = 1 if moving right, -1 if moving left
            // this reads: if _moveRight is true, then return 1f, else return -1f
            float moveInput = _moveRight ? 1f : -1f;

            ApplyMoveInput(moveInput);

            //Attack player if he is colliding

        }


	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!GetIsAttacking())
            {
                StartAttack();
                Debug.Log("EnemyAttack");
            }
        }
    }
}