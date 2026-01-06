using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using System.Collections;

public class EnableMouse : MonoBehaviour
{
    private bool mouse = false;

    [SerializeField] private GameObject cam;

    private CinemachineInputAxisController input;
    private Controls moveScript;

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
        ApplyState();
    }

    public void DisableMouse()
    {
        ChangeMouse(true, false);
    }

    public void EnableMouseVoid()
    {
        ChangeMouse(false, true);
    }

    public void TemporaryDisableMouse(float delay)
    {
        StartCoroutine(TemporaryMouse(delay));
    }

    private IEnumerator TemporaryMouse(float delay)
    {
        ChangeMouse(true, false);
        yield return new WaitForSeconds(delay);
        ChangeMouse(false, true);
    }

    private void ApplyState()
    {
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





// First Version
//using UnityEngine;
//using Unity.Cinemachine;
//using UnityEngine.InputSystem;
//using System.Collections;

//public class EnableMouse : MonoBehaviour
//{
//    private bool mouse = false;
//    [SerializeField] private GameObject cam;
//    private CinemachineInputAxisController input;
//    private Controls moveScript;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        moveScript = GetComponent<Controls>();
//        input = cam.GetComponent<CinemachineInputAxisController>();
//    }
//    public bool MouseEnabled()
//    {
//        return mouse;
//    }
//    public void ChangeMouse(bool x, bool y)
//    {
//        moveScript.ChangeMovementSpecific(x);
//        mouse = y;
//    }

//    public void DisableMouse()
//    {
//        ChangeMouse(true, false);
//    }

//    public void EnableMouseVoid()
//    {
//        ChangeMouse(false, true);
//    }

//    public void TemporaryDisableMouse(float delay)
//    {
//        StartCoroutine(TemporaryMouse(delay));
//    }

//    private IEnumerator TemporaryMouse(float delay)
//    {
//        ChangeMouse(true, false);
//        yield return new WaitForSeconds(delay);
//        ChangeMouse(false, true);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (mouse)
//        {
//            moveScript.ChangeMovementSpecific(false);
//            Cursor.visible = true;
//            Cursor.lockState = CursorLockMode.None;
//            input.enabled = false;
//        }
//        else
//        {
//            moveScript.ChangeMovementSpecific(true);
//            Cursor.visible = false;
//            Cursor.lockState = CursorLockMode.Locked;
//            input.enabled = true;
//        }
//    }
//}

