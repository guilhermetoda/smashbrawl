using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    // damage dealt
    [SerializeField] private int _damage = 1;

    //minimum velocity for damage
    [SerializeField] private float _minVelocity = 1f;
    [SerializeField] private bool _destroyAfterCollision = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ensure that the minimum velocity was met
        if (collision.relativeVelocity.magnitude > _minVelocity)
        {
            Health otherHealth = collision.gameObject.GetComponent<Health>();

            //check if the gameObject has Health Component
            if (otherHealth != null)
            {
                //damage the other health
                otherHealth.Damage(_damage);
            }
        }
        if (_destroyAfterCollision) 
        { 
            Destroy(gameObject);
        }
    }

}