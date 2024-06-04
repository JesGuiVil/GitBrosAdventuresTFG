using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UsarLlaveScript : MonoBehaviour
{
    private GameObject player; // Cambié el tipo de variable para hacer referencia al script del jugador
    private PersonajeBase personajeBase;
    private CofreScript cofreScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No se encontró ningún objeto con el tag 'Player'");
            return;
        }

        personajeBase = player.GetComponent<PersonajeBase>();
        if (personajeBase == null)
        {
            Debug.LogError("El objeto 'Player' no tiene el componente 'PersonajeBase'");
            return;
        }
        cofreScript= GameObject.FindGameObjectWithTag("Cofre").GetComponent<CofreScript>();
        if (cofreScript == null)
        {
            Debug.LogError("El objeto 'Cofre' no tiene el componente 'CofreScript'");
        }
    }

    public void Use()
    {
        if(personajeBase != null && personajeBase.cercaDelCofre) 
        {
            if (cofreScript != null)
            {
                cofreScript.ActivateCofre();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("El componente 'CofreScript' no está asignado correctamente.");
            }
        }
        else
        {
            Debug.LogWarning("El personaje no está cerca del cofre o 'personajeBase' no está asignado.");            
        }
    }
}