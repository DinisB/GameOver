using UnityEngine;

public class TrampolineScript : MonoBehaviour
{
    [SerializeField] private bool checks = false;
    private Controls cc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (checks) cc = GetComponent<Controls>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<TrampolineScript>() != null && checks)
        {
            cc.JumpFromExternal(5f);
        }
    }
}
