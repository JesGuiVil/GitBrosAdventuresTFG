using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assassin : PersonajeBase
{
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer >= cooldownCerca)
        {
            animator.SetTrigger("ataquemelee");
            cooldownTimer = 0f;
        }
    }
}