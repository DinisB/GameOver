using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Correct Order")]
    public List<Color> correctOrder = new List<Color>();

    [Header("Feedback")]
    public Light feedbackLight;
    public AudioSource audioSource;
    [SerializeField] private List<AudioClip> correctStepSounds = new List<AudioClip>();
    public AudioClip wrongSound;
    public AudioClip successSound;

    [Header("Spawn Object")]
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform spawnPoint;

    [Header("Trampolins")]
    [SerializeField] private List<Trampolines> trampolines = new List<Trampolines>();

    private int currentStep = 0;
    private bool puzzleCompleted = false;

    public void CheckTrampoline(Color trampolineColor, Vector3 position)
    {
        if (puzzleCompleted) return;
        if (currentStep >= correctOrder.Count) return;

        bool correct = trampolineColor == correctOrder[currentStep];
        feedbackLight.color = correct ? Color.green : Color.red;


        if (correct)
        {
            if (currentStep < correctStepSounds.Count)
            {
                audioSource.PlayOneShot(correctStepSounds[currentStep]);
            }

            currentStep++;

            if (currentStep >= correctOrder.Count)
            {
                puzzleCompleted = true;
                audioSource.PlayOneShot(successSound, 1f);
                Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else
        {
            currentStep = 0;
            audioSource.PlayOneShot(wrongSound);
            ShuffleTrampolineOrder();
        }

    }
    private void ShuffleTrampolineOrder()
    {
        // Cria uma cópia das cores originais
        List<Color> shuffledColors = new List<Color>(correctOrder);

        // Embaralha a lista
        for (int i = 0; i < shuffledColors.Count; i++)
        {
            Color temp = shuffledColors[i];
            int randomIndex = Random.Range(i, shuffledColors.Count);
            shuffledColors[i] = shuffledColors[randomIndex];
            shuffledColors[randomIndex] = temp;
        }

        // Aplica as cores embaralhadas aos trampolins
        for (int i = 0; i < trampolines.Count; i++)
        {
            trampolines[i].SetColor(shuffledColors[i]);
        }

        // Define nova ordem correta igual à lista embaralhada
        correctOrder = shuffledColors;
    }
}

