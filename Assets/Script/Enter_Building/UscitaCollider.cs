using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UscitaCollider : MonoBehaviour
{
    [SerializeField] private string RoomAddress;

    public RoomManger RoomManger;
    public GameObject globalworld; 
    public Animation animazioneFade;

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            // Disabilita la stanza corrente tramite DisableCurrentRoom
            RoomManger.DisableCurrentRoom(RoomAddress);

            // Se l'oggetto globalworld non e nullo (cioe esiste)
            if (globalworld != null)
            {
                // Attiva l'oggetto globalworld 
                globalworld.SetActive(true);
            }

            // Riproduce l'animazione di fade
            animazioneFade.Play();

            Vector3 entryPosition = RoomManger.GetEntryPosition();
            entryPosition.y -= 1f;
            entryPosition.z = 0;
            collision.transform.position = entryPosition;
          
        }
    }
}
