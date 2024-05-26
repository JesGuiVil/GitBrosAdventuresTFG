using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersonajeBase : MonoBehaviour
{
    [SerializeField] private float danioCerca;
    [SerializeField] public float cooldownCerca;
    [SerializeField] private float rangeCerca;
    [SerializeField] private float colliderDistanceCerca;
    [SerializeField] public float danioDistancia;
    [SerializeField] public float cooldownDistancia;
    [SerializeField] private float rangeDistancia;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask palancaLayer;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject proyectil;
    [SerializeField] public float vida;
    [SerializeField] private float maximoVida;
    [SerializeField] private AudioClip saltar;
    [SerializeField] private AudioClip correr;
    [SerializeField] private AudioClip habilidad;
    [SerializeField] private AudioClip daño;
    [SerializeField] private AudioClip muerte;
    [SerializeField] private AudioClip ataque1;
    [SerializeField] public AudioClip ataque2;
    public AudioSource audioSource;
    private AudioSource correrAudioSource;
    public float cooldownTimer = Mathf.Infinity;
    private Dash dash;
    private ControladorScript controladorScript;
    private EnemigoBase enemigo;
    private PalancaBase palanca;
    private PalancaPlataforma palancaP;
    public float Speed;
    public float JumpForce;
    public float tiempoJuego = 0f;
    public float Gravedad;
    private Rigidbody2D rigidbody2D;
    public Animator animator;
    private float Horizontal;
    public float direction => Horizontal;
    private bool Grounded;
    private bool estaAgua = false;
    private bool IsJumping;
    private Collider2D collider;
    private GameObject BarraVida;
    private BarraVidaScript barraVida;
    public bool isDead = false;
    public bool cercaDelNpc = false;


    public ControlDialogos controlDialogos;
    public Textos textoInicial;
    private GameObject gameOver;
    // Start is called before the first frame update
    protected void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vida = maximoVida;
        BarraVida=GameObject.FindGameObjectWithTag("Barravida");
        barraVida=BarraVida.GetComponent<BarraVidaScript>();
        barraVida.InicializarBarraVida(vida);
        collider= GetComponent<Collider2D>();
        controladorScript=GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorScript>();
        dash = GetComponent<Dash>();
        controlDialogos = ControlDialogos.Instance;
        gameOver=GameObject.FindGameObjectWithTag("gameover");
        gameOver.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        correrAudioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(IniciarDialogoConDelay());
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        cooldownTimer += Time.deltaTime;
        CheckGrounded();

        if (!isDead && !controladorScript.juegoPausado && !controlDialogos.mostrandoCartel)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
            tiempoJuego += Time.deltaTime;

            if (Horizontal < 0.0f)
                transform.localScale = new Vector3(-4.0f, 4.0f, 4.0f);
            else if (Horizontal > 0.0f)
                transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);

            // Reproducir el sonido de correr si está en el suelo, se está moviendo horizontalmente y no está ya reproduciéndose
            if (Grounded && Horizontal != 0.0f && (!correrAudioSource.isPlaying || correrAudioSource.clip != correr))
            {
                correrAudioSource.clip = correr;
                correrAudioSource.loop = true;
                correrAudioSource.Play();
            }
            // Detener el sonido de correr si no se está moviendo horizontalmente o no está en el suelo
            else if ((!Grounded || Horizontal == 0.0f) && correrAudioSource.isPlaying && correrAudioSource.clip == correr)
            {
                correrAudioSource.Stop();
            }

            animator.SetBool("running", Horizontal != 0.0f);

            if (Input.GetKeyDown(KeyCode.W) && Grounded && !estaAgua && !dash.IsDashing)
            {
                // Detener el sonido de correr antes de saltar
                if (correrAudioSource.isPlaying && correrAudioSource.clip == correr)
                {
                    correrAudioSource.Stop();
                }

                Jump();
                audioSource.PlayOneShot(saltar);
            }
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);

            // Detener el sonido de correr si el juego está pausado o se muestra un cartel
            if (correrAudioSource.isPlaying && correrAudioSource.clip == correr)
            {
                correrAudioSource.Stop();
            }
        }

        if (isDead && !gameOver.activeSelf)
        {
            StartCoroutine(GameOverSequence());
        }
    }


    IEnumerator IniciarDialogoConDelay()
    {
        yield return new WaitForSeconds(0.5f); // Añadir un retraso de medio segundo antes de activar el diálogo inicial
        controlDialogos.ActivarCartel(textoInicial);

        // Espera hasta que se presione la tecla E antes de avanzar al siguiente frase
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        controlDialogos.SiguienteFrase();
        
    }
    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(2f); // Espera 1 segundo
        gameOver.SetActive(true);

        yield return new WaitForSeconds(4f); // Espera 4 segundos después de activar game over
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena
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
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * rangeCerca * transform.localScale.x * colliderDistanceCerca,new Vector3(capsuleCollider.bounds.size.x * rangeCerca, capsuleCollider.bounds.size.y/2, capsuleCollider.bounds.size.z));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("agua"))
        {
            estaAgua=true;
            rigidbody2D.drag = 25f;
        }
        else if (collision.CompareTag("Npc"))
        {   
            cercaDelNpc = true;
        }
        else
        {
            estaAgua = false;
            rigidbody2D.drag = 0f;
        }
        
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Npc"))
        {   
            cercaDelNpc = false;
        }  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;
        if (otherGameObject.layer == LayerMask.NameToLayer("suelo"))
        {
            Grounded = true;
            IsJumping = false;
        }
        if (collision.gameObject.tag == "PlataformaMovil")
        {
            transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovil")
        {
            transform.parent = null;
        }
        else if (collision.gameObject.tag == "Npc")
        {
            cercaDelNpc = false;
        }
        
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
        if(!isDead && !dash.IsDashing){
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
        if(vida>0)  {
            animator.SetTrigger("Hurt");
            audioSource.PlayOneShot(daño);
        }
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
        animator.SetTrigger("Muerte");
        audioSource.PlayOneShot(muerte);
        gameObject.layer=LayerMask.NameToLayer("playermuerto");
    }
    public void ataqueDistancia(){

        GameObject Proyec = Instantiate(proyectil,firepoint.position,Quaternion.identity);
        Proyec.GetComponent<ProyectilPersonaje>().direction = transform.localScale.x;
        Proyec.GetComponent<ProyectilPersonaje>().SetLanzador(gameObject);
    }
   

    public void ataqueCerca(){
        audioSource.PlayOneShot(ataque1);
        RaycastHit2D hitEnemigo = Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * rangeCerca * transform.localScale.x * colliderDistanceCerca,new Vector3(capsuleCollider.bounds.size.x * rangeCerca, capsuleCollider.bounds.size.y/2, capsuleCollider.bounds.size.z),0, Vector2.left, 0, enemyLayer);
        RaycastHit2D hitPalanca = Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * rangeCerca * transform.localScale.x * colliderDistanceCerca,new Vector3(capsuleCollider.bounds.size.x * rangeCerca, capsuleCollider.bounds.size.y/2, capsuleCollider.bounds.size.z),0, Vector2.left, 0, palancaLayer);
        if (hitEnemigo.collider != null)
        {
            enemigo = hitEnemigo.transform.GetComponent<EnemigoBase>();
            if(enemigo!=null){
                enemigo.enemigoRecibirDanio(danioCerca);
            }
        }
        if (hitPalanca.collider != null)
        {   
            palancaP = hitPalanca.transform.GetComponent<PalancaPlataforma>();
            palanca = hitPalanca.transform.GetComponent<PalancaBase>();
            if (palancaP != null)
            {
                palancaP.ActivatePalanca();
            }
            if(palanca!=null){
                palanca.ActivatePalanca();
            }
        }
    }
}