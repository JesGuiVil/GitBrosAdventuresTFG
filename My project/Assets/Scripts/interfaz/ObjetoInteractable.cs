using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjetoInteractable : MonoBehaviour
{

    public float distanciaInteraccion = 2f; // Distancia de interacción
    private bool cercaDelObjeto = false;
    private ControlDialogos controlDialogos; // Variable para almacenar el controlador de diálogos
    private bool cartelMostrado = false; // Variable para seguir el estado del cartel
    public Textos textos;
    public Textos textos2;
    public Textos textos3;
    public Textos textoFinal;
    private Rogue rogue; // Person
    private Assassin assassin;
    private Archer archer;

    void Start()
    {
        controlDialogos = ControlDialogos.Instance;
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
                if (SceneManager.GetActiveScene().name == "EscenaRogue1")
                {
                    if (cartelMostrado)
                    {
                        controlDialogos.SiguienteFrase();
                    }
                    else if (rogue.tieneEspadas && rogue.yaHeHablado && rogue.espadasEntregada)
                    {
                        controlDialogos.ActivarCartel(textoFinal);
                        cartelMostrado = true;
                    }
                    else if (rogue.tieneEspadas && rogue.yaHeHablado)
                    {
                        controlDialogos.ActivarCartel(textos2);
                        cartelMostrado = true;
                    }
                    else
                    {
                        controlDialogos.ActivarCartel(textos);
                        cartelMostrado = true;
                        rogue.yaHeHablado = true;
                    }
                }
                if (SceneManager.GetActiveScene().name == "EscenaAssassin1")
                {
                    if (cartelMostrado)
                    {
                        controlDialogos.SiguienteFrase();
                    }
                    else if (assassin.tengoBaston && assassin.heAblado && assassin.cosaEntregada)
                    {
                        controlDialogos.ActivarCartel(textoFinal);
                        cartelMostrado = true;
                    }
                    else if (assassin.tengoBaston && assassin.heAblado)
                    {
                        controlDialogos.ActivarCartel(textos3);
                        cartelMostrado = true;
                    }
                    else if (assassin.tengoEspadas && assassin.heAblado)
                    {
                        controlDialogos.ActivarCartel(textos2);
                        cartelMostrado = true;
                    }
                    else
                    {
                        controlDialogos.ActivarCartel(textos);
                        cartelMostrado = true;
                        assassin.heAblado = true;
                    }
                }
                if (SceneManager.GetActiveScene().name == "EscenaArcher1")
                {
                    if (cartelMostrado)
                    {
                        controlDialogos.SiguienteFrase();
                    }
                    else if (archer.tengoAlgo && archer.Ablado && archer.algoEntregada)
                    {
                        controlDialogos.ActivarCartel(textoFinal);
                        cartelMostrado = true;
                    }
                    else if (archer.tengoAlgo && archer.Ablado)
                    {
                        controlDialogos.ActivarCartel(textos2);
                        cartelMostrado = true;
                    }
                    else
                    {
                        controlDialogos.ActivarCartel(textos);
                        cartelMostrado = true;
                        archer.Ablado = true;
                    }
                }
            }
        }
    }
}