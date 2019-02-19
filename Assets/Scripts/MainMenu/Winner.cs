using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Winner : MonoBehaviour {

    //text component
    private TextMeshProUGUI _text;

    private void Awake()
    {
        //get the text component
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void GameOver()
    {
        if (GameScore.winnerIndex != -1)
        {
            _text.SetText("Player " + GameScore.winnerIndex + " Wins!");
        }
        else
        {
            _text.SetText("Draw !");
        }
    }

}
