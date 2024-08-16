using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandler : MonoBehaviour
{
    // Indirizzo della stanza da caricare
    public string RoomAddress;
    // Riferimento al gestore delle stanze
    private RoomManger roomManger;
    // Riferimento all'oggetto del mondo globale
    public GameObject Globalworld;
    // Riferimento all'animazione da riprodurre
    public Animation Animazione;

    // Start viene chiamato prima del primo frame update
    void Start()
    {
        // Trova un oggetto di tipo RoomManger nella scena e assegna il riferimento a roomManger
        roomManger = FindAnyObjectByType<RoomManger>();
    }

    // Metodo chiamato quando un altro collider entra nel trigger 2D di questo oggetto
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Controlla se il collider appartiene a un oggetto con il tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Carica la stanza specificata dall'indirizzo
            roomManger.LoodRoom(RoomAddress);
            // Disattiva l'oggetto Globalworld
            Globalworld.SetActive(false);
            // Riproduce l'animazione
            Animazione.Play();

            roomManger.SetPosition(transform.position);



        }
    }

    
}
