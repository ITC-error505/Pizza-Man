
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Canvas gameOver;
    public Button home;
    public Button leaderboard;
    public Button retry;

    public void ResetState()
    {
        gameOver.enabled = false;
        home.enabled = false;
        leaderboard.enabled = false;
        retry.enabled = false;
    }

    public void Update()
    {
        home.onClick.AddListener(() => Application.OpenURL("https://itc-error505.github.io/frontend/Games.html"));
        leaderboard.onClick.AddListener(() => Application.OpenURL("https://itc-error505.github.io/frontend/EndGameLeaderboard.html"));
    }
    public void Enable()
    {
        gameOver.enabled = true;
        home.enabled = true;
        leaderboard.enabled = true;
        retry.enabled = true;
    }
}
