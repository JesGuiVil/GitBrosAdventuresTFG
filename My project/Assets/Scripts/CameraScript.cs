using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Rogue;
    public GameObject Archer;
    public GameObject Assassin;

    void Update()
    {
        Vector3 position = transform.position;
        position.x = Rogue.transform.position.x;
        position.x = Archer.transform.position.x;
        position.x = Assassin.transform.position.x;
        transform.position = position;
    }
}
