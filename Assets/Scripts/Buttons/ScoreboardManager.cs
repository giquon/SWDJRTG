using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardManager : MonoBehaviour
{
    public void GotoPlayGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
