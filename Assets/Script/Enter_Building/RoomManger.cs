using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class RoomManger : MonoBehaviour
{
    // Riferimento alla stanza corrente
    private GameObject CurrentRoom;
    // Riferimento al giocatore
    public GameObject Player;
    // Distanza offset per il posizionamento della nuova stanza
    public float Xasset = 10f;
    public float Yasset = 5f;

    public Vector3 entryPosition;


    // Metodo per caricare una stanza dall'indirizzo specificato
    public void LoodRoom(string roomAdress)
    {
        Addressables.LoadAssetAsync<GameObject>(roomAdress).Completed += OnroomLoad;
    }

    // Metodo callback chiamato quando il caricamento della stanza è completato
    private void OnroomLoad(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            // Se esiste una stanza corrente, distruggila
            if (CurrentRoom != null)
            {
                Destroy(CurrentRoom);
            }
            // Posiziona la nuova stanza 
            Vector3 RoomPosition = new Vector2(-200,300);
            CurrentRoom = Instantiate(obj.Result,  RoomPosition , Quaternion.identity);

            MovePlayerToNewRoom(RoomPosition);
        }
        else
        {
            Debug.Log("Caricamento stanza fallito");
        }
    }

    public void MovePlayerToNewRoom(Vector3 roomPosition)
    {
        Vector3 playerposition = new Vector3(-212 , 293, 0);
        Player.transform.position = playerposition;
    }

    public void SetPosition(Vector3 position)
    {
        entryPosition = Player.transform.position;
    }

    public Vector3 GetEntryPosition()
    {
        return entryPosition;
    }
    // Metodo per disabilitare la stanza corrente
    public void DisableCurrentRoom()
    {
        if (CurrentRoom != null)
        {
            CurrentRoom.SetActive(false);
        }
    }
}
