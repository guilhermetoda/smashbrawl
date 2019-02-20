using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// I am going to use this score as my static variable to the assignment, but I could add the score in the Player class.
public class GameScore : MonoBehaviour
{
    [SerializeField] private UnityEvent GameOverEvent;

    //current total score
    public static int[] currentScore;
    public static int _numberOfPlayers = 2;
    // when the first player gets 10 gems, he wins the match.
    public static int _numberOfGemsToWin = 10;
    //boolean flag to check if the game is over
    public static bool _gameOver = false;
    //Player Winners Index
    public static int winnerIndex;

    //Array of Booleans, each position of the array represents the index of the player
    //Everytime that a player dies, this variable will change to false using the deathEvent
    public static bool[] _isPlayerAlive;


    //Setting all the variables
    private void Awake()
    {
        currentScore = new int[_numberOfPlayers];
        _isPlayerAlive = new bool[_numberOfPlayers];
        for (int i=0; i<_numberOfPlayers; i++) 
        {
            currentScore[i] = 0;
            _isPlayerAlive[i] = false;
        }
        winnerIndex = -1;
        _gameOver = false;
    }
    // Checks if the Game is Over at every Frame
    private void Update()
    {
        if (_gameOver)
        {
            GameOverEvent.Invoke();
        }

    }

    //checks if the player wons
    //I am doing this to avoid checking this at every frame, but checking only when the player receives a new score
    private static void CheckWinner(int newScore, int playerIndex)
    {
        if (newScore >= _numberOfGemsToWin)
        {
            _gameOver = true;
            winnerIndex = playerIndex;
        }
    }
    // Return the player score
    public static int GetPlayerScore(int playerIndex)
    {
        return currentScore[playerIndex];
    }
    // Get all Scores (All players)
    public static int[] GetCurrentscore()
    {
        return currentScore;
    }

    // Set a new Score to the playerIndex, and check if he has the amount to win
    public static void SetPlayerScore(int playerIndex, int playerScore)
    {
        currentScore[playerIndex] = playerScore;
        CheckWinner(playerScore, playerIndex);
    }
    // Increase by 1 the player score
    public static void IncreasePlayerScore(int playerIndex)
    {
        currentScore[playerIndex]++;
        CheckWinner(currentScore[playerIndex], playerIndex);
    }
    // Reset the player score (used on the player dies)
    public static void ResetPlayerScore(int playerIndex)
    {
        currentScore[playerIndex] = 0;
    }
    // Set the array that the player is dead, I use that information to respawn the player in PlayerRespawn.cs
    public static void SetPlayerIsDead(int playerIndex)
    {
        _isPlayerAlive[playerIndex] = false;
        ResetPlayerScore(playerIndex);
    }


}