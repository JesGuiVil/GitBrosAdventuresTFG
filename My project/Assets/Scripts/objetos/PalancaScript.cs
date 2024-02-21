using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour
{
    public GameObject muro; // Asigna el GameObject del muro en el editor de Unity

    private bool isActivated = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            ActivatePalanca();
        }
    }

    private void ActivatePalanca()
    {
        // Reproducir la animación de la palanca
        animator.SetTrigger("Activate");

        // Desactivar el muro
        muro.SetActive(false);
    }
}
