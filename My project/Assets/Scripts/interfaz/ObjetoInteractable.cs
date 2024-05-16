using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractable : MonoBehaviour
{
    public Textos textos;
    public float distanciaInteraccion = 2f; // Distancia de interacción
    private bool cercaDelObjeto = false;
    private ControlDialogos controlDialogos; // Variable para almacenar el controlador de diálogos
    private bool cartelMostrado = false; // Variable para seguir el estado del cartel

    public Textos textosConLlave;

    public Textos textoFinal;

    private PersonajeBase personajeBase; // Person

    void Start()
    {
        personajeBase = GameObject.FindGameObjectWithTag("Player").GetComponent<PersonajeBase>();
        GameObject dialogos = GameObject.FindGameObjectWithTag("dialogos");
        if (dialogos != null)
        {
            controlDialogos = dialogos.GetComponent<ControlDialogos>();
            if (controlDialogos == null)
            {
                Debug.LogError("ControlDialogos no encontrado en el objeto encontrado por tag.");
            }
        }
        else
        {
            Debug.LogError("Objeto con tag 'dialogos' no encontrado.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cercaDelObjeto = true;
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cercaDelObjeto = false;
            if (controlDialogos != null && cartelMostrado)
            {
                controlDialogos.cierraCartel();
                cartelMostrado = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cercaDelObjeto = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cercaDelObjeto = false;
            if (controlDialogos != null && cartelMostrado)
            {
                controlDialogos.cierraCartel();
                cartelMostrado = false;
            }
        }
        
    }

    void Update()
    {
        // Verificar si el jugador está cerca y ha pulsado la tecla "E"
        if (cercaDelObjeto && Input.GetKeyDown(KeyCode.E))
        {
            if (controlDialogos != null)
            {
                if (cartelMostrado)
                {
                    controlDialogos.SiguienteFrase();
                }else if (personajeBase.tieneLlave && personajeBase.yaHeHablado && personajeBase.llaveEntregada)
                {
                    controlDialogos.ActivarCartel(textoFinal);
                    cartelMostrado = true;
                }
                else if (personajeBase.tieneLlave && personajeBase.yaHeHablado)
                {
                    controlDialogos.ActivarCartel(textosConLlave);
                    cartelMostrado = true;
                }
                else{
                    controlDialogos.ActivarCartel(textos);
                    cartelMostrado = true;
                    personajeBase.yaHeHablado=true;
                }
            }
        }
    }
}