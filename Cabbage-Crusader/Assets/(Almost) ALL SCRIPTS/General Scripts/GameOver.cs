using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("Fight1");
    }
    
    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
