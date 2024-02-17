using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CambiarScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex == 0)
            {
                cambiarEscena(1);
            }
            else if (currentSceneIndex == 1)
            {
                cambiarEscena(0);
            }
        }
    }

    public void cambiarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}