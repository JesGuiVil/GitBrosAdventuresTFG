using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EsqueletoScript : MonoBehaviour
{
    private Animator Animator;
    public GameObject BulletPrefab;
    private int Health = 1;
    private Rigidbody2D rb;
    public float speed; // Velocidad a la que el enemigo se mueve
    private GameObject Antonio; // Referencia al personaje

    private void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Buscar al personaje en la escena
        Antonio = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Antonio == null) return;

        // Perseguir al jugador
        Vector2 direction = (Antonio.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;

        // Cambiar la dirección del sprite según la posición del jugador
        if (direction.x >= 0.0f) transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        else transform.localScale = new Vector3(-4.0f, 4.0f, 4.0f);

        float distance = Mathf.Abs(Antonio.transform.position.x - transform.position.x);


        // Control de la animación
        if (Health <= 0)
        {
            Animator.SetBool("muerto", true);
        }
        else
        {
            Animator.SetBool("muerto", false);
        }
    }



    public void Hit()
    {
        Health -= 1;
        if (Health == 0)
        {
            Animator.SetBool("muerto", true);
            StartCoroutine(DestruirDespuesDe(0.3f));
        }

        IEnumerator DestruirDespuesDe(float duracion)
        {

            yield return new WaitForSeconds(duracion);

            // Destruir el GameObject después de la espera
            Destroy(gameObject);
        }
    }
}
