using System.Collections.Generic;
using UnityEngine;

public class AudioDistanceCulling : MonoBehaviour
{
    public float maxDistance = 10f;  // La distancia máxima a la que se escucharán los sonidos
    private GameObject player;
    private List<AudioSource> audioSources = new List<AudioSource>();

    void Start()
    {
        // Encuentra el objeto con la etiqueta "Player"
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No se encontró un GameObject con la etiqueta 'Player' en la escena.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource != null) // Asegúrate de que el AudioSource no sea nulo
                {
                    // Calcula la distancia entre el player y el objeto con el AudioSource
                    float distance = Vector3.Distance(player.transform.position, audioSource.transform.position);

                    // Habilita o deshabilita el AudioSource basado en la distancia
                    if (distance <= maxDistance)
                    {
                        if (!audioSource.isPlaying)
                        {
                            audioSource.Play();
                        }
                    }
                    else
                    {
                        if (audioSource.isPlaying)
                        {
                            audioSource.Stop();
                        }
                    }
                }
            }
        }
    }

    public void RegisterAudioSource(AudioSource audioSource)
    {
        if (!audioSources.Contains(audioSource))
        {
            audioSources.Add(audioSource);
        }
    }

    public void UnregisterAudioSource(AudioSource audioSource)
    {
        if (audioSources.Contains(audioSource))
        {
            audioSources.Remove(audioSource);
        }
    }
}

