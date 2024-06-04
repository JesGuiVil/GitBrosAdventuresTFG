using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Transform destination;
    public string portalTag;
    public float distancia = 0.2f;
    public string playerTag = "Player";
    private bool puedeTeleport = false;
    private Collider2D playerCollider;

    //Diccionario para mapear el origen y los destinos de los portales
    private Dictionary<string, string> portalMap = new Dictionary<string, string>
    {
        { "Portal1", "Portal2"},
        { "Portal2", "Portal1"}, //Para el caso en el que quiera volver
        { "Portal3", "Portal4"},
        { "Portal4", "Portal3"},
        { "Portal5", "Portal6"},
        { "Portal6", "Portal5"},
        { "Portal7", "Portal8"},
        { "Portal8", "Portal7"},
        { "Portal9", "Portal10"},
        { "Portal10", "Portal9"},
        { "Portal11", "Portal12"},
        { "Portal12", "Portal11"}
    };

    // Start is called before the first frame update
    void Start()
    {
        if (portalMap.ContainsKey(portalTag))
        {
            string destinationTag = portalMap[portalTag];
            destination = GameObject.FindGameObjectWithTag(destinationTag).GetComponent<Transform>();
        }
        else
        {
            Debug.Log("Tag del portal no encontrado en el diccionario" + portalTag);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (Vector2.Distance(transform.position, other.transform.position) > distancia)
            {
                puedeTeleport = true;
                playerCollider = other;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            puedeTeleport = false;
            playerCollider = null;
        }
    }

    void Update()
    {
        if (puedeTeleport && playerCollider != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerCollider.transform.position = new Vector2(destination.position.x, destination.position.y);
                puedeTeleport = false;
            }
        }
    }

}
