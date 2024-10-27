//gestisce lo slot dinamico e la logica per piantare il seme selezionato


using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    private InventorySlot currentSelectedSlot;
    public Transform plantPosition; // Posizione dove piantare il seme
    public Dictionary<Vector3Int, GameObject> occupiedTiles = new Dictionary<Vector3Int, GameObject>();
    public GameObject[] plantGameObjects;

    public void SetCurrentSelectedSlot(InventorySlot selectedSlot)
    {
        currentSelectedSlot = selectedSlot;
    }
    // Pianta il seme selezionato
    public void PlantSelectedSeed()
    {
        //InventorySlot selectedSlot = inventorySlots[Random.Range(0, inventorySlots.Length)];

        //InventorySlot selectedSlot = inventorySlots[inventorySlots.Length - 1];
        

        Vector3Int cellPosition = Vector3Int.FloorToInt(plantPosition.position);

        if (!occupiedTiles.ContainsKey(cellPosition))
        {
            Debug.Log("ortaggio piantato");

            currentSelectedSlot.PlantSeed();
            Plant plant = GetPlantAtPosition(cellPosition)?.GetComponent<Plant>();

            if (plant != null)
            {
                plant.StartGrowth(currentSelectedSlot);
                occupiedTiles[cellPosition] = plant.gameObject;
            }
        }
        else
        {
            Debug.Log("Cella occupata");
        }
 
    }

    // Ottiene la pianta alla posizione data
    private GameObject GetPlantAtPosition(Vector3Int position)
    {
        foreach (GameObject plant in plantGameObjects)
        {
            if (Vector3Int.FloorToInt(plant.transform.position) == position)
            {
                return plant;
            }
        }
        return null;
    }

    public void RemoveVegetableTile(Vector3Int cellPosition)
    {
        if (occupiedTiles.ContainsKey(cellPosition))
        {
            occupiedTiles.Remove(cellPosition);
        }
    }
}


