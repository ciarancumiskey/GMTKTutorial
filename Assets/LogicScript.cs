using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text playerScoreText;
    public GameObject gameOverScreen;

    [ContextMenu("Increase Score")]
    public void AddScore(int scoreIncrease)
    {
        if (!gameOverScreen.activeInHierarchy)
        {
            playerScore += scoreIncrease;
            playerScoreText.text = playerScore.ToString();
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
    }
}
