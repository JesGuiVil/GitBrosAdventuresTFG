using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EsqueletoScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed; // Velocidad a la que el enemigo se mueve
    private GameObject Player; // Referencia al personaje
    private EnemigoBase enemigoBase; // Referencia al componente EnemigoBase
    private AudioSource audioSource; // Referencia al componente AudioSource
    public float followDistance = 3f; // Distancia a la que el enemigo debe quedarse del jugador

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Buscar al personaje en la escena
        Player = GameObject.FindWithTag("Player");
        enemigoBase = GetComponent<EnemigoBase>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 2f;
        audioSource.clip = enemigoBase.Caminar; // Hacer que el sonido se repita mientras el enemigo se mueve
        audioSource.Play();
    }

    void Update()
    {
        if (Player == null || enemigoBase.enemyDead || Player.transform.position.x >= 94)
        {
            rb.velocity = Vector2.zero; // Detener al enemigo
            if (enemigoBase.enemyDead)
            {
                audioSource.loop = false;
                Destroy(gameObject, 2f); // Destruir después de 2 segundos
            }

            return;
        }

        // Calcular la distancia entre el enemigo y el jugador
        float distance = Player.transform.position.x - transform.position.x;

        // Si el enemigo está demasiado lejos, acercarse al jugador
        if (Mathf.Abs(distance) > followDistance)
        {
            Vector2 direction = (Player.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;

            // Cambiar la dirección del sprite según la posición del jugador
            if (direction.x >= 0.0f) transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
            else transform.localScale = new Vector3(-4.0f, 4.0f, 4.0f);
        }
        else
        {
            // Detener al enemigo si está dentro de la distancia de seguimiento
            rb.velocity = Vector2.zero;
        }
    }
}
