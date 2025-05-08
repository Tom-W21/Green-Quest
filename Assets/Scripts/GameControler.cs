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

    public GameObject theEndPanel;

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
            if (IsLastLevel())
            {
                ShowTheEnd(); // Mostra o painel final
            }
            else
            {
                NextLvl(); // Continua normalmente
            }
        }
    }

    // Verifica se está na última cena
    private bool IsLastLevel()
    {
        return SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1;
    }

    // Método que mostra o painel The End
    public void ShowTheEnd()
    {
        Time.timeScale = 0;
        theEndPanel.SetActive(true);
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    // Voltar ao menu principal
    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); // Supondo que a cena do menu é a de índice 0
    }

    // Sair do jogo
    public void ExitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
