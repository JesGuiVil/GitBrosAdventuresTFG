using UnityEngine;

public class ControladorScript : MonoBehaviour
{
    public GameObject menuComandos;

    private bool juegoPausado = false;

    private void Update()
    {
        // Detección de tecla para mostrar/ocultar el cartel y pausar/resumir el juego
        if (Input.GetKeyDown(KeyCode.P))
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
        menuComandos.GetComponent<MenuComandosScript>().MostrarMenuComandos(); // Mostrar el cartel al pausar el juego
    }

    private void ResumirJuego()
    {
        Time.timeScale = 1f;
        juegoPausado = false;
        menuComandos.GetComponent<MenuComandosScript>().OcultarMenuComandos(); // Ocultar el cartel al resumir el juego
    }
}
