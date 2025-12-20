using UnityEngine;

public class CheatScript : MonoBehaviour
{
    [SerializeField] private GameObject cheatui;
    [SerializeField] private EnableMouse mouse;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            mouse.ChangeMouse(false, true);
            cheatui.SetActive(true);
        }
    }

    public void Exit()
    {
        mouse.ChangeMouse(true, false);
    }
}
