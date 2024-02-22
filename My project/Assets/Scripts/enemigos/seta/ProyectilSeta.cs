using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilSeta : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private float direction;
    private float lifetime;
    private PersonajeBase personaje;
    [SerializeField] private float Damage;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        personaje = player.GetComponent<PersonajeBase>();
    }

    // Start is called before the first frame update

    void Start()
    {
        
    }
    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        float movementSpeed = velocidad * Time.deltaTime* direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 3) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        if (collision.CompareTag("Player") && !personaje.isDead)
        {
            personaje.RecibirDanio(Damage);
        }
        boxCollider.enabled = false;
        
        anim.SetTrigger("Explota");
    }
    public void SetDirection(float Direction)
    {
        lifetime = 0;
        direction = Direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != Direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Desactivate()
    {
        gameObject.SetActive(false);
    }
}