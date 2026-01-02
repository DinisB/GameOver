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

    // Sequencia do jogador
    private List<Color> playerSequence = new List<Color>();
    [SerializeField] private int maxJumps = 6;

    private int currentStep = 0;
    private bool puzzleCompleted = false;

    public void CheckTrampoline(Color trampolineColor)
    {
        if (puzzleCompleted) return;
        if (playerSequence.Count >= maxJumps) return;

        // Guardar a escolha do jogador
        playerSequence.Add(trampolineColor);

        // Som diferente por salto
        int index = playerSequence.Count - 1;
        if (index < correctStepSounds.Count)
        {
            audioSource.PlayOneShot(correctStepSounds[index]);
        }

        // Se já fez os 6 saltos → avaliar
        if (playerSequence.Count == maxJumps)
        {
            EvaluateSequence();
        }

    }

    private void EvaluateSequence()
    {
        bool correct = true;

        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (playerSequence[i] != correctOrder[i])
            {
                correct = false;
                break;
            }
        }

        emissiveRenderer.material = correct ? emissiveGreen : emissiveRed;

        if (correct)
        {
            puzzleCompleted = true;
            audioSource.PlayOneShot(successSound, 1f);
            keyg.SetActive(true);
            FindFirstObjectByType<UIManager>().RefreshCoins();
        }
        else
        {
            audioSource.PlayOneShot(wrongSound);
            ResetPuzzle();
        }
    }


    private void ShuffleTrampolineOrder()
    {
        // Copiar as 4 cores base
        List<Color> shuffled = new List<Color>(baseColors);

        // Baralhar 
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
    private void ResetPuzzle()
    {
        playerSequence.Clear();
        ShuffleTrampolineOrder();
    }

}

