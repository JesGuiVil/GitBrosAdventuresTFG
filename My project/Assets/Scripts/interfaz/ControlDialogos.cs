using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlDialogos : MonoBehaviour
{
    private Animator anim;
    private Queue <string> colaDialogos;
    private Textos texto;
    [SerializeField] TextMeshProUGUI textoPantalla;
    // Start is called before the first frame update
    void Start()
    {
        anim=gameObject.GetComponent<Animator>();
        colaDialogos = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivarCartel (Textos textoObjeto){
        anim.SetBool("mostrar",true);
        texto=textoObjeto;
    }
    public void ActivaTexto(){
    colaDialogos.Clear();

    // Comprobación de nulidad para texto
    if (texto != null){
        // Comprobación de nulidad para texto.arrayTextos
        if (texto.arrayTextos != null){
            foreach (string textoGuardar in texto.arrayTextos){
                colaDialogos.Enqueue(textoGuardar);
            }
            SiguienteFrase();
        }    
    }  
    }

    public void SiguienteFrase(){
        if(colaDialogos.Count==0){
            cierraCartel();
            return;
        }
        string fraseActual = colaDialogos.Dequeue();
        textoPantalla.text=fraseActual;
    }
    public void cierraCartel(){
        anim.SetBool("mostrar",false);
        textoPantalla.text = "";
    }
}