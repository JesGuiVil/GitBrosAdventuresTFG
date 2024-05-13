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

    void Start()
    {
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
                }
                else
                {
                    controlDialogos.ActivarCartel(textos);
                    cartelMostrado = true;
                }
            }
        }
    }
}