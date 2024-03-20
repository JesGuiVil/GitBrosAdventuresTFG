using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject PlataformasMoviles;
    public Transform uno;
    public Transform dos;
    public float velocidad;
    private Vector3 moverhacia;
    private PalancaPlataforma palanca;
    // Start is called before the first frame update
    void Start()
    {
        palanca= GameObject.FindGameObjectWithTag("Palanca").GetComponent<PalancaPlataforma>();
        if (palanca.isActivated)
        {
            moverhacia = dos.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (palanca.isActivated)
        {
            PlataformasMoviles.transform.position = Vector3.MoveTowards(PlataformasMoviles.transform.position, moverhacia, velocidad * Time.deltaTime);


            if (PlataformasMoviles.transform.position == dos.position)
            {
                moverhacia = uno.position;
            }
            if (PlataformasMoviles.transform.position == uno.position)
            {
                moverhacia = dos.position;
            }
        }
       
    }
}
