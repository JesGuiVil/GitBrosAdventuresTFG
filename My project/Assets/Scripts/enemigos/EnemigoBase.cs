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
    [SerializeField] public int damageDistancia;
    [SerializeField] private float colliderDistanceDistancia;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] proyectiles;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashSpeed;
    private bool isDashing = false;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    private PatrullaEnemiga patrulla;
    private PersonajeBase personaje;
    // Start is called before the first frame update
    void Start()
    {
        enemyDead=false;
        vidaEnemigo = maximoVidaEnemigo;
    }
    private void Awake()
    {
        patrulla = GetComponentInParent<PatrullaEnemiga>();
        anim = GetComponent<Animator>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        personaje = player.GetComponent<PersonajeBase>();
    }
    
    // Update is called once per frame
    void Update()
    {
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
        RaycastHit2D hitDistancia = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rangeDistancia * transform.localScale.x * colliderDistanceDistancia, new Vector3(boxCollider.bounds.size.x * rangeDistancia, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        return hitDistancia.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rangeMelee * transform.localScale.x * colliderDistanceMelee,new Vector3(boxCollider.bounds.size.x * rangeMelee, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rangeDistancia * transform.localScale.x * colliderDistanceDistancia, new Vector3(boxCollider.bounds.size.x * rangeDistancia, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    
    }
    private void RangedAttack()
    {
        
        cooldownTimer = 0;
        proyectiles[FindProyectil()].transform.position = firepoint.position;
        proyectiles[FindProyectil()].GetComponent<ProyectilEnemigo>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindProyectil()
    {
        for (int i = 0; i < proyectiles.Length; i++)
        {
            if (!proyectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    public void enemigoRecibirDanio(float danio)
    {
        vidaEnemigo -= danio;
        
        if (vidaEnemigo <= 0)
        {
            enemyDead=true;
            anim.SetTrigger("Die");
            gameObject.layer=LayerMask.NameToLayer("playermuerto");
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