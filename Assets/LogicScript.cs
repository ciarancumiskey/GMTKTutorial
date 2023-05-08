using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text playerScoreText;
    public Text highScoreText;
    public GameObject gameOverScreen;
    private int highScore;

    private const string PREF_HIGH_SCORE = "highScore";

    private void Start()
    {
        highScore = PlayerPrefs.GetInt(PREF_HIGH_SCORE, 0);
        Debug.Log("Loaded high score: " + highScore.ToString());
        highScoreText.text = string.Format("High Score: {0}", highScore);
    }

    [ContextMenu("Increase Score")]
    public void AddScore(int scoreIncrease)
    {
        if (!gameOverScreen.activeInHierarchy)
        {
            playerScore += scoreIncrease;
            playerScoreText.text = string.Format("Score: {0}", playerScore);
            if(playerScore > highScore)
            {
                Debug.Log("New high score set!");
                // Use the player's score in the high score field
                highScoreText.text = string.Format("High Score: {0}", playerScore);
                highScoreText.color = Color.yellow; // Change the text colour to signify the updated high score
            }
        }
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [ContextMenu("Game Over")]
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        GameObject gameOverScoreObject = GameObject.Find("GameOverScoreText");
        TMP_Text gameOverScoreText = gameOverScoreObject.GetComponent<TMP_Text>();
        if (playerScore > highScore)
        {            
            PlayerPrefs.SetInt(PREF_HIGH_SCORE, playerScore); //Overwrite the high score
            gameOverScoreText.text = string.Format("New High Score! {0}", playerScore);
            gameOverScoreText.color = Color.yellow;
        } else
        {
            gameOverScoreText.text = string.Format("Your Score: {0}", playerScore);
        }
    }

    [ContextMenu("Quit Game")]
    public void QuitGame()
    {
        Application.Quit();
    }
}
