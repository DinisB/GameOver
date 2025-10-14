using UnityEngine;

public class StartMinigame : MonoBehaviour
{
    [SerializeField] private EnableMouse mouse;
    [SerializeField] private GameObject minigame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouse.ChangeMouse();
        minigame.SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
