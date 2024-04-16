using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    private GameObject[] ranuras = new GameObject[3];
    private GameObject slot1;
    private GameObject slot2;
    private GameObject slot3;
    public bool[] estaLleno;
    private void Start(){
        slot1=GameObject.FindGameObjectWithTag("Slot1");
        slot2=GameObject.FindGameObjectWithTag("Slot2");
        slot3=GameObject.FindGameObjectWithTag("Slot3");
        ranuras[0]=slot1;
        ranuras[1]=slot2;
        ranuras[2]=slot3;
    }

    private void Update()
    {
        // Comprueba si se ha presionado alguna de las teclas 1, 2 o 3
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivarRanura(0); // Activar la primera ranura (0-indexada)
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivarRanura(1); // Activar la segunda ranura (0-indexada)
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivarRanura(2); // Activar la tercera ranura (0-indexada)
        }
    }

    public bool AgregarObjeto(GameObject objetoPrefab)
    {
        // Buscar una ranura vacía para agregar el objeto al inventario

        for (int i = 0; i < ranuras.Length; i++)
        {
            if (!estaLleno[i])
            {
                // Si se encuentra una ranura vacía, agregar el objeto al inventario
                estaLleno[i] = true;
                InstanciarBoton(objetoPrefab, ranuras[i]);
                return true; // Devolver true para indicar que el objeto se ha agregado correctamente
            }
        }

        // Si el inventario está lleno, devolver false
        return false;
    }

    private void InstanciarBoton(GameObject objetoPrefab, GameObject ranura)
    {
        // Instancia el botón del objeto en la ranura del inventario
        // (configura el botón según tus necesidades)
        GameObject botonObjeto = Instantiate(objetoPrefab, ranura.transform, false);
        // Asigna la ranura del inventario como padre del botón para que esté dentro de la ranura
        botonObjeto.transform.SetParent(ranura.transform, false);
    }

    private void ActivarRanura(int indiceRanura)
    {
        // Verificar si la ranura está llena
        if (estaLleno[indiceRanura])
        {
            // Obtener el botón de la ranura
            Button boton = ranuras[indiceRanura].GetComponentInChildren<Button>();
            if (boton != null)
            {
                GameObject objetoEnRanura = ranuras[indiceRanura].transform.GetChild(1).gameObject;
                if (objetoEnRanura != null)
                {
                // Obtener el tag del GameObject hijo en la ranura
                    string tagObjeto = objetoEnRanura.tag;
                    if (tagObjeto == "Consumible")
                    {
                    // Si el objeto es consumible, marcar la ranura como vacía
                        estaLleno[indiceRanura] = false;
                    }
                }
                // Simular el evento OnClick del botón
                boton.onClick.Invoke();
                
            }
            else
            {
                Debug.LogWarning("No se encontró ningún botón en la ranura " + indiceRanura);
            }
        }
        else
        {
            Debug.LogWarning("La ranura " + indiceRanura + " está vacía");
        }
    }
}
