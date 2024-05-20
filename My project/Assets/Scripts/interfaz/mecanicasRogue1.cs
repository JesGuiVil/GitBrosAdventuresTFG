using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicasRogue1 : MecanicasBase
{
    [SerializeField] private GameObject enemigoDrop;
    [SerializeField] private GameObject enemigoDrop2;
    [SerializeField] private GameObject enemigoDrop3;
    [SerializeField] private GameObject enemigo2;
    [SerializeField] private GameObject enemigo3;
    [SerializeField] private GameObject enemigo4;
    [SerializeField] private GameObject enemigo5;
    [SerializeField] private GameObject enemigo6;
    [SerializeField] private GameObject drop1;
    [SerializeField] private GameObject drop2;
    [SerializeField] private GameObject drop3;
    [SerializeField] private GameObject muro;
    [SerializeField] private GameObject palanca;

    private bool haDropado=false;
    private bool haDropado2=false;
    private bool haDropado3=false;
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
        if(!haDropado2){
            if (enemigoDrop2.GetComponent<EnemigoBase>().enemyDead && drop2 != null)
            {
            // Instanciar el objeto drop1 en la posición del enemigo1
            Instantiate(drop2, enemigoDrop2.transform.position, Quaternion.identity);
            haDropado2=true;
            }
        }
        if(!haDropado3){
            if (enemigoDrop3.GetComponent<EnemigoBase>().enemyDead && drop3 != null)
            {
            // Instanciar el objeto drop1 en la posición del enemigo1
            Instantiate(drop3, enemigoDrop3.transform.position, Quaternion.identity);
            haDropado3=true;
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
        }
        
    }
}
