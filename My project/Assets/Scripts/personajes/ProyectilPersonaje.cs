using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilPersonaje : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator anim;
    public float direction=1;
    [SerializeField] private float tiempoProyectil;
    private PersonajeBase personajeScript;
    private EnemigoBase enemigoScript;
    private PalancaBase palanca;
    private float tiempo = 0;
    private GameObject lanzador;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        if (direction < 0)
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
        if (collision.CompareTag("Enemigo")){

            enemigoScript = collision.GetComponent<EnemigoBase>();
            if(!enemigoScript.enemyDead)
            {
                enemigoScript.enemigoRecibirDanio(lanzador.GetComponent<PersonajeBase>().danioDistancia);
            }
        }
        if (collision.CompareTag("Palanca"))
        {
            palanca = collision.GetComponent<PalancaBase>();
            if(palanca!=null){
                palanca.ActivatePalanca();
            }
        }
        boxCollider.enabled = false;
        anim.SetTrigger("Explota");
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