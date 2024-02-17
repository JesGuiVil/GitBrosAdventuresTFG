using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecogerObjeto : MonoBehaviour
{
    private MenuComandosScript menuComandos;
    
    private void Start()
    {
        menuComandos = FindObjectOfType<MenuComandosScript>();
        
        // Mensaje de depuración para verificar si menuComandos se configuró correctamente
        if(menuComandos == null)
        {
            Debug.Log("No se encontró el objeto MenuComandos en la escena.");
        }
        else
        {
            Debug.Log("Se encontró el objeto MenuComandos en la escena.");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Mostrar mensaje para pedir al jugador que introduzca el comando
            Debug.Log("Introduce 'git add nombreobjeto' en la consola para recoger el objeto.");
            menuComandos.SetRecogerObjeto(gameObject); // Enviar la señal al script MenuComandos
        }
    }
}