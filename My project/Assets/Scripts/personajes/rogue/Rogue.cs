using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rogue : PersonajeBase
{

    public bool yaHeHablado = false;
    public bool pistas= false;
    public bool pedidas= false;
    public bool espadasEntregada = false;

    public bool tieneEspadas = false;
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