using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Transform destination;
    public bool isPortal1;
    public float distancia = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        if (isPortal1 == false)
         {
            destination = GameObject.FindGameObjectWithTag("Portal1").GetComponent<Transform>();
         } else
          {
            destination = GameObject.FindGameObjectWithTag("Portal2").GetComponent<Transform>();
          }
    }


    /// <param name="other"> </param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (Vector2.Distance(transform.position, other.transform.position) > distancia)
        {
            other.transform.position = new Vector2 (destination.position.x, destination.position.y);
        }
    }

}
