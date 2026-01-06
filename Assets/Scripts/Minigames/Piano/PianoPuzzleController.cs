using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPuzzleController : MonoBehaviour
{
    // Define os estados do puzzle
    public enum PuzzleState
    {
        Inativo,
        SimonParte1,
        RevelarPeca5,
        InserirPeca5,
        SimonParte2,
        Completo
    }

    // Estado atual do puzzle
    public PuzzleState currentState = PuzzleState.Inativo;

    // Referencias as teclas do piano
    public PianoKey[] keys; // tamanho 5
    public GameObject key5Object;

    // Sequencias do puzzle
    public List<int> part1Sequence = new List<int>() { 0, 2, 1, 3 };
    public List<int> fullSequence = new List<int>() { 0, 2, 1, 3, 4 };

    // Variaveis para controlar o input do jogador
    private int playerIndex;
    private int currentLength;

    private bool inputAtivo;

    // Serve para usar o Mouse e o camera controller para sair do piano
    [SerializeField] private PianoCameraController cameraController;
    [SerializeField] private EnableMouse mouseController;


    void Start()
    {
        LoadProgress();
    }

    void Update()
    {
        if (!inputAtivo) return;
        // Detecta clique do rato
        if (Input.GetMouseButtonDown(0))
        {
            // Cria um raycast a partir da pos���o do rato
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Verifica se o raycast colidiu com alguma tecla
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                PianoKey key = hit.collider.GetComponent<PianoKey>();
                // Se houver uma colis�o com uma tecla � processado o input
                if (key != null)
                    HandleKeyPress(key.keyID);
            }
        }
    }

    public void StartPuzzle()
    {
        inputAtivo = true;

        if (currentState == PuzzleState.Inativo)
            StartPart1();
        else if (currentState == PuzzleState.SimonParte2)
            StartPart2();
    }
   
    void HandleKeyPress(int keyID)
    {
        keys[keyID].Play();

        if (currentState == PuzzleState.SimonParte1)
            HandlePart1(keyID);
        else if (currentState == PuzzleState.SimonParte2)
            HandlePart2(keyID);
    }
    
    // Parte um do puzzle
    void StartPart1()
    {
        currentState = PuzzleState.SimonParte1;
        playerIndex = 0;
    }

    void HandlePart1(int keyID)
    {
        if (keyID == part1Sequence[playerIndex])
        {
            playerIndex++;
            if (playerIndex >= part1Sequence.Count)
                StartCoroutine(CompletePart1());
        }
        else
        {
            playerIndex = 0;
        }
    }

    IEnumerator CompletePart1()
    {
        inputAtivo = false;
        yield return new WaitForSeconds(0.5f);

        currentState = PuzzleState.RevelarPeca5;
        RevealKey5();
    }

    // Pe�a que falta
    void RevealKey5()
    {
        // Aqui disparas a anima��o das meshes
        Debug.Log("Revelar pe�a 5");
    }


    public void InsertKey5()
    {
        key5Object.SetActive(true);
        SaveProgress();

        currentState = PuzzleState.SimonParte2;
        StartPart2();
    }

    //Parte dois do puzzle

    void StartPart2()
    {
        currentState = PuzzleState.SimonParte2;
        inputAtivo = true;
        playerIndex = 0;
        currentLength = fullSequence.Count;
    }

    void HandlePart2(int keyID)
    {
        int expectedKey = (currentLength == 1) ? 4 : fullSequence[playerIndex];

        if (keyID == expectedKey)
        {
            playerIndex++;

            if (playerIndex >= currentLength)
            {
                currentLength--;
                playerIndex = 0;

                if (currentLength <= 0)
                    CompletePuzzle();
            }
        }
        else
        {
            RestartPart2();
        }
    }

    void RestartPart2()
    {
        playerIndex = 0;
    }

  

    void CompletePuzzle()
    {
        currentState = PuzzleState.Completo;
        ExitPiano();
        Debug.Log("Puzzle Completo!");
    }

    public void ExitPiano()
    {
        cameraController.ExitPiano();
        mouseController.DisableMouse();
    }



    //Salvar o processo do jogador, para que ele possa continuar da parte dois se perder na mesma
    void SaveProgress()
    {
        PlayerPrefs.SetInt("PianoPuzzleStage", 1);
    }

    void LoadProgress()
    {
        int stage = PlayerPrefs.GetInt("PianoPuzzleStage", 0);

        if (stage >= 1)
        {
            key5Object.SetActive(true);
            currentState = PuzzleState.SimonParte2;
        }
    }
}
