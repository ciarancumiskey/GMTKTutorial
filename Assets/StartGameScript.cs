using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    public string levelName; // The name of the gameplay scene

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
