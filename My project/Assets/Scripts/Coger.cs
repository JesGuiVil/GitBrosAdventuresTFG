using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coger : MonoBehaviour
{
    private Inventario inventory;
    public GameObject itemButton;

    private void Start(){
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            for (int i=0; i<inventory.ranuras.Length; i++){
                if (inventory.estaLleno[i]==false){
                    inventory.estaLleno[i]=true;
                    Instantiate(itemButton, inventory.ranuras[i].transform,false);
                    Destroy(gameObject);
                    break;
                }  
            }
        }
    }

}