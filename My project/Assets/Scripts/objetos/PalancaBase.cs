using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaBase : MonoBehaviour
{
    public GameObject muro; // Asigna el GameObject del muro en el editor de Unity

    public bool isActivated = false;
    private Animator animator;
    private AudioSource audioSourcePalanca;
    [SerializeField] private AudioClip activar;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSourcePalanca = gameObject.GetComponent<AudioSource>();
    }

    public void ActivatePalanca()
    {
        if(!isActivated){
            // Reproducir la animación de la palanca
            animator.SetTrigger("Activate");

            // Desactivar el muro
            muro.SetActive(false);
            isActivated=true;
            audioSourcePalanca.PlayOneShot(activar);
        }
    }
        
}
