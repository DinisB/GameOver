using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public float fadeTime = 1f;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
    }

    // Dá Triggered quando o player entra na area
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(FadeIn());
    }
    // Dá Triggered quando o player sai da area
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(FadeOut());
    }
    // Fade in da musica
    IEnumerator FadeIn()
    {
        audioSource.Play();
        while (audioSource.volume < 0.15f)
        {
            audioSource.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
    }
    // Fade out da musica
    IEnumerator FadeOut()
    {
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= Time.deltaTime / fadeTime;
            yield return null;
        }
        audioSource.Stop();
    }
}