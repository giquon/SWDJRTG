using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{
    public Text score;

    private void Awake()
    {
        score.text = "Your Score: " + PlayerPrefs.GetInt("tempscore");
    }

    public void Sumbit()
    {
        //handle submissions here :)
        SceneManager.LoadScene("Scoreboard", LoadSceneMode.Single);
    }

    public void GotoScoreboard()
    {
        SceneManager.LoadScene("Scoreboard", LoadSceneMode.Single);
    }
}
