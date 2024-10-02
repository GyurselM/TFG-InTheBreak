using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip musicClip; // La pista de música que deseas reproducir
    private AudioSource audioSource;
    public GameObject go;
    public AudioClip clickAnyButtonClip;

    void Awake()
    {
        // Asegúrate de que este GameObject persista a través de las escenas
        DontDestroyOnLoad(go);

        // Obtén o agrega un componente AudioSource si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Asigna la pista de música al AudioSource y configura para que se reproduzca en bucle
        audioSource.clip = musicClip;
        audioSource.loop = true;

        // Reproduce la música
        audioSource.Play();
    }
}
