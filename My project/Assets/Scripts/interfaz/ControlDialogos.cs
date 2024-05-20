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

    private Assassin assassin;

    private Archer archer;

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

    public void ActivarCartel(Textos textoObjeto)
    {
        animDialogos.SetBool("mostrar", true);
        texto = textoObjeto;
    }

    public void ActivaTexto()
    {
        colaDialogos.Clear();
        Time.timeScale = 0f;

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
            cierraCartel();
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
                // por hacer cambio de escena al final del nivel

            }
            

            return;
        }
        string fraseActual = colaDialogos.Dequeue();
        textoPantalla.text = fraseActual;
    }

    public void cierraCartel()
    {
        animDialogos.SetBool("mostrar", false);
        textoPantalla.text = "";
        Time.timeScale = 1f;
    }
}
