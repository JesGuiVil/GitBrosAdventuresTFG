using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ataqueRanged : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private PatrullaEnemiga patrulla;
    private Animator anim;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] proyectiles;
    [SerializeField] private GameObject personaje;
    private PersonajeBase personajeScript;
    // Start is called before the first frame update
    void Start()
    {
        personajeScript = personaje.GetComponent<PersonajeBase>();
        
    }
    private void Awake()
    {
        patrulla = GetComponentInParent<PatrullaEnemiga>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            
            Debug.Log("personaje Detectado a rango");
            if(!personajeScript.isDead){

                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    anim.SetTrigger("ataqueDistancia");
                }
            } 
        }
        else
        {
            Debug.Log("personaje no detectado a rango");
        }
        if (patrulla != null)
        {
            patrulla.enabled = (!PlayerInSight() || personajeScript.isDead);
        }

    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void RangedAttack()
    {
        
        cooldownTimer = 0;
        anim.SetTrigger("ataqueDistancia");

        proyectiles[FindProyectil()].transform.position = firepoint.position;
        proyectiles[FindProyectil()].GetComponent<ProyectilSeta>().SetDirection(Mathf.Sign(transform.localScale.x));
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
}