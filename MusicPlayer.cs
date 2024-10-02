using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip musicClip; // La pista de m�sica que deseas reproducir
    private AudioSource audioSource;
    public GameObject go;
    public AudioClip clickAnyButtonClip;

    void Awake()
    {
        // Aseg�rate de que este GameObject persista a trav�s de las escenas
        DontDestroyOnLoad(go);

        // Obt�n o agrega un componente AudioSource si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Asigna la pista de m�sica al AudioSource y configura para que se reproduzca en bucle
        audioSource.clip = musicClip;
        audioSource.loop = true;

        // Reproduce la m�sica
        audioSource.Play();
    }
}
