using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UsarEspadasScript : MonoBehaviour
{
    private GameObject player; // Cambié el tipo de variable para hacer referencia al script del jugador
    private PersonajeBase personajeBase;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        personajeBase = player.GetComponent<PersonajeBase>();
    }

    public void Use()
    {
        if(personajeBase.cercaDelNpc) 
        {
            personajeBase.espadasEntregada=true;
            Destroy(gameObject);
        }
        
    }
}