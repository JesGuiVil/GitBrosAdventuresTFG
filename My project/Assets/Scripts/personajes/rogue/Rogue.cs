using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rogue : PersonajeBase
{
    protected override void Update()
    {
        // Llama al método Update de la clase base
        base.Update();

        // Agrega la lógica específica del Rogue aquí
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer >= cooldownCerca)
        {
            animator.SetTrigger("ataquemelee");
            cooldownTimer = 0f;
        }
    }
}