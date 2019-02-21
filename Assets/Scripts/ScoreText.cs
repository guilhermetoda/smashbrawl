using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{

    [SerializeField] int _playerIndex;

    //text component
    private TextMeshProUGUI _text;

    private void Awake()
    {
        //get the text component
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.SetText(GameScore.GetPlayerScore(_playerIndex).ToString());
        
    }

}