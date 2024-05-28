using UnityEngine;

public class RegisterAudioSource : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioDistanceCulling audioManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            // Encuentra el objeto que controla el culling de audio y registra el AudioSource
            audioManager = FindObjectOfType<AudioDistanceCulling>();
            if (audioManager != null)
            {
                audioManager.RegisterAudioSource(audioSource);
            }
        }
    }

    void OnDestroy()
    {
        if (audioSource != null && audioManager != null)
        {
            audioManager.UnregisterAudioSource(audioSource);
        }
    }
}
