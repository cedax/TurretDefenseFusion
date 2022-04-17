using UnityEngine;

public class AudioTorreta : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private float volumen;

    private void Start() {
        volumen = 0.25f;
        audioSource.volume = volumen;
    }

    public void Disparo(){
        audioSource.volume = volumen;
        audioSource.Play();
    }
}