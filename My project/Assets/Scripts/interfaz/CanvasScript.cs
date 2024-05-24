using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    private GameObject Player;
    private void Start(){
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        Vector3 position = transform.position;
        position.x = Player.transform.position.x;
        position.y = Player.transform.position.y;
        transform.position = position;
    }
}