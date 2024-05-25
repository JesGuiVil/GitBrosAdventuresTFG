using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;
    private PersonajeBase personaje;
    private float baseGravity;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashForce = 20f;
    [SerializeField] private float TimeCanDash = 1f;
    public Animator animator;

    private bool isDashing;
    private bool canDash = true;
    public bool IsDashing => isDashing;

    [SerializeField] private AudioClip dashSound; // Sonido del dash
    private AudioSource audioSource; // Referencia al AudioSource

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        personaje = GetComponent<PersonajeBase>();
        baseGravity = rb.gravityScale;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)&& canDash)
        {
            animator.SetTrigger("Dash");
            audioSource.PlayOneShot(dashSound); // Reproduce el sonido del dash
            StartCoroutine(miDash());
        }
    }

    private IEnumerator miDash()
    {
        if(personaje.direction != 0 && canDash)
        {
            isDashing = true;
            canDash = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(personaje.direction * dashForce, 0f);
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
            rb.gravityScale = baseGravity;
            yield return new WaitForSeconds(TimeCanDash);
            canDash = true;
        }
        
    }
}
