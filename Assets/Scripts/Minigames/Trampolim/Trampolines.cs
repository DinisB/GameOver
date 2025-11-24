using UnityEngine;

public class Trampolines : MonoBehaviour
{
    //propriedades do trampolim
    [Header("Config ai")]
    [SerializeField] private Color trampolineColor = Color.white;
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private Renderer objectRenderer;
    [SerializeField] private float jumpForce = 5f;
    private Controls playerControls;

    //construtores e metodos
    private void Start()
    {
        objectRenderer.material.color = trampolineColor;
        playerControls = FindFirstObjectByType<Controls>().GetComponent<Controls>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerControls.JumpFromExternal(jumpForce);

            // Usa o PuzzleManager
            puzzleManager.CheckTrampoline(trampolineColor);

            jumpSound.Play();

            // Feedback visual para o trampolim
            objectRenderer.material.color = Color.yellow;
            Invoke(nameof(ResetColor), 0.3f);
        }
    }
    public void SetColor(Color newColor)
    {
        trampolineColor = newColor;
        objectRenderer.material.color = newColor;
    }

    private void ResetColor()
    {
        objectRenderer.material.color = trampolineColor;
    }
}


