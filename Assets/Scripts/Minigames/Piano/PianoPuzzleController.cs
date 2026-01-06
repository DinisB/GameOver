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

    // SIMON SAYS (parte 2)
    private List<int> currentSequence = new List<int>();
    private int playerIndexSimon;

    // Estado atual do puzzle
    public PuzzleState currentState = PuzzleState.Inativo;

    // Espelhos e peça 5 interagivel
    [SerializeField] private GameObject mirrorGroup;
    [SerializeField] private Animator mirrorAnimator;
    [SerializeField] private Interactive key5Pickup;

    // Referencias as teclas do piano
    public PianoKey[] keys; // tamanho 5
    public GameObject key5Object;

    // Chave da area da musica
    [SerializeField] private Interactive areaKeyObject;

    // Sequencias do puzzle
    public List<int> part1Sequence = new List<int>() { 0, 2, 1, 3 };
   
    // Variaveis para controlar o input do jogador
    private int playerIndex;
    

    private bool inputAtivo;

    // Serve para usar o Mouse e o camera controller para sair do piano
    [SerializeField] private PianoCameraController cameraController;
    [SerializeField] private EnableMouse mouseController;


    void Start()
    {
        PlayerPrefs.DeleteKey("PianoPuzzleStage");
        currentState = PuzzleState.Inativo;
        // LoadProgress();
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

    void AddNewSimonStep()
    {
        int randomKey = Random.Range(0, keys.Length);
        currentSequence.Add(randomKey);
    }

    IEnumerator PlaySimonSequence()
    {
        inputAtivo = false;

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < currentSequence.Count; i++)
        {
            keys[currentSequence[i]].Play();
            yield return new WaitForSeconds(0.8f);
        }

        playerIndexSimon = 0;
        inputAtivo = true;
    }

    IEnumerator NextSimonRound()
    {
        inputAtivo = false;
        yield return new WaitForSeconds(0.6f);

        AddNewSimonStep();

        if (currentSequence.Count >= 5)
        {
            CompletePuzzle();
        }
        else
        {
            StartCoroutine(PlaySimonSequence());
        }
    }



    public void StartPuzzle()
{
    inputAtivo = true;

    if (currentState == PuzzleState.Inativo)
    {
        StartPart1();
    }
    else if (currentState == PuzzleState.InserirPeca5)
    {
        ShowKey5OnPiano();
        StartPart2();
    }
    else if (currentState == PuzzleState.SimonParte2)
    {
        StartPart2();
    }
}

   
    void HandleKeyPress(int keyID)
    {
        if (keyID < 0 || keyID >= keys.Length)
            return;

        keys[keyID].Play();

        if (currentState == PuzzleState.SimonParte1)
            HandlePart1(keyID);
        else if (currentState == PuzzleState.SimonParte2)
            HandlePart2(keyID);
    }

    void ShowKey5OnPiano()
    {
        key5Object.SetActive(true);
        currentState = PuzzleState.SimonParte2;
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

    
    void RevealKey5()
    {
        mirrorGroup.SetActive(true);
        mirrorAnimator.SetTrigger("Reveal");
        key5Pickup.gameObject.SetActive(true);
        key5Pickup.isOn = true;

        inputAtivo = false;  
        ExitPiano();
    }

    public void OnKey5PickedUp()
    {
        currentState = PuzzleState.InserirPeca5;
    }


    //public void InsertKey5()
    //{
    //ey5Object.SetActive(true);
    //SaveProgress();

    // currentState = PuzzleState.SimonParte2;
    //StartPart2();
    // }

    //Parte dois do puzzle

    void StartPart2()
    {
        currentState = PuzzleState.SimonParte2;
        inputAtivo = false;

        currentSequence.Clear();
        playerIndexSimon = 0;

        AddNewSimonStep();
        StartCoroutine(PlaySimonSequence());
    }

    void HandlePart2(int keyID)
    {
        if (keyID == currentSequence[playerIndexSimon])
        {
            playerIndexSimon++;

            if (playerIndexSimon >= currentSequence.Count)
            {
                StartCoroutine(NextSimonRound());
            }
        }
        else
        {
            playerIndexSimon = 0;
            StartCoroutine(PlaySimonSequence());
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

        areaKeyObject.gameObject.SetActive(true);

        areaKeyObject.isOn = true;
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
            // garante que a peça 5 nao apareca novamente para poder ser interagida
            key5Pickup.gameObject.SetActive(false);
            currentState = PuzzleState.SimonParte2;
        }
    }
}
