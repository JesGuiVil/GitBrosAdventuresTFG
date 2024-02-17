using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rogue : PersonajeBase
{
    [SerializeField] private float vida;
    [SerializeField] private float maximoVida;
    [SerializeField] private BarraVidaScript barraVida;
    void Start()
    {
        PersonajeBaseStart();
        vida = maximoVida;
        barraVida.InicializarBarraVida(vida);
    }

    void Update()
    {
        PersonajeBaseUpdate();

        animator.SetBool("attack_1", false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("attack_1", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       SetaScript seta = collision.collider.GetComponent<SetaScript>();
    }
    public void RecibirDanio(float danio){
        vida-=danio;
        barraVida.CambiarVidaActual(vida);
        if (vida<=0){
            Destroy(gameObject);
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
}