using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Rogue;
    
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Rogue.transform.position.x;
        transform.position = position;
    }
}
