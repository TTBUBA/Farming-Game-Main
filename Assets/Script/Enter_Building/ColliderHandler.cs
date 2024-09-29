using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandler : MonoBehaviour
{
    // Indirizzo della stanza da caricare
    public string RoomAddress;
    // Riferimento al gestore delle stanze
    private RoomManger roomManger;

    public GameObject Globalworld;
    public Animation Animazione_fade;

    void Start()
    {
        // Trova un oggetto di tipo RoomManger nella scena e assegna il riferimento a roomManger
        roomManger = FindAnyObjectByType<RoomManger>();
    }

    
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
            Animazione_fade.Play();

            roomManger.SetPosition(transform.position);

        }
    }

    
}
