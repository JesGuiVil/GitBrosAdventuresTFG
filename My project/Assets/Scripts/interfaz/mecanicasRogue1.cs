using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mecanicasRogue1 : MonoBehaviour
{
    [SerializeField] private GameObject enemigo1;
    [SerializeField] private GameObject drop1;

    private bool haDropado=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!haDropado){
            if (enemigo1.GetComponent<EnemigoBase>().enemyDead && drop1 != null)
            {
            // Instanciar el objeto drop1 en la posición del enemigo1
            Instantiate(drop1, enemigo1.transform.position, Quaternion.identity);
            haDropado=true;
            }
        }
        
    }
}
