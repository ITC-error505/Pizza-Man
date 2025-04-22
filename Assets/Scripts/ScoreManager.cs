using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject[] life = new GameObject[3];
    public GameManager gameManager;
    public TextMeshProUGUI endScore;

   

    // Start is called before the first frame update
    void Start()
    {
        ResetState();
    }

    // Update is called once per frame
    void Update()
    {
        SetScore();

        if (gameManager.lives < 1)
        {
            life[0].gameObject.SetActive(false);
        }
        else if (gameManager.lives < 2) 
        {
            life[1].gameObject.SetActive(false);
        }

        else if (gameManager.lives < 3)
        {
            life[2].gameObject.SetActive(false);
        }
    }

    public void SetScore()
    {
        scoreText.text = "" + gameManager.score;
        endScore.text = "Score: " + gameManager.score;
    }

    public void ResetState()
    {
        SetScore();
        for (int i = 0; i < life.Length; i++)
        {
            life[i].gameObject.SetActive(true);
        }
        scoreText.enabled = true;
    }

    public void HideScore()
    {
        scoreText.enabled = false;
    }
}