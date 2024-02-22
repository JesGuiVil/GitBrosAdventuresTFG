using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rogue : PersonajeBase
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
    
    void Start()
    {
        PersonajeBaseStart();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
        PersonajeBaseUpdate();

        animator.SetBool("attack_1", false);
        animator.SetBool("ataqueDistancia", false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("attack_1", true);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            animator.SetBool("ataqueDistancia", true);
        }
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * rangeCerca * transform.localScale.x * colliderDistanceCerca,new Vector3(capsuleCollider.bounds.size.x * rangeCerca, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
    }
    
   
}