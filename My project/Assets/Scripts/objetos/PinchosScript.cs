using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pinchos : MonoBehaviour
{
    private PersonajeBase personajeScript;
    [SerializeField] private float danio;

    private void Start()
    {
        // Busca el GameObject del personaje por su etiqueta
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            // Obtén el componente PersonajeBase del GameObject del personaje
            personajeScript = player.GetComponent<PersonajeBase>();
        }
        else
        {
            Debug.LogError("No se pudo encontrar el GameObject del personaje con la etiqueta 'Player'.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Asegúrate de que el personajeScript no sea nulo y no esté muerto
            if (personajeScript != null && !personajeScript.isDead)
            {
                // Llama al método RecibirDanio del personaje para infligir daño
                personajeScript.RecibirDanio(danio);
            }
        }
    }
}
