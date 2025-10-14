using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class EnableMouse : MonoBehaviour
{
    private bool mouse = false;
    [SerializeField] private GameObject cam;
    private CinemachineInputAxisController input;
    [SerializeField] private InputActionReference mouseAction;
    private bool canChange = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = cam.GetComponent<CinemachineInputAxisController>();
    }
    public bool MouseEnabled()
    {
        return mouse;
    }
    public void ChangeMouse()
    {
        canChange = !canChange;
        mouse = !mouse;
    }
    private void OnEnable()
    {
        mouseAction.action.Enable();
    }

    private void OnDisable()
    {
        mouseAction.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseAction.action.triggered && canChange) mouse = !mouse;

        if (mouse)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            input.enabled = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            input.enabled = true;
        }
    }
}
