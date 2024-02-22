using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    private GameObject Rogue;
    private void Start(){
        Rogue = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        Vector3 position = transform.position;
        position.x = Rogue.transform.position.x;
        position.y = Rogue.transform.position.y;
        transform.position = position;
    }
}