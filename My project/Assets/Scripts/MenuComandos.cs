using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuComandos : MonoBehaviour
{
    //Atributos
    [SerializeField] private GameObject comandosMenu;
    private GameObject personajeActual;
    
    public bool isPaused;
    public InputField inputField;

    public GameObject Rogue;
    public GameObject Archer;
    public GameObject Assassin;

    void Start()
    {
        comandosMenu.SetActive(false);

        // Asegurarse de que no haya un personaje instanciado al inicio
        if (personajeActual != null)
        {
            Destroy(personajeActual);
        }

        // Instanciar el personaje inicial (puedes cambiar esto según tus necesidades)
        InstanciarPersonaje(Rogue);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ReanudarJuego();
            } else {
                PausarJuego();
            }
        }
    }

    public void PausarJuego() 
    {
        Debug.Log("Pausar Juego");

        // Asegurarse de que no haya un personaje instanciado al pausar
        if (personajeActual != null)
        {
            Destroy(personajeActual);
        }

        comandosMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ReanudarJuego() 
    {
        Debug.Log("Reanudar Juego");
        comandosMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // Asegurarse de que no haya un personaje instanciado al reanudar
        if (personajeActual != null)
        {
            Destroy(personajeActual);
        }
    }

    public void SetInputField(string inputText) 
    {
        string[] commandParts = inputText.Split(' ');

        if (commandParts.Length == 3 && commandParts[0] == "git" && commandParts[1] == "checkout") 
        {
            CambiarPersonaje(commandParts[2].ToLower());
        } 
        else 
        {
            Debug.Log("Comando no reconocido: " + inputText);
        }
    }

    private void CambiarPersonaje(string nuevoPersonaje) 
    {
        // Verificar si el personaje actual no es nulo antes de destruirlo
        if (personajeActual != null)
        {
            Destroy(personajeActual);
        }

        // Instanciar el nuevo personaje según el comando
        switch (nuevoPersonaje) 
        {
            case "rogue":
                InstanciarPersonaje(Rogue);
                break;
            case "assassin":
                InstanciarPersonaje(Assassin);
                break;
            case "archer":
                InstanciarPersonaje(Archer);
                break;
            default:
                Debug.Log("Personaje no reconocido: " + nuevoPersonaje);
                break;
        }
    }

    private void InstanciarPersonaje(GameObject prefab) 
    {
        // Instanciar el nuevo personaje y asignarlo a la variable personajeActual
        personajeActual = Instantiate(prefab, transform.position, Quaternion.identity);
    }
}