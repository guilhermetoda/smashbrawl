using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    // load scene from string
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // quit game
    public void ExitGame()
    {
        Application.Quit();
    }

}