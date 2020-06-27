using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreboardManager : MonoBehaviour
{
    public Text score;

    private void Awake()
    {
        score.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
    }

    public void GotoPlayGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
