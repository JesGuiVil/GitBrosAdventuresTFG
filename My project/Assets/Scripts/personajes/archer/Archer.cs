using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : PersonajeBase
{
    public bool tengoAlgo = false;
    public bool Ablado = false;
    public bool algoEntregada = false;
    public bool tengoLlave = false;
    public bool tengopocion = false;
    public bool yapedido = false;

    protected override void Update()
    {
        // Llama al método Update de la clase base
        base.Update();
        if (!isDead)
        {
            // Agrega la lógica específica del Rogue aquí
            if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer >= cooldownCerca)
            {
                animator.SetTrigger("ataquemelee");
                cooldownTimer = 0f;
            }
        }
    }
}