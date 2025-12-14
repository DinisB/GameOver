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
    [SerializeField] private GameObject key;
    private Button currentSlot;
    private EnableMouse mouse;
    private bool isChanging = false;
    private bool completed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Button btn in slots)
        {
            btn.onClick.AddListener(() => SelectSlot(btn));
        }

        spin.onClick.AddListener(() => SpinSlot());

        quit.onClick.AddListener(() => QuitMinigame());

        mouse = FindFirstObjectByType<EnableMouse>().GetComponent<EnableMouse>();
    }

    // Update is called once per frame
    void Update()
    {

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
        TMP_Text txt = btn.GetComponentInChildren<TMP_Text>();
        int counter = 0;
        isChanging = true;
        string selected = null;
        while (counter != 5)
        {

            List<PossibleSlots> list = new List<PossibleSlots>
    {
        PossibleSlots.Banana,
        PossibleSlots.Apple,
        PossibleSlots.Orange,
        PossibleSlots.Cherry
    };

            list.RemoveAll(s => s.ToString() == txt.text);

            PossibleSlots randomSlot = list[Random.Range(0, list.Count)];
            if (selected == null) selected = randomSlot.ToString();
            txt.text = randomSlot.ToString();
            counter++;
            yield return new WaitForSeconds(.1f);
        }
        txt.text = selected;
        isChanging = false;
        StartCoroutine(CheckSlots());
    }

    IEnumerator CheckSlots()
    {
        int count = 0;
        string[] code = File.ReadAllLines(Application.streamingAssetsPath + '/' + "code.txt");
        Debug.Log(code);
        int num = 0;
        foreach (Button btn in slots)
        {
            string slotText = btn.GetComponentInChildren<TMP_Text>().text;
            if (slotText == code[num])
            {
                count++;
            }
            num++;
        }

        if (count == 3 && !completed)
        {
            Debug.Log("Sucess");
            completed = true;
            foreach (Button btn in slots)
            {
                btn.image.color = Color.green;
            }
            yield return new WaitForSeconds(1);
            key.SetActive(true);
            gameObject.SetActive(false);
            mouse.ChangeMouse(true, false);
        }
        else { Debug.Log("Wrong"); }
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
