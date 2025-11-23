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
    [SerializeField] private GameObject keyg;

    [Header("Emissive Feedback Material")]
    public Renderer emissiveRenderer;
    public Material emissiveGreen;
    public Material emissiveRed;

    [Header("Trampolins")]
    [SerializeField] private List<Color> baseColors = new List<Color>();
    [SerializeField] private List<Trampolines> trampolines = new List<Trampolines>();

    private int currentStep = 0;
    private bool puzzleCompleted = false;

    public void CheckTrampoline(Color trampolineColor)
    {
        if (puzzleCompleted) return;
        if (currentStep >= correctOrder.Count) return;

        bool correct = trampolineColor == correctOrder[currentStep];

        emissiveRenderer.material = correct ? emissiveGreen : emissiveRed;


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
                keyg.SetActive(true);
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
        // Copiar as 4 cores base
        List<Color> shuffled = new List<Color>(baseColors);

        // Baralhar (Fisher�Yates)
        for (int i = 0; i < shuffled.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffled.Count);
            (shuffled[i], shuffled[randomIndex]) = (shuffled[randomIndex], shuffled[i]);
        }

        // Aplicar aos trampolins
        for (int i = 0; i < trampolines.Count; i++)
        {
            trampolines[i].SetColor(shuffled[i]);
        }
    }

}

