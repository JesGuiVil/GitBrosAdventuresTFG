using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilEnemigo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator anim;
    public float direction=1;
    [SerializeField] private float tiempoProyectil;
    private PersonajeBase personaje;
    private float tiempo = 0;
    private GameObject lanzador;
    [SerializeField] private AudioClip hitaudio;
    [SerializeField] private AudioClip proyectilaudio;
    private AudioSource audiosource;
    private Rigidbody2D rb;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        personaje = player.GetComponent<PersonajeBase>();
        audiosource = GetComponent<AudioSource>();
        if(gameObject.name.Contains("bomba")){
            rb = GetComponent<Rigidbody2D>();
        }
       
        if (direction > 0)
        {
            Vector3 escalaTemp = transform.localScale;
            escalaTemp.x *= -1;
            transform.localScale = escalaTemp;
        }
        // Reproducir el sonido del proyectil en bucle
        audiosource.clip = proyectilaudio;
        audiosource.loop = true;
        audiosource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        
 
        transform.position = new Vector3(transform.position.x + (velocidad * Time.deltaTime * direction), transform.position.y, transform.position.z);
        tiempo += Time.deltaTime;
        if (tiempo >= tiempoProyectil)
        {
            hit = true;
            audiosource.loop = false;
            audiosource.Stop();
            anim.SetTrigger("Explota");
            boxCollider.enabled = false;
            if(gameObject.name.Contains("bomba")){   
                rb.isKinematic = true; // Cambiar el Rigidbody2D a Kinematic
                rb.velocity = Vector2.zero; // Detener cualquier movimiento residual
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;

        if (collision.CompareTag("Player") && !personaje.isDead)
        {
            lanzador.GetComponent<EnemigoBase>().DamageDistanciaPlayer();
        }
        audiosource.loop = false;
        audiosource.Stop();
        anim.SetTrigger("Explota");
        boxCollider.enabled = false;
        if(gameObject.name.Contains("bomba")){  
            rb.isKinematic = true; // Cambiar el Rigidbody2D a Kinematic
            rb.velocity = Vector2.zero; // Detener cualquier movimiento residual
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit = true;
        GameObject otherGameObject = collision.gameObject;
        if (otherGameObject.layer == LayerMask.NameToLayer("Default")||otherGameObject.layer == LayerMask.NameToLayer("suelo"))
        {
            hit = false;
        }
        else
        {

            if (otherGameObject.CompareTag("Player") && !personaje.isDead)
            {
                lanzador.GetComponent<EnemigoBase>().DamageDistanciaPlayer();
            }
            boxCollider.enabled = false;
            audiosource.loop = false;
            audiosource.Stop();
            anim.SetTrigger("Explota");
            if(gameObject.name.Contains("bomba")){   
                rb.isKinematic = true; // Cambiar el Rigidbody2D a Kinematic
                rb.velocity = Vector2.zero; // Detener cualquier movimiento residual
            }
            

        }
    }
    public void SetLanzador(GameObject Lanzador)
    {
        lanzador = Lanzador;
    }
    private void Desactivate()
    {
        Destroy(gameObject);
    }
    public void PlayExplosion()
    {
        audiosource.PlayOneShot(hitaudio);
    }
}