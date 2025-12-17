using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private float _maxInteractionDistance;

    private Transform _cameraTransform;
    private Interactive _currentInteractive;
    private bool _refreshCurrentInteractive;

    private Ray raymouse;
    private RectTransform rect;
    private EnableMouse mouse;
    [SerializeField] private float range = 8f;

    void Start()
    {
        rect = FindAnyObjectByType<Canvas>().GetComponentInChildren<RectTransform>();
        mouse = FindAnyObjectByType<EnableMouse>().GetComponent<EnableMouse>();
        _cameraTransform = GetComponentInChildren<Camera>().transform;
        _currentInteractive = null;
        _refreshCurrentInteractive = false;
    }

    void Update()
    {
        if (mouse.MouseEnabled() == true) { raymouse = Camera.main.ScreenPointToRay(Input.mousePosition); }
        else if (mouse.MouseEnabled() == false) { raymouse = Camera.main.ScreenPointToRay(rect.position); }

        UpdateCurrentInteractive();
        CheckForPlayerInteraction();
    }

    private void UpdateCurrentInteractive()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(raymouse, out hit, range))
            CheckObjectForInteraction(hit.collider);
        else if (_currentInteractive != null)
            ClearCurrentInteractive();
    }

    private void CheckObjectForInteraction(Collider collider)
    {
        Interactive interactive = collider.GetComponent<Interactive>();

        if (interactive == null || !interactive.isOn)
        {
            if (_currentInteractive != null)
                ClearCurrentInteractive();
        }
        else if (interactive != _currentInteractive || _refreshCurrentInteractive)
            SetCurrentInteractive(interactive);
    }

    private void ClearCurrentInteractive()
    {
        _currentInteractive = null;
        _uiManager.ShowDefaultCrosshair();
        _uiManager.HideInteractionPanel();
    }

    private void SetCurrentInteractive(Interactive interactive)
    {
        _currentInteractive = interactive;
        _refreshCurrentInteractive = false;

        string interactionMessage = interactive.GetInteractionMessage();

        if (interactionMessage != null && interactionMessage.Length > 0)
        {
            _uiManager.ShowInteractionCrosshair();
            _uiManager.ShowInteractionPanel(interactionMessage);
        }
        else
        {
            _uiManager.ShowDefaultCrosshair();
            _uiManager.HideInteractionPanel();
        }
    }

    private void CheckForPlayerInteraction()
    {
        if (Input.GetButtonDown("Interact") && _currentInteractive != null)
        {
            _currentInteractive.Interact();
            _refreshCurrentInteractive = true;
        }
    }

    public void RefreshCurrentInteractive()
    {
        _refreshCurrentInteractive = true;
    }
}
