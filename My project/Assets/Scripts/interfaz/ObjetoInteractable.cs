using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractable : MonoBehaviour
{
    public Textos textos;

    private void OnMouseDown(){
        FindObjectOfType<ControlDialogos>().ActivarCartel(textos);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
