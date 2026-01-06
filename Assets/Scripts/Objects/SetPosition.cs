using UnityEngine;

public class SetPosition : MonoBehaviour
{
    [SerializeField] private GameObject[] fuses;
    [SerializeField] private GameObject[] positions;
    private PlayerInteraction playerInteraction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInteraction = FindAnyObjectByType<PlayerInteraction>().GetComponent<PlayerInteraction>();
    }

    public void GetFuses()
    {
        for (int i = 0; i < fuses.Length; i++)
        {
            fuses[i].transform.position = positions[i].transform.position;
        }
    }
}
