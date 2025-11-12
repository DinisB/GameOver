using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PianoManager : MonoBehaviour
{
    [SerializeField] private RawImage[] pianoKeys;
    [SerializeField] private AudioClip[] pianoSounds;
    [SerializeField] private AudioSource pianoSource;
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
                    pianoKeys[i - 1].color = Color.gray;

                    if (currentKey.Count > 0 && i == currentKey[0])
                    {
                        currentKey.RemoveAt(0);
                        Debug.Log("Certo!");
                    }
                    else if (currentKey.Count == 1)
                    {
                        currentKey.RemoveAt(0);
                        Debug.Log("Ganhas-te!");
                    }
                    else if (currentKey.Count == 0)
                    {
                        Debug.Log("Ganhas-te!");
                    }
                    else
                    {
                        currentKey = new List<int>(pianoTune);
                        Debug.Log("Errado!");
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
}
