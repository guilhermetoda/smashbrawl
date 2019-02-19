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
            /*// ***Quin, I dont know if this is the best way to do it, if not, please warn me.
            collision.SendMessageUpwards("Damage", damage);
            Debug.Log("Attacou");*/
    }
}
