using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assassin : PersonajeBase
{
    public bool tengoEspadas = false;
    public bool heAblado = false;
    public bool cosaEntregada = false;
    protected override void Update()
    {
        base.Update();
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer >= cooldownCerca && tengoEspadas)
            {
                animator.SetTrigger("ataquemelee");
                cooldownTimer = 0f;
            }
        }
        
    }
}