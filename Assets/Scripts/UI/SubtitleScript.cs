using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleScript : MonoBehaviour
{
    private TMP_Text subtitleText;
    private Coroutine clearCoroutine;
    private const string Nothing = "";
    private float duration;

    void Start()
    {
        subtitleText = GetComponent<TMP_Text>();
        subtitleText.text = Nothing;
    }

    public void SetAudio(AudioClip audioClip)
    {
        this.duration = audioClip.length;
    }

    public void SetText(string text)
    {
        subtitleText.text = text;

        if (clearCoroutine != null)
            StopCoroutine(clearCoroutine);

        clearCoroutine = StartCoroutine(ClearTextAfterDuration(duration));
    }

    IEnumerator ClearTextAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        subtitleText.text = Nothing;
        clearCoroutine = null;
    }
}
