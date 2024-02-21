using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsarPocionScript : MonoBehaviour
{
    public float cantidadVidaARecuperar = 25000; // Cantidad de vida a recuperar al usar la poción
    private GameObject player; // Cambié el tipo de variable para hacer referencia al script del jugador

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Use()
    {
        player.GetComponent<PersonajeBase>().Curar(cantidadVidaARecuperar); // Llamar al método Curar del jugador y pasarle la cantidad de vida a recuperar
        Destroy(gameObject);
    }
}
