using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBase : MonoBehaviour
{
    [SerializeField] private float vidaEnemigo;
    [SerializeField] private float maximoVidaEnemigo;
    // Start is called before the first frame update
    void Start()
    {
        vidaEnemigo = maximoVidaEnemigo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void enemigoRecibirDanio(float danio)
    {
        vidaEnemigo -= danio;
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }
}
