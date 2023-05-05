using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text playerScoreText;

    [ContextMenu("Increase Score")]
    public void AddScore()
    {
        playerScore += 1;
        playerScoreText.text = playerScore.ToString();
    }
}
