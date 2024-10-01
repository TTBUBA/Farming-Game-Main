using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class RoomManger : MonoBehaviour
{
    [SerializeField] private List<GameObject> Rooms;

    public GameObject Player;
    // Riferimento alla stanza corrente
    [SerializeField] private GameObject CurrentRoom;
    [SerializeField] private Vector3 entryPosition;

    public void ActivateRoom(string roomAddress)
    {
        foreach (GameObject room in Rooms)
        {
            //Controlla il nome Della stanza
            if(room.name == roomAddress)
            {
                if(CurrentRoom != null && CurrentRoom != room)
                {
                    room.SetActive(false);
                }

                room.SetActive(true);
                CurrentRoom = room;
                Debug.Log("stanza " + "=" + roomAddress);
            }
        }
    }

    public void DisableCurrentRoom(string roomAddress)
    {
        foreach (GameObject room in Rooms)
        {
            //Controlla il nome Della stanza
            if (room.name == roomAddress)
            {
                room.SetActive(false);
            }
        }
    }
    public void SetPosition(Vector3 position)
    {
        entryPosition = Player.transform.position;
    }

    public Vector3 GetEntryPosition()
    {
        return entryPosition;
    }

}
