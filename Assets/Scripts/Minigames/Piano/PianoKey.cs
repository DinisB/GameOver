using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public int keyID; // 0 a 4
    public Light keyLight; // Luz que acende quando a tecla é pressionada
    public AudioSource audioSource; // Serve para tocar o som da tecla

    public void Play()
    {
        audioSource.Play();
        StartCoroutine(LightFlash());
    }

    private System.Collections.IEnumerator LightFlash()
    {
        // Acende a luz por 0.25 segundos
        keyLight.enabled = true;
        yield return new WaitForSeconds(0.25f);
        keyLight.enabled = false;
    }
}
