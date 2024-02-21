using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour
{
    private Inventario inventario;
    public int i;
    // Start is called before the first frame update
    void Start()
    {
        inventario = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
    }

    // Update is called once per frame
    void Update()
    {
       if(transform.childCount <= 0)
        {
            inventario.estaLleno[i] = false;
        } 
    }
}