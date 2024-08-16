using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UscitaCollider : MonoBehaviour
{
    // Riferimento al gestore delle stanze (RoomManger)
    private RoomManger RoomAddress;
    // Riferimento all'oggetto del mondo globale
    public GameObject globalworld;
    // Riferimento all'animazione di fade
    public Animation animazioneFade;
    // Riferimento a un oggetto figlio
    private GameObject childObject;

    
    void Start()
    {
        // Trova un oggetto di tipo RoomManger nella scena e assegna il riferimento a RoomAddress
        RoomAddress = FindAnyObjectByType<RoomManger>();
        // Trova l'oggetto con il tag "Globalworld" e assegna il riferimento a globalworld
        globalworld = GameObject.FindWithTag("Globalworld");
        // Trova il figlio dell'oggetto globalworld chiamato "Test" e assegna il valore a childObject
        childObject = globalworld.transform.Find("Word").gameObject;
    }

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Controlla se il collider appartiene a un oggetto con il tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Disabilita la stanza corrente tramite il gestore delle stanze
            RoomAddress.DisableCurrentRoom();

            // Se l'oggetto globalworld non è nullo (cioè esiste)
            if (globalworld != null)
            {
                // Attiva l'oggetto globalworld e il suo figlio childObject
                globalworld.SetActive(true);
                childObject.SetActive(true);
            }
            else
            {
                // Logga un messaggio di debug se globalworld non è stato trovato
                Debug.Log("non trovato");
            }

            // Riproduce l'animazione di fade
            animazioneFade.Play();


            Vector3 entryPosition = RoomAddress.GetEntryPosition();
            entryPosition.y -= 4f;
            entryPosition.z = 0;
            collision.transform.position = entryPosition;
          
        }
    }
}
