using UnityEngine;

public class StartSpinMinigame : MonoBehaviour
{
    [SerializeField] private EnableMouse mouse;
    [SerializeField] private GameObject minigame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void MinigameBegin()
    {
        mouse.ChangeMouse(false, true);
        minigame.SetActive(true); 
    }
}
