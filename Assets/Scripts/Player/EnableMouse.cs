using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class EnableMouse : MonoBehaviour
{
    private bool mouse = false;
    [SerializeField] private GameObject cam;
    private CinemachineInputAxisController input;
    private Controls moveScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveScript = GetComponent<Controls>();
        input = cam.GetComponent<CinemachineInputAxisController>();
    }
    public bool MouseEnabled()
    {
        return mouse;
    }
    public void ChangeMouse(bool x, bool y)
    {
        moveScript.ChangeMovementSpecific(x);
        mouse = y;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse)
        {
            moveScript.ChangeMovementSpecific(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            input.enabled = false;
        }
        else
        {
            moveScript.ChangeMovementSpecific(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            input.enabled = true;
        }
    }
}
