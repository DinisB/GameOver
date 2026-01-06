using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
    public void End()
    {
        StartCoroutine(LoadGameOverScene());

    }

    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
