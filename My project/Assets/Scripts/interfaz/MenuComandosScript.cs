using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuComandosScript : MonoBehaviour
{
    [SerializeField] public GameObject objetoARecoger;
    [SerializeField] public GameObject InputComandos;
    private TMP_InputField inputField;
    private Inventario inventario;
    private GameObject personaje;

    private void Start()
    {
        inputField = InputComandos.GetComponent<TMP_InputField>();
        if (inputField == null)
        {
            Debug.LogError("No se encontró el TMP_InputField en el hijo InputComandos.");
            return;
        }
         // Obtener referencia al script Inventario del personaje
        personaje = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("personaje detectado");
        if (personaje != null)
        {
            inventario = personaje.GetComponent<Inventario>();
            Debug.Log("inventario detectado");
        }
        else
        {
            Debug.LogError("No se encontró el objeto del personaje.");
        }
        OcultarMenuComandos();
        
    }

    public void MostrarMenuComandos()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 2f);
        InputComandos.SetActive(true);
        // Limpiar y activar el InputField
        inputField.text = "";
        inputField.ActivateInputField();
        Time.timeScale = 0f;
    }

    public void OcultarMenuComandos()
    {
        transform.localScale = Vector3.zero;
        InputComandos.SetActive(false);
        // Desactivar el InputField
        inputField.DeactivateInputField();
        Time.timeScale = 1f;
    }

    public void SetInputField(string inputText)
    {
        Debug.Log("Comando introducido: " + inputText);
        string[] commandParts = inputText.Split(' ');

        if (commandParts.Length == 3 && commandParts[0] == "git" && commandParts[1] == "add")
        {
            string objeto = commandParts[2].ToLower();
            Debug.Log("intentando pillar objeto");
            RecogerObjeto(objeto);
        }
        else if (inputText == "git commit")
        {
            Debug.Log("Guardando situación de la escena...");
            GuardarEscena();
        }
        else if (inputText == "git restore")
        {
            Debug.Log("Cargando situación de la escena...");
            CargarEscena();
        }
        else
        {
            Debug.Log("Comando no reconocido: " + inputText);
        }
        inputField.text = "";
    }

    private void RecogerObjeto(string objeto)
    {
        Debug.Log("Intentando recoger objeto: " + objeto);
        // Verificar si el jugador está sobre el objeto y el nombre coincide
        if (objetoARecoger != null && objetoARecoger.tag == objeto)
        {
             
            GameObject objetoPrefab = ObtenerPrefab(objetoARecoger.tag);
            if (inventario != null && inventario.AgregarObjeto(objetoPrefab))
            {
                // Objeto agregado al inventario con éxito
                Debug.Log("Objeto recogido con éxito: " + objeto);
                Destroy(objetoARecoger); // Destruir el objeto del juego después de agregarlo al inventario
                objetoARecoger = null; // Limpiar el objeto recogido
            }
            else
            {
                Debug.Log("El inventario está lleno, no se puede recoger el objeto.");
            }
        }
        else
        {
            Debug.Log("Nombre incorrecto u objeto no disponible.");
        }
    }

    public void SetRecogerObjeto(GameObject objeto)
    {
        objetoARecoger = objeto; // Almacenar el objeto que se va a recoger
        Debug.Log("Objeto a recoger asignado: " + objeto.name);
    }

    private GameObject ObtenerPrefab(string tagObjeto)
    {
        // Construir la ruta relativa al prefab correspondiente al tag del objeto con el sufijo "boton"
        string rutaPrefab = "Objetos/" + tagObjeto + "boton";
        GameObject prefab = Resources.Load<GameObject>(rutaPrefab); // Buscar en la carpeta Resources

        // Si se encontró el prefab, devolverlo
        if (prefab != null)
        {
            return prefab;
        }
        else
        {
            Debug.LogWarning("No se encontró el prefab para el objeto con el tag: " + tagObjeto);
            return null;
        }
    }
    private void GuardarEscena()
    {
        // Obtener el nombre de la escena actual
        string nombreEscenaActual = SceneManager.GetActiveScene().name;

        // Guardar el nombre de la escena actual en PlayerPrefs
        PlayerPrefs.SetString("UltimaEscenaGuardada", nombreEscenaActual);

        Debug.Log("Escena actual guardada: " + nombreEscenaActual);

    }
    private void CargarEscena()
    {
        // Obtener el nombre de la última escena guardada desde PlayerPrefs
        string nombreUltimaEscenaGuardada = PlayerPrefs.GetString("UltimaEscenaGuardada", "");

        if (!string.IsNullOrEmpty(nombreUltimaEscenaGuardada))
        {
            // Cargar la última escena guardada
            SceneManager.LoadScene(nombreUltimaEscenaGuardada);

            Debug.Log("Cargando última escena guardada: " + nombreUltimaEscenaGuardada);
        }
        else
        {
            Debug.LogWarning("No se encontró una última escena guardada.");
        }
    }
}