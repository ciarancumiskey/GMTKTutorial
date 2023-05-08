using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene(1); // The index of the gameplay scene in BuildSettings
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
