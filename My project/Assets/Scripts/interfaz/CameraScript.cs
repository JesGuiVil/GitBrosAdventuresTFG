using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject Personaje;


    private void Start(){
        Personaje=GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        Vector3 position = transform.position;
        position.x = Personaje.transform.position.x;
        position.y = Personaje.transform.position.y;
        transform.position = position;
    }
}
