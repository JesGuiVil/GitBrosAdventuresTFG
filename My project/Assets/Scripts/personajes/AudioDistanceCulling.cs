using System.Collections.Generic;
using UnityEngine;

public class AudioDistanceCulling : MonoBehaviour
{
    [SerializeField] public float maxDistance;  // La distancia máxima a la que se escucharán los sonidos
    private float maxDistanceSquared; // Distancia máxima al cuadrado
    private GameObject player;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private ControladorScript controlador;

    void Start()
    {
        // Encuentra el objeto con la etiqueta "Player"
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No se encontró un GameObject con la etiqueta 'Player' en la escena.");
            enabled = false;
            return;
        }

        // Encuentra el objeto que contiene el ControladorScript
        controlador = FindObjectOfType<ControladorScript>();
        if (controlador == null)
        {
            Debug.LogError("No se encontró un objeto con ControladorScript en la escena.");
            enabled = false;
            return;
        }

        maxDistanceSquared = maxDistance * maxDistance;
    }

    void Update()
    {
        if (controlador.juegoPausado)
        {
            StopAllAudio();
        }
        else
        {
            UpdateAudioSources();
        }
    }

    void StopAllAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    void UpdateAudioSources()
    {
        if (player != null)
        {
            Vector2 playerPosition = player.transform.position;  // Obtener la posición del jugador en 2D
            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource != null)
                {
                    // Obtener la posición del GameObject que contiene el AudioSource en 2D
                    Vector2 audioSourcePosition = audioSource.gameObject.transform.position;  
                    // Calcula la distancia al cuadrado entre el player y el objeto con el AudioSource (solo X e Y)
                    float distanceSquared = (playerPosition - audioSourcePosition).sqrMagnitude;

                    // Habilita o deshabilita el AudioSource basado en la distancia
                    if (distanceSquared <= maxDistanceSquared)
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
        if (audioSource != null && !audioSources.Contains(audioSource))
        {
            audioSources.Add(audioSource);
            Debug.Log("Registered Audio Source: " + audioSource.name);
        }
    }

    public void UnregisterAudioSource(AudioSource audioSource)
    {
        if (audioSource != null && audioSources.Contains(audioSource))
        {
            audioSources.Remove(audioSource);
            Debug.Log("Unregistered Audio Source: " + audioSource.name);
        }
    }
}