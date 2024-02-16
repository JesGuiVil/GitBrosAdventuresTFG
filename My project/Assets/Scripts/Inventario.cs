using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public GameObject[] ranuras;
    public bool[] estaLleno;
    
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
}