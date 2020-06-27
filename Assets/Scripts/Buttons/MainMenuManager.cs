using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void GotoPlayGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void GotoScoreboard()
    {
        SceneManager.LoadScene("Scoreboard", LoadSceneMode.Single);
    }

    public void GotoExit()
    {
        Application.Quit();
    }
}
