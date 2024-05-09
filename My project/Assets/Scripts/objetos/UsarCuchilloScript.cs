using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsarCuchilloScript : MonoBehaviour
{ // Cantidad de vida a recuperar al usar la poción
    private GameObject player;
    private PersonajeBase personajeScript; // Cambié el tipo de variable para hacer referencia al script del jugador

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        personajeScript = player.GetComponent<PersonajeBase>();
    }

    public void Use()
    {

        if (!personajeScript.isDead && personajeScript.cooldownTimer >= personajeScript.cooldownDistancia)
            {
                personajeScript.animator.SetTrigger("ataquedistancia");
                personajeScript.cooldownTimer = 0f;
            }
    }
}
