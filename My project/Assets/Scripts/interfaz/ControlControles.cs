using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlControles : MonoBehaviour
{
    private Animator animControles;
    private Queue<string> colaControles;
    public Textos texto;
    private ControladorScript controladorScript;
    private AudioSource audioSourceControles;
    [SerializeField] private AudioClip abrircontroles;
    [SerializeField] private AudioClip cerrarcontroles;
    [SerializeField] TextMeshProUGUI textoControles;

    void Start()
    {
        controladorScript = GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorScript>();
        animControles = gameObject.GetComponent<Animator>();
        colaControles = new Queue<string>();
        audioSourceControles = gameObject.GetComponent<AudioSource>();
    }

    public void ActivarCartelGrande()
    {
        animControles.SetBool("mostrarGrande", true);
    }

    public void ActivaTextoControles()
    {
        colaControles.Clear();

        if (texto != null && texto.arrayTextos != null)
        {
            foreach (string textoGuardar in texto.arrayTextos)
            {
                colaControles.Enqueue(textoGuardar);
            }
            SiguienteFraseControles();
        }

        controladorScript.PausarJuego();
    }

    public void SiguienteFraseControles()
    {
        if (colaControles.Count == 0)
        {
            CerrarCartelGrande();
            return;
        }

        string fraseActual = colaControles.Dequeue();
        textoControles.text = fraseActual;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (animControles.GetBool("mostrarGrande"))
            {
                audioSourceControles.PlayOneShot(cerrarcontroles);
                CerrarCartelGrande();
            }
            else if (!controladorScript.juegoPausado) // Note that I corrected this to match the previously suggested 'JuegoPausado' property
            {
                audioSourceControles.PlayOneShot(abrircontroles);
                animControles.SetBool("mostrarGrande", true);
                
            }
        }
    }

    public void MostrarTextoControles()
    {
        if (texto != null && texto.arrayTextos != null && texto.arrayTextos.Length > 0)
        {
            textoControles.text = texto.arrayTextos[0]; // Aquí asumimos que solo quieres mostrar el primer texto de los controles
        }
        else
        {
            textoControles.text = "No hay controles definidos.";
        }
    }

    public void CerrarCartelGrande()
    {
        animControles.SetBool("mostrarGrande", false);
        textoControles.text = "";
        controladorScript.DespausarJuego();
    }
}