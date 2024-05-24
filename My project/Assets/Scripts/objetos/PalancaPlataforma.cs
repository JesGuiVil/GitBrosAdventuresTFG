using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaPlataforma : MonoBehaviour
{

    public bool isActivated = false;
    private Animator animator;
    private AudioSource audioSourcePalancaPlataforma;
    [SerializeField] private AudioClip activacion;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSourcePalancaPlataforma = gameObject.GetComponent<AudioSource>();
    }

    public void ActivatePalanca()
    {
        if (!isActivated)
        {
            // Reproducir la animaci�n de la palanca
            animator.SetTrigger("Activate");
            isActivated = true;
            audioSourcePalancaPlataforma.PlayOneShot(activacion);
        }

    }
}
