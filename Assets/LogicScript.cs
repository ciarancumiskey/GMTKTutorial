using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public bool gameIsPaused = false;

    public Text playerScoreText;
    public Text highScoreText;
    public Text soundEnabledText;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public AudioSource checkpointAudio;
    public AudioSource collisionAudio;
    public AudioSource newHighScoreAudio;
    public AudioSource chirpAudio;

    private int highScore;
    private bool isHighScoreBeaten;
    private float soundEffectsVolume;
    private string soundEnabledTextString;

    public const string PREF_HIGH_SCORE = "highScore";
    public const string PREF_SOUND_ENABLED = "soundEnabled";

    private void Start()
    {
        // Load the high score
        highScore = PlayerPrefs.GetInt(PREF_HIGH_SCORE, 0);
        Debug.Log("Loaded high score: " + highScore.ToString());
        highScoreText.text = string.Format("High Score: {0}", highScore);

        // Manage sound effects
        soundEffectsVolume = PlayerPrefs.GetFloat(PREF_SOUND_ENABLED, 1.0f);
        checkpointAudio.mute = soundEffectsVolume == 0.0f;
        collisionAudio.mute = soundEffectsVolume == 0.0f;
        newHighScoreAudio.mute = soundEffectsVolume == 0.0f;
        chirpAudio.mute = soundEffectsVolume == 0.0f;
    }

    void Update()
    {
        // Don't show the pause screen if the player has already died
        if (!gameOverScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            } else
            {
                Debug.Log("How fast can you move in the frozen time?");
                PauseGame();
            }
        }
    }

    [ContextMenu("Increase Score")]
    public void AddScore(int scoreIncrease)
    {
        if (!gameOverScreen.activeInHierarchy)
        {
            playerScore += scoreIncrease;
            checkpointAudio.PlayOneShot(checkpointAudio.clip);
            playerScoreText.text = string.Format("Score: {0}", playerScore);
            if (playerScore > highScore)
            {
                if (!isHighScoreBeaten) // Only play the first time the user breaks their record
                {
                    newHighScoreAudio.Play();
                    isHighScoreBeaten = true;
                } else
                {
                    checkpointAudio.PlayOneShot(checkpointAudio.clip);
                }
                Debug.Log("New high score set!");
                // Use the player's score in the high score field
                highScoreText.text = string.Format("High Score: {0}", playerScore);
                highScoreText.color = Color.yellow; // Change the text colour to signify the updated high score
            } else
            {
                checkpointAudio.PlayOneShot(checkpointAudio.clip);
            }
        }
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame(); // Ensure that the game is unpaused once restarted
    }

    [ContextMenu("Game Over")]
    public void GameOver()
    {
        Time.timeScale = 0.2f; // slow down the game for added dramatisation
        gameOverScreen.SetActive(true);
        collisionAudio.PlayOneShot(collisionAudio.clip);
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

    public void ResumeGame()
    {
        gameIsPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
        // Re-enable chirping if the user hasn't disabled it
        chirpAudio.mute = soundEffectsVolume == 0.0f;
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        // Pause the game
        Time.timeScale = 0.0f;
        // Set the text for the pause screen
        soundEnabledTextString = chirpAudio.mute ? "Sound: Off" : "Sound: On";
        soundEnabledText.text = soundEnabledTextString;
        // Show the pause screen
        pauseScreen.SetActive(true);
        // Disable chirping
        chirpAudio.mute = true;
    }

    [ContextMenu("Chirp")]
    public void Chirp()
    {
        chirpAudio.Play();
    }

    [ContextMenu("ToggleSounds")]
    public void ToggleSounds()
    {
        if(soundEffectsVolume > 0f)
        {
            soundEffectsVolume = 0f;   
        } else
        {
            soundEffectsVolume = 1f;
        }
        PlayerPrefs.SetFloat(PREF_SOUND_ENABLED, soundEffectsVolume);
        checkpointAudio.mute = !checkpointAudio.mute;
        collisionAudio.mute = !collisionAudio.mute;
        newHighScoreAudio.mute = !newHighScoreAudio.mute;
        soundEnabledTextString = collisionAudio.mute ? "Sound: Off" : "Sound: On";
        soundEnabledText.text = soundEnabledTextString;
        Debug.Log(string.Format("Sounds enabled? {0}", !checkpointAudio.mute));
    }
}
