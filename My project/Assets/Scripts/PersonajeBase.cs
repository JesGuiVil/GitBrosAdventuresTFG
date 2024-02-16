using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeBase : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;
    public float tiempoJuego = 0f;

    private Rigidbody2D Rigidbody2D;
    protected Animator animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private bool IsJumping;

    // Start is called before the first frame update
    protected void PersonajeBaseStart()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    protected void PersonajeBaseUpdate()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        tiempoJuego += Time.deltaTime;

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-4.0f, 4.0f, 4.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);

        animator.SetBool("running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.8f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.8f))
    {
        Grounded = true;

        if (Rigidbody2D.velocity.y <= 0.0f)
        {
            IsJumping = false;
            animator.SetBool("jumping", false);
            animator.SetBool("falling", false);
        }
    }
    else
    {
        Grounded = false;

        if (Rigidbody2D.velocity.y > 0.0f)
        {
            // Está subiendo, por lo tanto, activa la animación de salto
            animator.SetBool("jumping", true);
            animator.SetBool("falling", false);
        }
        else
        {
            // Está cayendo, por lo tanto, activa la animación de caída
            animator.SetBool("jumping", false);
            animator.SetBool("falling", true);
        }
    }

    if (Input.GetKeyDown(KeyCode.W) && Grounded)
    {
        Jump();
    }   
    /*
    if (Input.GetKey(KeyCode.E) && Time.time > LastShoot + 0.25f) {
        Shoot();
        LastShoot = Time.time;
    }
    */
    }

    void Jump() {
        if (Grounded) {
            Rigidbody2D.AddForce(Vector2.up * JumpForce);
            IsJumping = true;
        }
    }

    void Shoot() {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.2f, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

    void FixedUpdate()
    {
       Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    void OnApplicationQuit() {
        Debug.Log("Tiempo total de juego: " + tiempoJuego + " segundos");
    }
}