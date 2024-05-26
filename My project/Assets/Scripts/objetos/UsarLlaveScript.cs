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
        personajeBase = player.GetComponent<PersonajeBase>();
        cofreScript= GameObject.FindGameObjectWithTag("Cofre").GetComponent<CofreScript>();
    }

    public void Use()
    {
        if(personajeBase.cercaDelCofre) 
        {
            cofreScript.ActivateCofre();
            Destroy(gameObject);
            
        }
        
    }
}