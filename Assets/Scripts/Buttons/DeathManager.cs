using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
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
