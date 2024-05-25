using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EsqueletoScript : MonoBehaviour
{


    private Rigidbody2D rb;
    public float speed; // Velocidad a la que el enemigo se mueve
    private GameObject Antonio; // Referencia al personaje
    private EnemigoBase enemigoBase; // Referencia al componente EnemigoBase
    private bool isScheduledForDestruction = false;

    [SerializeField] private AudioClip volar; // Clip de sonido para volar
    [SerializeField] private AudioClip Morir;
    private AudioSource audioSource; // Referencia al componente AudioSource

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Buscar al personaje en la escena
        Antonio = GameObject.FindWithTag("Player");

        enemigoBase = GetComponent<EnemigoBase>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = volar;
        audioSource.loop = true; // Hacer que el sonido se repita mientras el enemigo se mueve
    }

    void Update()
    {
        if (Antonio == null || enemigoBase.enemyDead || Antonio.transform.position.x >= 94)
        {
            rb.velocity = Vector2.zero; // Detener al enemigo
            if (enemigoBase.enemyDead && !isScheduledForDestruction)
            {
                audioSource.PlayOneShot(Morir);
                isScheduledForDestruction = true;
                Destroy(gameObject, 2f); // Destruir después de 4 segundos
            }
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            return;
        }

        // Perseguir al jugador
        Vector2 direction = (Antonio.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        // Cambiar la direcci�n del sprite seg�n la posici�n del jugador
        if (direction.x >= 0.0f) transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        else transform.localScale = new Vector3(-4.0f, 4.0f, 4.0f);

        float distance = Mathf.Abs(Antonio.transform.position.x - transform.position.x);

    }
}
