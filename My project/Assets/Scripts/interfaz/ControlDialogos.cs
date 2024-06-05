using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlDialogos : MonoBehaviour
{
    private Animator animDialogos;
    private Queue<string> colaDialogos;
    private Textos texto;
    [SerializeField] private TextMeshProUGUI textoPantalla;

    // Instancia Singleton de ControlDialogos
    private static ControlDialogos instance;
    private Rogue rogue; // Person
    private AudioSource audioSourceDialogos;
    [SerializeField] private AudioClip abrir;
    [SerializeField] private AudioClip cerrar;
    private Assassin assassin;
    private GameObject main;
    private Archer archer;
    private bool mostrandoTextos3 = false;
    private bool puedeMostrarSiguiente = true;
    private ControladorScript controladorScript;
    private bool seHaGirado=false;
    public bool mostrandoCartel = false;

    // Propiedad estática para acceder a la instancia Singleton
    public static ControlDialogos Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ControlDialogos>();
                if (instance == null)
                {
                    Debug.LogError("No se encontró una instancia de ControlDialogos en la escena.");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Asegúrate de que solo haya una instancia de ControlDialogos en la escena
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Para que el objeto persista al cargar nuevas escenas
        animDialogos = GetComponent<Animator>();
        colaDialogos = new Queue<string>();
        audioSourceDialogos = GetComponent<AudioSource>();
        controladorScript = GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorScript>();

        if (SceneManager.GetActiveScene().name == "EscenaRogue1")
        {
            rogue = GameObject.FindGameObjectWithTag("Player").GetComponent<Rogue>();
        }
        if (SceneManager.GetActiveScene().name == "EscenaAssassin1")
        {
            assassin = GameObject.FindGameObjectWithTag("Player").GetComponent<Assassin>();
            main = GameObject.FindGameObjectWithTag("Npc");
        }
        if (SceneManager.GetActiveScene().name == "EscenaArcher1")
        {
            archer = GameObject.FindGameObjectWithTag("Player").GetComponent<Archer>();
        }
    }

    public void ActivarCartel(Textos textoObjeto)
    {
        mostrandoCartel = true;
        audioSourceDialogos.PlayOneShot(abrir);
        animDialogos.SetBool("mostrar", true);

        texto = textoObjeto;
        if (textoObjeto.esTextos3)
        {
            mostrandoTextos3 = true;
        }
        else
        {
            mostrandoTextos3 = false;
        }
        
    }

    public void ActivaTexto()
    {
        colaDialogos.Clear();
        controladorScript.PausarJuego();

        // Comprobación de nulidad para texto
        if (texto != null)
        {
            // Comprobación de nulidad para texto.arrayTextos
            if (texto.arrayTextos != null)
            {
                foreach (string textoGuardar in texto.arrayTextos)
                {
                    colaDialogos.Enqueue(textoGuardar);
                }
                SiguienteFrase();
            }
        }
    }

    public void SiguienteFrase()
    {
        if (colaDialogos.Count == 0)
        {
            CierraCartel();
            audioSourceDialogos.PlayOneShot(cerrar);
            if (SceneManager.GetActiveScene().name == "EscenaRogue1")
            {
                if (rogue.espadasEntregada)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            if (SceneManager.GetActiveScene().name == "EscenaAssassin1")
            {
                if (assassin.cosaEntregada)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            if (SceneManager.GetActiveScene().name == "EscenaArcher1")
            {
                if (archer.algoEntregada)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            return;
        }
        string fraseActual = colaDialogos.Dequeue();
        textoPantalla.text = fraseActual;
        StartCoroutine(EsperarAntesDePermitirSiguiente());
        // Ejecutar la acción después de mostrar la primera frase de textos3
        if (mostrandoTextos3 && colaDialogos.Count == 0 && !seHaGirado)
        {
            main.GetComponent<SpriteRenderer>().flipX = !main.GetComponent<SpriteRenderer>().flipX;
            seHaGirado=true;
        }   
    }

    public void CierraCartel()
    {
        animDialogos.SetBool("mostrar", false);
        textoPantalla.text = "";
        controladorScript.DespausarJuego();
        mostrandoCartel = false;
    }

    private IEnumerator EsperarAntesDePermitirSiguiente()
    {
        puedeMostrarSiguiente = false;
        yield return new WaitForSecondsRealtime(2f);
        puedeMostrarSiguiente = true;
    }

    public bool PuedeMostrarSiguiente()
    {
        return puedeMostrarSiguiente;
    }
}