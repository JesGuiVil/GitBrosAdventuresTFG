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
    private void OnCollisionEnter2D(Collision2D collision)
    {
       SetaScript seta = collision.collider.GetComponent<SetaScript>();
    }

    public void ataqueDistancia(){
        animator.SetBool("ataqueDistancia", true);
    }
   
}