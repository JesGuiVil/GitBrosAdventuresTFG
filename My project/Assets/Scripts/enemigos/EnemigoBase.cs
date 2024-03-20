using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBase : MonoBehaviour
{
    [SerializeField] private float vidaEnemigo;
    [SerializeField] private float maximoVidaEnemigo;
    private Animator anim;
    public bool enemyDead;
    [SerializeField] private float attackCooldownMelee;
    [SerializeField] private float rangeMelee;
    [SerializeField] private float damageMelee;
    [SerializeField] private float colliderDistanceMelee;
    [SerializeField] private float attackCooldownDistancia;
    [SerializeField] private float rangeDistancia;
    [SerializeField] private float alturaDistancia;
    [SerializeField] private float posicionDistancia;
    [SerializeField] public int damageDistancia;
    [SerializeField] private float colliderDistanceDistancia;
    [SerializeField] private Transform puntoProyectil;
    [SerializeField] private GameObject proyectil;
    private Rigidbody2D rb;
    private float baseGravity;
    [SerializeField] private float dashingTime = 0.2F;
    [SerializeField] private float dashForce = 20f;
    [SerializeField] private float timeCanDash = 1f;
    private bool isDashing;
    private bool canDash = true;
    private float direction;

    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    private PatrullaEnemiga patrulla;
    private PersonajeBase personaje;
    private MecanicasBase mecanicasBase;
    // Start is called before the first frame update
    void Start()
    {
        enemyDead=false;
        vidaEnemigo = maximoVidaEnemigo;
    }
    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        baseGravity = rb.gravityScale;
        patrulla = GetComponentInParent<PatrullaEnemiga>();
        anim = GetComponent<Animator>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        personaje = player.GetComponent<PersonajeBase>();
        GameObject Mecanicas = GameObject.FindGameObjectWithTag("Mecanicas");
        mecanicasBase = Mecanicas.GetComponent<MecanicasBase>();

    }
    
    // Update is called once per frame
    void Update()
    {
        direction = transform.localScale.x;
        cooldownTimer += Time.deltaTime;
        if (PlayerInSightMelee())
        {
            
            if (!personaje.isDead){
                if (cooldownTimer >= attackCooldownMelee)
                {
                    cooldownTimer = 0;
                    anim.SetTrigger("meleAtack");
                }
            }
        }
        if (PlayerInSightDistancia())
        {
            
            if(!personaje.isDead){

                if (cooldownTimer >= attackCooldownDistancia)
                {
                    cooldownTimer = 0;
                    anim.SetTrigger("ataqueDistancia");
                }
            } 
        }
        if (patrulla != null)
        {
            patrulla.enabled = (!PlayerInSightMelee() && !PlayerInSightDistancia() || personaje.isDead);
        } 
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2 (direction * dashForce, 0f);
        yield return new WaitForSeconds(dashingTime);
        isDashing= false;
        rb.gravityScale = baseGravity;
        yield return new WaitForSeconds(timeCanDash);
        canDash= true;

    }
    private bool PlayerInSightMelee()
    {
        RaycastHit2D hitMelee = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rangeMelee * transform.localScale.x * colliderDistanceMelee,new Vector3(boxCollider.bounds.size.x * rangeMelee, boxCollider.bounds.size.y, boxCollider.bounds.size.z),0, Vector2.left, 0, playerLayer);
       
        if (hitMelee.collider != null)
        {
            personaje = hitMelee.transform.GetComponent<PersonajeBase>();
        }
        return hitMelee.collider != null;
    }
    private bool PlayerInSightDistancia()
    {
        Vector3 center = boxCollider.bounds.center;
        center.y += posicionDistancia;
        RaycastHit2D hitDistancia = Physics2D.BoxCast(center + transform.right * rangeDistancia * transform.localScale.x * colliderDistanceDistancia, new Vector3(boxCollider.bounds.size.x * rangeDistancia, boxCollider.bounds.size.y * alturaDistancia, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        return hitDistancia.collider != null;
    }
    private void OnDrawGizmos()
    {
        Vector3 center = boxCollider.bounds.center;
        center.y += posicionDistancia;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rangeMelee * transform.localScale.x * colliderDistanceMelee,new Vector3(boxCollider.bounds.size.x * rangeMelee, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(center + transform.right * rangeDistancia * transform.localScale.x * colliderDistanceDistancia, new Vector3(boxCollider.bounds.size.x * rangeDistancia, boxCollider.bounds.size.y * alturaDistancia, boxCollider.bounds.size.z));
    
    }

    private void LanzaProyectil()
    {
        GameObject Proyec = Instantiate(proyectil,puntoProyectil.position,Quaternion.identity);
        Proyec.GetComponent<ProyectilEnemigo>().direction = transform.localScale.x;
        Proyec.GetComponent<ProyectilEnemigo>().SetLanzador(gameObject);
    }

    public void enemigoRecibirDanio(float danio)
    {
        vidaEnemigo -= danio;
        
        if (vidaEnemigo <= 0)
        {
            enemyDead=true;
            anim.SetTrigger("Die");
            gameObject.layer=LayerMask.NameToLayer("playermuerto");
            mecanicasBase.AumentarContadorEnemigos();
        }else{
            anim.SetTrigger("Hurt");
        }

    }
    
    private void DamageMeleePlayer()
    {
        if (PlayerInSightMelee() && !personaje.isDead)
        {
            personaje.RecibirDanio(damageMelee);
        }
    }
    public void DamageDistanciaPlayer()
    {
        if (!personaje.isDead)
        {
            personaje.RecibirDanio(damageDistancia);
        }
    }
}