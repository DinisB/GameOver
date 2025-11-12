using UnityEngine;
using UnityEngine.InputSystem;
public class CheckMouse : MonoBehaviour
{
    private Ray raymouse;
    private RectTransform rect;
    private EnableMouse mouse;
    [SerializeField] private InputActionReference clickAction;
    [SerializeField] private float range = 8f;
    [SerializeField] private int mini;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = FindAnyObjectByType<Canvas>().GetComponentInChildren<RectTransform>();
        mouse = FindAnyObjectByType<EnableMouse>().GetComponent<EnableMouse>();
    }
    private void OnEnable()
    {
        clickAction.action.Enable();
    }

    private void OnDisable()
    {
        clickAction.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.MouseEnabled() == true) { raymouse = Camera.main.ScreenPointToRay(Input.mousePosition); }
        else if (mouse.MouseEnabled() == false) { raymouse = Camera.main.ScreenPointToRay(rect.position); }

        RaycastHit hit;

        if (Physics.Raycast(raymouse, out hit, range))
        {
            if (clickAction.action.triggered && hit.collider.name == gameObject.name)
            {
                if (mini == 0) {
                    gameObject.GetComponent<StartSpinMinigame>().enabled = true;
                    gameObject.GetComponent<StartSpinMinigame>().MinigameBegin(); }

                if (mini == 1)
                {
                    gameObject.GetComponent<StartPianoMinigame>().enabled = true;
                    gameObject.GetComponent<StartPianoMinigame>().MinigameBegin();
                }
            }
        }
    }
}
