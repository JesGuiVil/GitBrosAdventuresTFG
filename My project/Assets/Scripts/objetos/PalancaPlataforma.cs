using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaPlataforma : MonoBehaviour
{

    public bool isActivated = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ActivatePalanca()
    {
        if (!isActivated)
        {
            // Reproducir la animación de la palanca
            animator.SetTrigger("Activate");
            isActivated = true;
        }

    }
}
