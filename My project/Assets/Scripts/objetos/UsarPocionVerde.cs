using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsarPocionVerde : MonoBehaviour
{
    private GameObject pinchosTilemap;

    // Start is called before the first frame update
    void Start()
    {
        pinchosTilemap = GameObject.FindGameObjectWithTag("pinchos"); //Busca el objeto Tilemap cuyo tag sea 'pinchos'
    }

    public void Use()
    {
        if (pinchosTilemap != null) {
            pinchosTilemap.SetActive(false); //Se desactiva el tilemap
        }
        Destroy(gameObject); //Se destruye la poción tras usarla
    }
}