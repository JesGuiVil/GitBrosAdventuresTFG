using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicasAssassin1 : MecanicasBase
{
    [SerializeField] private GameObject enemigoDrop;
    [SerializeField] private GameObject enemigoDropDos;
    [SerializeField] private GameObject drop1;
    [SerializeField] private GameObject drop2;
    private bool Dropado = false;
    private bool DropadoDos = false;
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
                // Instanciar el objeto drop1 en la posición del enemigo1
                Instantiate(drop1, enemigoDrop.transform.position, Quaternion.identity);
                Dropado = true;
               
            }
        }
        if (!DropadoDos)
        {
            if (enemigoDropDos.GetComponent<EnemigoBase>().enemyDead && drop2 != null)
            {
                // Instanciar el objeto drop1 en la posición del enemigo1
                Instantiate(drop2, enemigoDropDos.transform.position, Quaternion.identity);
                DropadoDos = true;

            }
        }
    }
}
