using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorScript : MonoBehaviour
{
    private MenuComandosScript menuComandos;

    public bool juegoPausado = false;
    private void Start(){
        menuComandos = GameObject.FindGameObjectWithTag("Consola").GetComponent<MenuComandosScript>();
    }

    private void Update()
    {
        // Detección de tecla para mostrar/ocultar el cartel y pausar/resumir el juego
        if (Input.GetKeyDown(KeyCode.BackQuote)) 
        {
            if (juegoPausado)
            {
                ResumirJuego();
            }
            else
            {
                PausarJuego();
            }
        }
        
    }

    private void PausarJuego()
    {
        Time.timeScale = 0f;
        juegoPausado = true;
        menuComandos.MostrarMenuComandos(); // Mostrar el cartel al pausar el juego
    }

    private void ResumirJuego()
    {
        Time.timeScale = 1f;
        juegoPausado = false;
        menuComandos.OcultarMenuComandos(); // Ocultar el cartel al resumir el juego
    }
    public void cambiarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}