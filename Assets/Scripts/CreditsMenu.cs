using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0); // volta ao menu
    }
}
