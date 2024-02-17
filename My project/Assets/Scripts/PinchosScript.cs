using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinchos : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemigo"))
        {
            //Salud salud = collision.GetComponent<Salud>();
            //if (salud != null)
            //{
                //salud.RecibirDanio(10000);
            //}
        }
    }
}