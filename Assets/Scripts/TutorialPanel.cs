using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    public GameObject tutorialPanel;

    // Mostra o painel
    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    // Esconde o painel
    public void HideTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}