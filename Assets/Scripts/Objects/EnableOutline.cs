using UnityEngine;

public class EnableOutline : MonoBehaviour
{
    private Outline outline;
    private Ray raymouse;
    private RectTransform rect;
    private EnableMouse mouse;
    private Color ogColor;

    [SerializeField] private float range = 8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = FindAnyObjectByType<Canvas>().GetComponentInChildren<RectTransform>();
        mouse = FindAnyObjectByType<EnableMouse>().GetComponent<EnableMouse>();
        outline = GetComponent<Outline>();
        ogColor = outline.OutlineColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.MouseEnabled() == true) { raymouse = Camera.main.ScreenPointToRay(Input.mousePosition); }
        else if (mouse.MouseEnabled() == false) { raymouse = Camera.main.ScreenPointToRay(rect.position); }

        RaycastHit hit;

        if (Physics.Raycast(raymouse, out hit, range))
        {
            Color targetColor = (hit.transform == transform) ? Color.yellow : ogColor;
            outline.OutlineColor = Color.Lerp(outline.OutlineColor, targetColor, Time.deltaTime * 10);
        }
        else
        {
            outline.OutlineColor = Color.Lerp(outline.OutlineColor, ogColor, Time.deltaTime * 10);
        }
    }
}
