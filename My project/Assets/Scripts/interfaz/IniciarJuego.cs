using UnityEngine;
using UnityEngine.SceneManagement;

public class IniciarJuego : MonoBehaviour
{
    // Nombre de la escena a cargar

    void Update()
    {
        // Verificar si se presiona la tecla "E"
        if (Input.GetKeyDown(KeyCode.E) && SceneManager.GetActiveScene().name == "EscenaInicial" )
        {
            // Cargar la escena especificada
            SceneManager.LoadScene("EscenaRogue1");
        }
        else if (Input.GetKeyDown(KeyCode.E) && SceneManager.GetActiveScene().name == "EscenaFinal" )
        {
            // Cargar la escena especificada
            SceneManager.LoadScene("MenuInicial");
        }
        
    }
}