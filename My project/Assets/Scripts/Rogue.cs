using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rogue : PersonajeBase
{
    
    void Start()
    {
        PersonajeBaseStart();
    }

    void Update()
    {
        PersonajeBaseUpdate();

        animator.SetBool("attack_1", false);
        animator.SetBool("ataqueDistancia", false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("attack_1", true);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            animator.SetBool("ataqueDistancia", true);
        }
    }
    
    
    public void ataqueDistancia(){
        animator.SetBool("ataqueDistancia", true);
    }
   
}