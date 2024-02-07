using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : PersonajeBase
{
    void Start()
    {
        PersonajeBaseStart();
    }

    void Update()
    {
        PersonajeBaseUpdate();

        animator.SetBool("attack_1", false);

        if(Input.GetKeyDown(KeyCode.Space)) {
            animator.SetBool("attack_1", true);
        }

    }
}