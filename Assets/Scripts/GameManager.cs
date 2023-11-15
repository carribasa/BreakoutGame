using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // attributes --------------------------------------------------
    [SerializeField]
    private int lives = 3;
    public TMP_Text livesText;

    [SerializeField]
    private int score;
    public TMP_Text scoreText;

    // ------------------------------------------------------------

    void Start()
    {
        UpdateLivesText();
    }

    private void Update()
    {
        UpdateScoreText();
    }

    // ------------------------------------------------------------

    public void LoseHealth()
    {
        lives--;
        UpdateLivesText();

        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            ResetLevel();
        }
    }

    public void ResetLevel()
    {
        FindObjectOfType<Ball>().ResetBall();
        FindObjectOfType<Player>().ResetPlayer();
    }

    public void CheckLevelCompleted()
    {
        if (transform.childCount <= 1)
        {
            SceneManager.LoadScene("Success");
        }
    }

    void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Vidas: " + lives.ToString();
        }
    }

    void UpdateScoreText()
    {
        if (livesText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void increaseScore(int score)
    {
        this.score += score;
    }

    public int Lives { get { return this.lives; } }
    public int Score { get { return this.score; } }
}
