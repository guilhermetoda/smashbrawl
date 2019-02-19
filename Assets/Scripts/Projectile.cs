using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float _velocity = 10f; // foward launch velocity
    [SerializeField] private float _lifeTime = 5f; // life time until destruction

    protected Rigidbody2D _rigidBody; //rigidbody on the projectile

    private void Awake()
    {
        // get the rigidbody and apply velocity
        _rigidBody = GetComponent<Rigidbody2D>();
        //changing the velocity applied to the projectile goes or left or right
        if (transform.right.x < 0)
        {
            _rigidBody.velocity = new Vector2(_velocity, 0f);
        }
        else
        {
            _rigidBody.velocity = new Vector2(-_velocity, 0f);
        }

        //destroy after life time ends
        Destroy(gameObject, _lifeTime);
    }

}
