using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{
    public int scorePlastic;
    public Text scorePText;

    public int scoreGlass;
    public Text scoreGText;

    public int scorePaper;
    public Text scorePaperText;

    public int scoreCan;
    public Text scoreCText;

    public static GameControler instance;

    public GameObject gameOverPanel;

    private void Awake()
    {
        instance = this;

        Time.timeScale = 1;
        

        GetPlastic();
        GetGlass();
        GetPaper();
        GetCan();
    }

    private void Start()
    {
        scorePlastic = 0;
        scoreGlass = 0;
        scorePaper = 0;
        scoreCan = 0;

        UpdateUI(); // Atualiza os textos com 0

        if (scorePText == null || scoreGText == null || scorePaperText == null || scoreCText == null)
        {
            Debug.LogError("Algum campo de texto não está atribuído no Inspector!");
        }
    }
    private void UpdateUI()
    {
        scorePText.text = scorePlastic.ToString();
        scoreGText.text = scoreGlass.ToString();
        scorePaperText.text = scorePaper.ToString();
        scoreCText.text = scoreCan.ToString();
    }
    public void GetPlastic()
    {
        scorePlastic++;
        scorePText.text = scorePlastic.ToString();
        CheckLevelCompletion();
    }

    public void GetGlass()
    {
        scoreGlass++;
        scoreGText.text = scoreGlass.ToString();
        CheckLevelCompletion();
    }

    public void GetPaper()
    {
        scorePaper++;
        scorePaperText.text = scorePaper.ToString();
        CheckLevelCompletion();
    }

    public void GetCan()
    {
        scoreCan++;
        scoreCText.text = scoreCan.ToString();
        CheckLevelCompletion();
    }

    // Transição de Level.
    public void NextLvl()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Última fase alcançada.");
        }
    }

    private void CheckLevelCompletion()
    {
        if (scorePlastic >= 3 && scoreGlass >= 3 && scorePaper >= 3 && scoreCan >= 3)
        {
            NextLvl();
        }
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
