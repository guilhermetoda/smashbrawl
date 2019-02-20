using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {

    // Player prefabs, each position of the array represents one player
    [SerializeField] GameObject[] _playersPrefab; 

    private void Update()
    {
        // Check at every frame if there is a player dead
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
