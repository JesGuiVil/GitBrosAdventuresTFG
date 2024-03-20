using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaPlataforma : MonoBehaviour
{
    public GameObject plataforma; // Asigna el GameObject del muro en el editor de Unity

    public bool isActivated = false;
    private Animator animator;

    private void Start()
    {
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
