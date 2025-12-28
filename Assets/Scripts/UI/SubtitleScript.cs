using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleScript : MonoBehaviour
{
    private TMP_Text subtitleText;
    private string nothing = " ";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        subtitleText = GetComponent<TMP_Text>();
        subtitleText.text = nothing;
    }

    // Update is called once per frame
    public void SetText(float duration, string text)
    {
        subtitleText.text = text;
        StartCoroutine(ClearTextAfterDuration(duration));
    }

    IEnumerator ClearTextAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        subtitleText.text = nothing;
    }
}
