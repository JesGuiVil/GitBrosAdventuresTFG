using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeBase : MonoBehaviour
{
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float danioCerca;
    [SerializeField] private float cooldownCerca;
    [SerializeField] private float rangeCerca;
    [SerializeField] private float colliderDistanceCerca;
    [SerializeField] private float danioDistancia;
    [SerializeField] private float cooldownDistancia;
    [SerializeField] private float rangeDistancia;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    private EnemigoBase enemigo;
    public float Speed;
    public float JumpForce;
    public float tiempoJuego = 0f;
    public float Gravedad;
    private Rigidbody2D rigidbody2D;
    protected Animator animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private bool IsJumping;
    private Collider2D collider;
    [SerializeField] public float vida;
    [SerializeField] private float maximoVida;
    [SerializeField] private BarraVidaScript barraVida;
    public bool isDead = false;

    // Start is called before the first frame update
    protected void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vida = maximoVida;
        barraVida.InicializarBarraVida(vida);
        collider= GetComponent<Collider2D>();
    }
    
    // Update is called once per frame
    protected void Update()
    {
        cooldownTimer += Time.deltaTime;
        CheckGrounded();
        if(!isDead)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");

            tiempoJuego += Time.deltaTime;

            if (Horizontal < 0.0f) transform.localScale = new Vector3(-4.0f, 4.0f, 4.0f);
            else if (Horizontal > 0.0f) transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);

            animator.SetBool("running", Horizontal != 0.0f);

            if (Input.GetKeyDown(KeyCode.W) && Grounded)
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer >= cooldownCerca)
            {
                animator.SetTrigger("ataquemelee");
                cooldownTimer = 0f;
            }
            if (Input.GetKeyDown(KeyCode.E) && cooldownTimer >= cooldownDistancia) {
                animator.SetTrigger("ataquedistancia");
                cooldownTimer = 0f;
            }
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
        }
    }
    private void OnDrawGizmos()
    {
        // Visualizar el raycast izquierdo
        Vector2 leftRaycastOrigin = transform.position + Vector3.down * 0.75f + Vector3.left * 0.3f; // Desplazar a la izquierda
        float raycastLength = 0.1f; // Longitud del raycast

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(leftRaycastOrigin, leftRaycastOrigin + Vector2.down * raycastLength);

        // Visualizar el raycast derecho
        Vector2 rightRaycastOrigin = transform.position + Vector3.down * 0.75f + Vector3.right * 0.3f; // Desplazar a la derecha
        Gizmos.DrawLine(rightRaycastOrigin, rightRaycastOrigin + Vector2.down * raycastLength);
        //gizmo del rango de ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * rangeCerca * transform.localScale.x * colliderDistanceCerca,new Vector3(capsuleCollider.bounds.size.x * rangeCerca, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
    }

    private void CheckGrounded()
    {
        // Raycast izquierdo
        Vector2 leftRaycastOrigin = transform.position + Vector3.down * 0.75f + Vector3.left * 0.3f; // Desplazar a la izquierda
        RaycastHit2D leftHit = Physics2D.Raycast(leftRaycastOrigin, Vector2.down, 0.15f);

        // Raycast derecho
        Vector2 rightRaycastOrigin = transform.position + Vector3.down * 0.75f + Vector3.right * 0.3f; // Desplazar a la derecha
        RaycastHit2D rightHit = Physics2D.Raycast(rightRaycastOrigin, Vector2.down, 0.15f);

        Debug.DrawRay(leftRaycastOrigin, Vector3.down * 0.15f, Color.blue); // Dibuja el raycast izquierdo
        Debug.DrawRay(rightRaycastOrigin, Vector3.down * 0.15f, Color.blue); // Dibuja el raycast derecho
        
        // Verifica si cualquiera de los dos raycasts toca el suelo
        if (leftHit.collider != null || rightHit.collider != null)
        {
            Grounded = true;

            if (rigidbody2D.velocity.y <= 0.1f)
            {
                IsJumping = false;
                animator.SetBool("jumping", false);
                animator.SetBool("falling", false);
            }
        }
        else
        {
            Grounded = false;

            if (rigidbody2D.velocity.y > 0.0f)
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
    }

    void Jump() {
        if (Grounded) {
            rigidbody2D.AddForce(Vector2.up * JumpForce);
            IsJumping = true;
        }
    }

    void FixedUpdate()
    {   
        if(!isDead){
            if (!Grounded) 
            {
                rigidbody2D.velocity += Vector2.up * Gravedad * Time.fixedDeltaTime;
            }
            rigidbody2D.velocity = new Vector2(Horizontal * Speed, rigidbody2D.velocity.y);    
        }
    }

    void OnApplicationQuit() {
        Debug.Log("Tiempo total de juego: " + tiempoJuego + " segundos");
    }
    public void RecibirDanio(float danio){
        vida-=danio;
        barraVida.CambiarVidaActual(vida);
        if (vida<=0){
            Morir();
        }
    }
    public void Curar(float curacion){
        if ((vida+curacion)>maximoVida){
            vida=maximoVida;
        }
        else
        {
            vida+=curacion;
        }
        barraVida.CambiarVidaActual(vida);
    }
    public void Morir()
    {
        isDead = true;
        animator.SetTrigger("RogueMuerte");
        gameObject.layer=LayerMask.NameToLayer("playermuerto");
    }
    public void ataqueDistancia(){
        
    }
    public void ataqueCerca(){
        
        RaycastHit2D hit = Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * rangeCerca * transform.localScale.x * colliderDistanceCerca,new Vector3(capsuleCollider.bounds.size.x * rangeCerca, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),0, Vector2.left, 0, enemyLayer);
        if (hit.collider != null)
        {
            enemigo = hit.transform.GetComponent<EnemigoBase>();
            if(enemigo!=null){
                enemigo.enemigoRecibirDanio(danioCerca);
            }
        }
    }
}