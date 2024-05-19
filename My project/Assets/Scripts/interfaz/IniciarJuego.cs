using UnityEngine;
using UnityEngine.SceneManagement;

public class IniciarJuego : MonoBehaviour
{
    // Nombre de la escena a cargar

    void Update()
    {
        // Verificar si se presiona la tecla "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Cargar la escena especificada
            SceneManager.LoadScene("EscenaRogue1");
        }
    }
}