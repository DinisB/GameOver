using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private GameObject shopUI;
    private PlayerInventory playerInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>().GetComponent<PlayerInventory>();
    }

    public void BuyItem()
    {
        if (playerInventory.GetCoins() > 0)
        {
            playerInventory.SpendCoin();
            FindFirstObjectByType<UIManager>().RefreshCoins();
            objectToActivate.SetActive(true);
            gameObject.SetActive(false);
            shopUI.SetActive(false);
            FindFirstObjectByType<EnableMouse>().DisableMouse();
        }
    }
}
