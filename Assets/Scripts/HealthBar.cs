using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] int _playerIndex;

    private Image _healthbar;

    private void Awake()
    {
        _healthbar = GetComponent<Image>();
    }

    private void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject player = null;
        for (int i=0; i < players.Length; i++)
        {
            if (players[i].GetComponent<Player>().GetPlayerIndex() == _playerIndex)
            {
                player = players[i];
            }
        }

        if (player != null)
        {
            Health health = player.GetComponent<Health>();
            if (health != null)
            {
                _healthbar.fillAmount = health.GetHealthPercentage();
            }
            else
            {
                _healthbar.fillAmount = 0;
            }
        }
    }
}
