using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    // Slot degli ortaggi
    public Slot_Vegetable[] inventorySlots;
    private Slot_Vegetable currentSelectedSlot;

    public Transform plantPosition; // Posizione dove piantare il seme
    //Dizionario che traccia le celle occupate (Vector3Int) agli oggetti (GameObject) corrispondenti.
    public Dictionary<Vector3Int, GameObject> occupiedTiles = new Dictionary<Vector3Int, GameObject>(); 
    public GameObject[] plantGameObjects;

    public void SetCurrentSelectedSlot(Slot_Vegetable selectedSlot)
    {
        currentSelectedSlot = selectedSlot;
    }

    // Pianta il seme selezionato
    public void PlantSelectedSeed()
    {
        Vector3Int cellPosition = Vector3Int.FloorToInt(plantPosition.position);
        Plant plant = GetPlantAtPosition(cellPosition)?.GetComponent<Plant>();

        if (plant != null && plant.IsPlanting) // Verifica se la pianta esiste e se è in fase di piantagione
        {
            if (!occupiedTiles.ContainsKey(cellPosition))
            {
                Debug.Log("Ortaggio piantato");
                currentSelectedSlot.PlantSeed();
                plant.StartGrowth(currentSelectedSlot);
                occupiedTiles[cellPosition] = plant.gameObject;
            }
            else
            {
                Debug.Log("Cella occupata");
            }
        }
    }

    // Ottiene la pianta alla posizione data
    public GameObject GetPlantAtPosition(Vector3Int position)
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
