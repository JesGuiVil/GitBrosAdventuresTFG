using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorScript : MonoBehaviour
{
    private MenuComandosScript menuComandos;
    private PersonajeBase personajeBase;
    private int contadorPausas = 0;
    public bool juegoPausado => contadorPausas > 0;

    private void Start()
    {
        menuComandos = GameObject.FindGameObjectWithTag("Consola").GetComponent<MenuComandosScript>();
    }

    private void Update()
    {
        // Detección de tecla para mostrar/ocultar el cartel y pausar/resumir el juego
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (menuComandos.EstaMostrando())
            {
                menuComandos.OcultarMenuComandos();
                DespausarJuego();
            }
            else
            {
                PausarJuego();
                menuComandos.MostrarMenuComandos();
            }
        }
    }

    public void PausarJuego()
    {
        contadorPausas++;
        if (contadorPausas == 1)
        {
            Time.timeScale = 0;
            Debug.Log("Juego pausado");
        }
    }

    public void DespausarJuego()
    {
        if (contadorPausas > 0)
        {
            contadorPausas--;
            if (contadorPausas == 0)
            {
                Time.timeScale = 1;
                Debug.Log("Juego despausado");
            }
        }
    }

    public void cambiarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}
