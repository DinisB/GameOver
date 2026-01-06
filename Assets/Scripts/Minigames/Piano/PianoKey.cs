using UnityEngine;
using System.Collections;

public class PianoKey : MonoBehaviour
{
    public int keyID; // 0 a 4

    [Header("Visual")]
    public MeshRenderer meshRenderer;
    public Material normalMaterial; // non-emissive
    public Material emissiveMaterial; // emissive
    public float glowTime = 0.25f; //Tempo de brilho ao ser pressionado

    [Header("Audio")]
    public AudioSource audioSource;

    public void Play()
    {
        audioSource.Play();
        StartCoroutine(Glow());
    }
    // Faz o piano key brilhar por um tempo definido
    private IEnumerator Glow()
    {
        meshRenderer.material = emissiveMaterial;
        yield return new WaitForSeconds(glowTime);
        meshRenderer.material = normalMaterial;
    }
}
