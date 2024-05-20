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
    private PersonajeBase personajeBase;
    [SerializeField] private TextMeshProUGUI textoPantalla;

    // Instancia Singleton de ControlDialogos
    private static ControlDialogos instance;

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
        personajeBase = GameObject.FindGameObjectWithTag("Player").GetComponent<PersonajeBase>();
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

            if (personajeBase.espadasEntregada)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
