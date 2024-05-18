using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlControles : MonoBehaviour
{
    private Animator animDialogos;

    private Animator animControles;
    private Queue <string> colaDialogos;
    private Textos texto;

    public Textos controles;

    private PersonajeBase personajeBase;
    [SerializeField] TextMeshProUGUI textoPantalla;
    [SerializeField] TextMeshProUGUI textoControles;
    // Start is called before the first frame update
    void Start()
    {
        animDialogos=gameObject.GetComponent<Animator>();
        colaDialogos = new Queue<string>();
        personajeBase = GameObject.FindGameObjectWithTag("Player").GetComponent<PersonajeBase>();
    }

    public void ActivarCartel (Textos textoObjeto){
        animDialogos.SetBool("mostrar",true);
        texto=textoObjeto;
    }
    public void ActivaTexto(){
    colaDialogos.Clear();
    Time.timeScale = 0f;

    // Comprobación de nulidad para texto
    if (texto != null){
        // Comprobación de nulidad para texto.arrayTextos
        if (texto.arrayTextos != null){
            foreach (string textoGuardar in texto.arrayTextos){
                colaDialogos.Enqueue(textoGuardar);
            }
            SiguienteFrase();
        }    
    }  
    }

    public void SiguienteFrase(){
        if(colaDialogos.Count==0){
            cierraCartel();
            Time.timeScale = 1f;
            if (personajeBase.llaveEntregada)
            {
                SceneManager.LoadScene("EscenaAssassin1");
            }
            
            return;
        }
        string fraseActual = colaDialogos.Dequeue();
        textoPantalla.text=fraseActual;
    }
    public void cierraCartel(){
        animDialogos.SetBool("mostrar",false);
        textoPantalla.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
        // Comprobar si se presiona la tecla 'C'
        if (animDialogos.GetBool("mostrarGrande"))
            {
                CerrarCartelGrande();
            }
            else
            {
                animDialogos.SetBool("mostrarGrande", true);
                Time.timeScale = 0f;
            }
        }
    }
    
     public void MostrarTextoControles()
    {
        if (controles != null && controles.arrayTextos != null && controles.arrayTextos.Length > 0)
        {
            textoControles.text = controles.arrayTextos[0]; // Aquí asumimos que solo quieres mostrar el primer texto de los controles
        }
        else
        {
            textoControles.text = "No hay controles definidos.";
        }
    }

    // Método para cerrar el cartel grande
    public void CerrarCartelGrande()
    {
        animDialogos.SetBool("mostrarGrande", false);
        textoControles.text = "";
        Time.timeScale = 1f;
    }
    
}