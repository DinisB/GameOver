using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SpinMinigame : MonoBehaviour
{
    [SerializeField] private Button[] slots;
    [SerializeField] private Button spin;
    [SerializeField] private Button quit;
    private Button currentSlot;
    private EnableMouse mouse;
    private bool isChanging = false;
    private bool completed = false;
    [SerializeField] private Sprite[] icons;
    private string[] current;
    [SerializeField] private Image[] slotimages;
    private PlayerInventory playerInventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>().GetComponent<PlayerInventory>();
        current = new string[slots.Length];

        foreach (Button btn in slots)
        {
            btn.onClick.AddListener(() => SelectSlot(btn));
        }

        spin.onClick.AddListener(() => SpinSlot());

        quit.onClick.AddListener(() => QuitMinigame());

        mouse = FindFirstObjectByType<EnableMouse>().GetComponent<EnableMouse>();
    }
    void SpinSlot()
    {
        if (!isChanging) { StartCoroutine(ChangeSlot(currentSlot)); }
    }

    void SelectSlot(Button btn)
    {
        currentSlot = btn;
    }

    IEnumerator ChangeSlot(Button btn)
    {
        int counter = 0;
        isChanging = true;

        int index = btn.transform.GetSiblingIndex();
        string selected = null;
        Image icon = slotimages[index];

        List<PossibleSlots> list = new List<PossibleSlots>
    {
        PossibleSlots.Banana,
        PossibleSlots.Apple,
        PossibleSlots.Orange,
        PossibleSlots.Cherry
    };

        while (counter != 5)
        {
            list.RemoveAll(s => s.ToString() == current[index]);

            PossibleSlots randomSlot = list[Random.Range(0, list.Count)];
            if (selected == null)
            {
                selected = randomSlot.ToString();
            }

            icon.sprite = icons[(int)randomSlot];
            counter++;
            yield return new WaitForSeconds(.1f);
        }

        icon.sprite = icons[(int)System.Enum.Parse(typeof(PossibleSlots), selected)];
        current[index] = selected;

        isChanging = false;
        StartCoroutine(CheckSlots());
    }


    IEnumerator CheckSlots()
    {
        int count = 0;
        string[] code = File.ReadAllLines(Application.streamingAssetsPath + "/code.txt");

        for (int i = 0; i < slots.Length; i++)
        {
            if (current[i] == code[i])
            {
                count++;
            }
        }

        if (count == 3 && !completed)
        {
            completed = true;

            foreach (Button btn in slots)
            {
                btn.image.color = Color.green;
            }

            yield return new WaitForSeconds(1);
            playerInventory.AddCoin();
            FindFirstObjectByType<UIManager>().RefreshCoins();
            mouse.ChangeMouse(true, false);
        }
    }


    void QuitMinigame()
    {
        gameObject.SetActive(false);
        mouse.ChangeMouse(true, false);
    }

    public enum PossibleSlots
    {
        Banana,
        Apple,
        Orange,
        Cherry
    }
}
