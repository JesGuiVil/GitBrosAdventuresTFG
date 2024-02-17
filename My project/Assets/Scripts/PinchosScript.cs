using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pinchos : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemigo"))
        {
            Rogue rogue = collision.GetComponent<Rogue>(); // Obtén el componente Rogue en lugar de Salud
            if (rogue != null)
            {
                rogue.RecibirDanio(10000); // Llama al método RecibirDanio del Rogue para infligir daño
            }
        }
    }
}