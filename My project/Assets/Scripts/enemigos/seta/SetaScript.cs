using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetaScript : EnemigoBase
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private PatrullaEnemiga patrulla;
    private PersonajeBase personaje;
    private Animator anim;
    // Start is called before the first frame update
    
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
        if (PlayerInSight())
        {
            Debug.Log("personaje Detectado a melee");
            if (!personaje.isDead){
                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    anim.SetTrigger("meleAtack");
                }
            }
            
        }else
        {
            Debug.Log("personaje no detectado a melee");
        }
        if (patrulla != null)
        {
            patrulla.enabled = (!PlayerInSight() || personaje.isDead);
        }

    }
    
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),0, Vector2.left, 0, playerLayer);
       
        if (hit.collider != null)
        {
            personaje = hit.transform.GetComponent<PersonajeBase>();
        }

        return hit.collider != null;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void DamagePlayer()
    {
        if (PlayerInSight() && !personaje.isDead)
        {
            personaje.RecibirDanio(damage);
        }
    }
}