using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicasRogue1 : MecanicasBase
{
    [SerializeField] private GameObject enemigoDrop;
    [SerializeField] private GameObject enemigo2;
    [SerializeField] private GameObject enemigo3;
    [SerializeField] private GameObject enemigo4;
    [SerializeField] private GameObject enemigo5;
    [SerializeField] private GameObject enemigo6;
    [SerializeField] private GameObject pocion;
    [SerializeField] private GameObject drop1;
    [SerializeField] private GameObject muro;
    [SerializeField] private GameObject palanca;

    private bool haDropado=false;
    private bool muroDestruido=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!haDropado){
            if (enemigoDrop.GetComponent<EnemigoBase>().enemyDead && drop1 != null)
            {
            // Instanciar el objeto drop1 en la posición del enemigo1
            Instantiate(drop1, enemigoDrop.transform.position, Quaternion.identity);
            haDropado=true;
            }
        }
        if(contadorEnemigos>16 && !muroDestruido){
            Destroy(muro);
            muroDestruido=true;
        }
        if(palanca.GetComponent<PalancaBase>().isActivated){
            enemigo2.SetActive(true);
            enemigo3.SetActive(true);
            enemigo4.SetActive(true);
            enemigo5.SetActive(true);
            enemigo6.SetActive(true);
            pocion.SetActive(true);
        }
        
    }
}
