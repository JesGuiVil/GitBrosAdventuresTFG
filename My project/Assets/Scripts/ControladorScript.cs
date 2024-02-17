using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorScript : MonoBehaviour
{
    public GameObject menuComandos;

    private bool juegoPausado = false;

    private void Update()
    {
        // Detección de tecla para mostrar/ocultar el cartel y pausar/resumir el juego
        if (Input.GetKeyDown(KeyCode.BackQuote)) // Utilizamos la tecla de coma ','
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
        if (!juegoPausado && Input.GetKeyDown(KeyCode.Comma))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex == 0)
            {
                cambiarEscena(1);
            }
            else if (currentSceneIndex == 1)
            {
                cambiarEscena(0);
            }
        }
    }

    private void PausarJuego()
    {
        Time.timeScale = 0f;
        juegoPausado = true;
        menuComandos.GetComponent<MenuComandosScript>().MostrarMenuComandos(); // Mostrar el cartel al pausar el juego
    }

    private void ResumirJuego()
    {
        Time.timeScale = 1f;
        juegoPausado = false;
        menuComandos.GetComponent<MenuComandosScript>().OcultarMenuComandos(); // Ocultar el cartel al resumir el juego
    }
    public void cambiarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}