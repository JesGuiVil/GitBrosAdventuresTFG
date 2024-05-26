using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreScript : MonoBehaviour
{

    public bool abierto;
    private Animator animator;
    private AudioSource audioSourceCofre;
    [SerializeField] private AudioClip abrir;
    [SerializeField] private GameObject objeto;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSourceCofre = gameObject.GetComponent<AudioSource>();
    }

    public void ActivateCofre()
    {
        if(!abierto){
            // Reproducir la animación de la palanca
            animator.SetTrigger("Activate");
            Instantiate(objeto, gameObject.transform.position, Quaternion.identity);
            abierto=true;
            audioSourceCofre.PlayOneShot(abrir);
        }
    }
}
