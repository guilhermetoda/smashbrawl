using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    //current Health
    [SerializeField] private int _currentHealth = 3;

    [SerializeField] private GameObject _deathParticlesPrefab;
    [SerializeField] private GameObject _damageParticlesPrefab;
    //particles duration
    [SerializeField] private float _deathParticlesDuration = 2f;
    [SerializeField] private float _damageParticlesDuration = 2f;

    [SerializeField] private UnityEvent DeathEvent;
    //Creating an event when the character receives damage
    [SerializeField] private UnityEvent HitEvent;

    //maximum health
    private int _maxHealth;

    //awake is called
    private void Awake()
    {
        _maxHealth = _currentHealth;
    }

    public void Damage(int amount)
    {
        _currentHealth -= amount;

        //check if is a die
        if (_currentHealth <= 0)
        {
            if (_deathParticlesPrefab != null)
            {
                GameObject spawnParticles = Instantiate(_deathParticlesPrefab, transform.position, Quaternion.identity);
                Destroy(spawnParticles, _deathParticlesDuration);
            }

            if (DeathEvent != null)
            {
                DeathEvent.Invoke();
            }

            //DIEEEE
            Destroy(gameObject);

        }
        else
        {
            if (_damageParticlesPrefab != null)
            {
                GameObject spawnParticles = Instantiate(_damageParticlesPrefab, transform.position, Quaternion.identity);
                Destroy(spawnParticles, _damageParticlesDuration);
            }

            if (HitEvent != null)
            {
                HitEvent.Invoke();
            }
        }
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    //returns current % of health
    public float GetHealthPercentage()
    {
        return (float)_currentHealth / _maxHealth;
    }

}