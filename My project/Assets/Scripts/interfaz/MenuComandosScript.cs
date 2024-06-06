using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class MenuComandosScript : MonoBehaviour
{
    [SerializeField] public GameObject objetoARecoger;
    [SerializeField] public GameObject InputComandos;
    private TMP_InputField inputField;
    private Inventario inventario;
    private GameObject personaje;
    private EntregarObjetoScript entregarObjetoScript;
    private Assassin assassin;
    private PersonajeBase personajeBase;
    public GameObject espadasboton;
    private Rogue rogue; // Person
    private AudioSource audioSourceComandos;
    [SerializeField] public AudioClip coger;
    private Archer archer;
    private bool mostrandoMenu = false;
    private ControladorScript controladorScript;

    private void Start()
    {
        audioSourceComandos = gameObject.GetComponent<AudioSource>();
        inputField = InputComandos.GetComponent<TMP_InputField>();
        if (inputField == null)
        {
            Debug.LogError("No se encontró el TMP_InputField en el hijo InputComandos.");
            return;
        }
        
        personaje = GameObject.FindGameObjectWithTag("Player");
        personajeBase = personaje.GetComponent<PersonajeBase>();
        controladorScript = GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorScript>();
        
        if (personaje != null)
        {
            inventario = personaje.GetComponent<Inventario>();
        }
        else
        {
            Debug.LogError("No se encontró el objeto del personaje.");
        }

        OcultarMenuComandos();

        if (SceneManager.GetActiveScene().name == "EscenaRogue1")
        {
            rogue = GameObject.FindGameObjectWithTag("Player").GetComponent<Rogue>();
        }
        if (SceneManager.GetActiveScene().name == "EscenaAssassin1")
        {
            assassin = GameObject.FindGameObjectWithTag("Player").GetComponent<Assassin>();
        }
        if (SceneManager.GetActiveScene().name == "EscenaArcher1")
        {
            archer = GameObject.FindGameObjectWithTag("Player").GetComponent<Archer>();
        }
    }

    public void MostrarMenuComandos()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 2f);
        InputComandos.SetActive(true);
        inputField.text = "";
        inputField.ActivateInputField();
        mostrandoMenu = true;
    }

    public void OcultarMenuComandos()
    {
        transform.localScale = Vector3.zero;
        InputComandos.SetActive(false);
        inputField.DeactivateInputField();
        mostrandoMenu = false;
    }

    public bool EstaMostrando()
    {
        return mostrandoMenu;
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
        else if (inputText == "git merge")
        {
            Debug.Log("Intentando realizar git merge...");
            if (SceneManager.GetActiveScene().name == "EscenaRogue1")
            {
                if (rogue.tieneEspadas && personajeBase.cercaDelNpc && rogue.yaHeHablado && rogue.pedidas)
                {
                    UsarEspadas();
                    rogue.espadasEntregada = true;
                }
                else
                {
                    Debug.Log("No se puede realizar git merge");
                }
            }
            if (SceneManager.GetActiveScene().name == "EscenaAssassin1")
            {
                if (assassin.tengoBaston && personajeBase.cercaDelNpc && assassin.heAblado && assassin.pedido)
                {
                    UsarBaston();
                    assassin.cosaEntregada = true;
                }
                else
                {
                    Debug.Log("No se puede realizar git merge");
                }
            }
            if (SceneManager.GetActiveScene().name == "EscenaArcher1")
            {
                if (archer.tengoAlgo && personajeBase.cercaDelNpc && archer.Ablado && archer.yapedido)
                {
                    UsarLibro();
                    archer.algoEntregada = true;
                }
                else
                {
                    Debug.Log("No se puede realizar git merge");
                }
            }
        }
        else if (inputText == "git pull")
        {
            Debug.Log("Intentando realizar git pull...");
            if (SceneManager.GetActiveScene().name == "EscenaAssassin1")
            {
                assassin = GameObject.FindGameObjectWithTag("Player").GetComponent<Assassin>();
                if (!assassin.tengoEspadas && personajeBase.cercaDelNpc && assassin.heAblado)
                {
                    inventario.AgregarObjeto(espadasboton);
                    assassin.tengoEspadas = true;
                }
            }
        }
        else
        {
            Debug.Log("Comando no reconocido: " + inputText);
        }
        inputField.text = "";
    }
    public void SetRecogerObjeto(GameObject objeto)
    {
        objetoARecoger = objeto;
        Debug.Log("Objeto a recoger asignado: " + objeto.name);
    }
    private GameObject ObtenerPrefab(string tagObjeto)
    {
        string rutaPrefab = "Objetos/" + tagObjeto + "boton";
        GameObject prefab = Resources.Load<GameObject>(rutaPrefab);
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
    private void RecogerObjeto(string objeto)
    {
        Debug.Log("Intentando recoger objeto: " + objeto);
        if (objetoARecoger != null && objetoARecoger.tag == objeto)
        {
            GameObject objetoPrefab = ObtenerPrefab(objetoARecoger.tag);
            if (inventario != null && inventario.AgregarObjeto(objetoPrefab))
            {
                audioSourceComandos.PlayOneShot(coger);
                Debug.Log("Objeto recogido con éxito: " + objeto);
                Destroy(objetoARecoger);
                objetoARecoger = null;
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

    

    private void GuardarEscena()
    {
        string nombreEscenaActual = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("UltimaEscenaGuardada", nombreEscenaActual);
        Debug.Log("Escena actual guardada: " + nombreEscenaActual);
    }

    private void CargarEscena()
    {
        string nombreUltimaEscenaGuardada = PlayerPrefs.GetString("UltimaEscenaGuardada", "");
        if (!string.IsNullOrEmpty(nombreUltimaEscenaGuardada))
        {
            controladorScript.DespausarJuego();
            SceneManager.LoadScene(nombreUltimaEscenaGuardada);
            Debug.Log("Cargando última escena guardada: " + nombreUltimaEscenaGuardada);
        }
        else
        {
            Debug.LogWarning("No se encontró una última escena guardada.");
        }
    }

    private void UsarEspadas()
    {
        for (int i = 0; i < inventario.ranuras.Length; i++)
        {
            if (inventario.estaLleno[i])
            {
                GameObject objetoEnRanura = inventario.ranuras[i].transform.GetChild(0).gameObject;
                if (objetoEnRanura != null && objetoEnRanura.CompareTag("espadas"))
                {
                    EntregarObjetoScript entregarObjetoScript = objetoEnRanura.GetComponent<EntregarObjetoScript>();
                    if (entregarObjetoScript != null)
                    {
                        entregarObjetoScript.Use();
                        return;
                    }
                }
            }
        }
        Debug.Log("No se encontró las espadas en el inventario.");
    }
    private void UsarBaston()
    {
        for (int i = 0; i < inventario.ranuras.Length; i++)
        {
            if (inventario.estaLleno[i])
            {
                GameObject objetoEnRanura = inventario.ranuras[i].transform.GetChild(0).gameObject;
                if (objetoEnRanura != null && objetoEnRanura.CompareTag("baston"))
                {
                    EntregarObjetoScript entregarObjetoScript = objetoEnRanura.GetComponent<EntregarObjetoScript>();
                    if (entregarObjetoScript != null)
                    {
                        entregarObjetoScript.Use();
                        return;
                    }
                }
            }
        }
        Debug.Log("No se encontró el baston en el inventario.");
    }
    private void UsarLibro()
    {
        for (int i = 0; i < inventario.ranuras.Length; i++)
        {
            if (inventario.estaLleno[i])
            {
                GameObject objetoEnRanura = inventario.ranuras[i].transform.GetChild(0).gameObject;
                if (objetoEnRanura != null && objetoEnRanura.CompareTag("libro"))
                {
                    EntregarObjetoScript entregarObjetoScript = objetoEnRanura.GetComponent<EntregarObjetoScript>();
                    if (entregarObjetoScript != null)
                    {
                        entregarObjetoScript.Use();
                        return;
                    }
                }
            }
        }
        Debug.Log("No se encontró el libro en el inventario.");
    }
}
