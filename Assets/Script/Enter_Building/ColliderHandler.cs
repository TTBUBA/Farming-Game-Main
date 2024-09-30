using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandler : MonoBehaviour
{
    // Indirizzo della stanza da caricare
    [SerializeField] private string RoomAddress;
    // Riferimento al gestore delle stanze
    [SerializeField] private RoomManger roomManger;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject PointSpawnPlayer;
    public GameObject Globalworld;
    public Animation Animazione_fade;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            // Carica la stanza specificata dall'indirizzo
            roomManger.LoodRoom(RoomAddress);
            // Disattiva l'oggetto Globalworld
            Globalworld.SetActive(false);
            // Riproduce l'animazione
            Animazione_fade.Play();

            roomManger.SetPosition(transform.position);

            //Quando entro posiziona il player sulla posizione del PointSpawnPlayer
            Vector3 PlayerPosition = PointSpawnPlayer.transform.position;
            Player.transform.position = PlayerPosition;
        }
    }

    
}
