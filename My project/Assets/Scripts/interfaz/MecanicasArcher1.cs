using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicasArcher1 : MecanicasBase
{
    [SerializeField] private GameObject enemigoDrop;
    [SerializeField] private GameObject drop1;
    private bool Dropado = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Dropado)
        {
            if (enemigoDrop.GetComponent<EnemigoBase>().enemyDead && drop1 != null)
            {
                // Instanciar el objeto drop1 en la posici�n del enemigo1
                Instantiate(drop1, enemigoDrop.transform.position, Quaternion.identity);
                Dropado = true;
               
            }
        }
        
        
    }
}
