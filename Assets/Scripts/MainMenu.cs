using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits"); // ou use SceneManager.LoadScene(2); se for pelo índice
    }
}
