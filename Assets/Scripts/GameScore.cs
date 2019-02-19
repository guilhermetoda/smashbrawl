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
    // when the first player gets 10 gems, he wins the battle.
    public static int _numberOfGemsToWin = 5;
    public static bool _gameOver = false;
    public static int winnerIndex;

    //Array of Booleans, each position of the array represents the index of the player
    //Everytime that a player dies, this variable will change to false using the deathEvent
    public static bool[] _isPlayerAlive;


    private void Awake()
    {
        currentScore = new int[_numberOfPlayers];
        _isPlayerAlive = new bool[_numberOfPlayers];
        for (int i=0; i<_numberOfPlayers; i++) 
        {
            currentScore[i] = 0;
            _isPlayerAlive[i] = true;
        }
        winnerIndex = -1;

        //GameObject.Find("EndGameScreen").SetActive(false);


    }

    private void Update()
    {
        if (_gameOver)
        {
            GameOverEvent.Invoke();
        }
    }

    private static void CheckWinner(int newScore, int playerIndex)
    {
        if (newScore >= _numberOfGemsToWin)
        {
            _gameOver = true;
            winnerIndex = playerIndex;
        }
    }

    public static int GetPlayerScore(int playerIndex)
    {

        return currentScore[playerIndex];
    }

    public static int[] GetCurrentscore()
    {
        return currentScore;
    }

    public static void SetPlayerScore(int playerIndex, int playerScore)
    {
        currentScore[playerIndex] = playerScore;
        CheckWinner(playerScore, playerIndex);
    }

    public static void IncreasePlayerScore(int playerIndex)
    {
        currentScore[playerIndex]++;
        CheckWinner(currentScore[playerIndex], playerIndex);
    }

    public static void ResetPlayerScore(int playerIndex)
    {
        currentScore[playerIndex] = 0;
    }

    public static void SetPlayerIsDead(int playerIndex)
    {
        _isPlayerAlive[playerIndex] = false;
    }


}