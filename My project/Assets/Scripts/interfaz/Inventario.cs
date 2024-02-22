using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public GameObject[] ranuras;
    public bool[] estaLleno;

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
