using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {

    [SerializeField] GameObject[] _playersPrefab; 

    private void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i=0; i<GameScore._numberOfPlayers; i++)
        {
            // If the player is dead
            if (!GameScore._isPlayerAlive[i])
            {
                //then, respawn
                Instantiate(_playersPrefab[i], transform);
                GameScore._isPlayerAlive[i] = true;
            }    

        }
    }
}
