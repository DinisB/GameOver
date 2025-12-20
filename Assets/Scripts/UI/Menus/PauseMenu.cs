using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private EnableMouse mouse;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                Pause();
            else
                Resume();
        }
    }

    private void Pause()
    {
        isPaused = true;

        container.SetActive(true);

        // Freeze game
        Time.timeScale = 0f;

        mouse.ChangeMouse(false, true);
    }

    public void Resume()
    {
        isPaused = false;

        container.SetActive(false);

        // Resume game
        Time.timeScale = 1f;

        mouse.ChangeMouse(true, false);
    }

    public void ResumeButton()
    {
        Resume();
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void QuitButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Bye");
    }
}
