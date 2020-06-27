using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menumanager : MonoBehaviour
{
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void GotoDeathscreen()
    {
        SceneManager.LoadScene("Deathscreen", LoadSceneMode.Single);
    }

    public void GotoScoreboard()
    {
        SceneManager.LoadScene("Scoreboard", LoadSceneMode.Single);
    }

    public void GotoGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
