using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;

    public Pacman pacman;

    public Transform pellets;

    public ScoreManager lifes;

    public EndGame endUI;

    public int ghostMultiply { get; private set; } = 1;

    public int score { get; private set; }
    public int lives { get; private set; }

    private string uri = "https://backend-aqzm.onrender.com/score/post";

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        /* if (this.lives <= 0 && Input.anyKeyDown)
         {
             NewGame();
         }*/
        endUI.retry.onClick.AddListener(() => NewGame());
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
        this.lifes.ResetState();
        endUI.ResetState();
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        ResetGhostMultiply();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern IntPtr GetLocalStorageValue();
#endif

    private void GameOver()
    {
        string token = "SetInBrowser";
#if UNITY_WEBGL && !UNITY_EDITOR
        IntPtr ptr = GetLocalStorageValue();
        token = Marshal.PtrToStringUTF8(ptr);
#endif
        StartCoroutine(PostScoreToEndpoint(uri, score, 1, token));

        endUI.Enable();
        lifes.HideScore();

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(this.score + (ghost.points * this.ghostMultiply));
        this.ghostMultiply++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }
        Invoke(nameof(ResetGhostMultiply), pellet.duration);
        CancelInvoke();
        PelletEaten(pellet);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiply()
    {
        this.ghostMultiply = 1;
    }

    IEnumerator PostScoreToEndpoint(string uri, int score, int gameId, string accountIdToken)
    {
        ScoreData scoreData = new();
        scoreData.score = score;
        scoreData.gameId = gameId;
        string json = JsonUtility.ToJson(scoreData);

        var req = new UnityWebRequest(uri, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        req.SetRequestHeader("Authorization", "Bearer " + accountIdToken);
        req.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error:" + req.error);
        }
        else
        {
            Debug.Log("Received: " + req.downloadHandler.text);
        }
    }

    [System.Serializable]
    private class ScoreData
    {
        public int score;
        public int gameId;
    }
}
