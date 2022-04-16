using UnityEngine;

public class AudioTorreta : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] [Range(0.00f, 1f)] private float volumen;

    private void Start() {
        volumen = 0.10f;
        audioSource.volume = volumen;
    }

    public void Disparo(){
        audioSource.volume = volumen;
        audioSource.Play();
    }
}