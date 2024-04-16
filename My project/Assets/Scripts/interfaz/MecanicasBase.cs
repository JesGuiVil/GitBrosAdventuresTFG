using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanicasBase : MonoBehaviour
{
    public int contadorEnemigos;
    // Start is called before the first frame update
    void Start()
    {
        contadorEnemigos=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AumentarContadorEnemigos()
    {
        contadorEnemigos++;
    }
}
