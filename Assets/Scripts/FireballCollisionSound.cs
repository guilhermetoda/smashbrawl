using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollisionSound : MonoBehaviour {

    [SerializeField] AudioClip _audioClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2d(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _audioSource.Stop();
            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }
    }

}
