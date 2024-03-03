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

    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        personaje = player.GetComponent<PersonajeBase>();

        if (direction > 0)
        {
            Vector3 escalaTemp = transform.localScale;
            escalaTemp.x *= -1;
            transform.localScale = escalaTemp;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        
 
        transform.position = new Vector3(transform.position.x + (velocidad * Time.deltaTime * direction), transform.position.y, transform.position.z);
        tiempo += Time.deltaTime;
        if (tiempo >= tiempoProyectil)
        {
            anim.SetTrigger("Explota");
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;

        if (collision.CompareTag("Player") && !personaje.isDead)
        {
            lanzador.GetComponent<EnemigoBase>().DamageDistanciaPlayer();
        }
        anim.SetTrigger("Explota");
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
            anim.SetTrigger("Explota");
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
}