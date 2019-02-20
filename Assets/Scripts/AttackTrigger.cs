using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    [SerializeField] int _damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Health otherHealth = collision.gameObject.GetComponent<Health>();

        //check if the gameObject has Health Component
        if (otherHealth != null)
        {
            //damage the other health
            otherHealth.Damage(_damage);
        }
            
    }
}
