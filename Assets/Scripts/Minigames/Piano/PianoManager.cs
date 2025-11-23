using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PianoManager : MonoBehaviour
{
    [SerializeField] private RawImage[] pianoKeys;
    [SerializeField] private AudioClip[] pianoSounds;
    [SerializeField] private AudioSource pianoSource;
    [SerializeField] private Button quit;
    [SerializeField] private GameObject keyg;
    private bool completed = false;
    private EnableMouse mouse;
    private List<int> pianoTune = new List<int> {
    3, 2, 1, 2, 3, 3, 3,
    2, 2, 2,
    3, 4, 4,
    3, 2, 1, 2, 3, 3, 3
};

    private List<int> currentKey;

    void Start()
    {
        currentKey = new List<int>(pianoTune);

        quit.onClick.AddListener(() => QuitMinigame());

        mouse = FindFirstObjectByType<EnableMouse>().GetComponent<EnableMouse>();
    }


    void Update()
    {
        for (int i = 1; i <= 4; i++)
        {
            KeyCode key = KeyCode.Alpha0 + i;

            if (Input.GetKeyDown(key))
            {
                if (i - 1 < pianoKeys.Length)
                {
                    pianoSource.PlayOneShot(pianoSounds[i - 1]);

                    if (currentKey.Count > 0 && i == currentKey[0])
                    {
                        currentKey.RemoveAt(0);
                        Debug.Log("Certo!");
                        pianoKeys[i - 1].color = Color.green;
                        if (currentKey.Count == 0 && !completed)
                        {
                            completed = true;
                            Debug.Log("Ganhas-te!");
                            keyg.SetActive(true);
                            QuitMinigame();
                        }
                    }
                    else
                    {
                        currentKey = new List<int>(pianoTune);
                        Debug.Log("Errado!");
                        pianoKeys[i - 1].color = Color.red;
                    }
                }
            }

            if (Input.GetKeyUp(key))
            {
                if (i - 1 < pianoKeys.Length)
                {
                    pianoKeys[i - 1].color = Color.white;
                }
            }
        }
    }

    void QuitMinigame()
    {
        gameObject.SetActive(false);
        mouse.ChangeMouse(true, false);
    }
}
