using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuyManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame(string sceneName2)
    {
        SceneManager.LoadScene(sceneName2);
    }
}
