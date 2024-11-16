using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    // Slot degli ortaggi
    public Slot_Vegetable[] VegetableSlots;
    public Slot_Vegetable currentSelectedSlot { get; private set; } // slot vegetable

    public Transform plantPosition; // Posizione dove piantare il seme
    //Dizionario che traccia le celle occupate (Vector3Int) agli oggetti (GameObject) corrispondenti.
    public Dictionary<Vector3Int, GameObject> occupiedTiles = new Dictionary<Vector3Int, GameObject>(); 
    public GameObject[] plantGameObjects;


    [Header("INPUT CONTROLLER")]
    public InputActionReference plant_Controller;

    [Header("INPUT KEYBOARD")]
    public InputActionReference plant_Keyboard;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DEBUG();
        }
    }

    private void DEBUG()
    {
        //VERIFICA QUALE CELLA E OCCUPATA 
        foreach(KeyValuePair<Vector3Int, GameObject> entry in occupiedTiles)
        { 
            Debug.Log($" cella {entry.Key} oggetto {entry.Value.name}");
        }
    }
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

    //===========INPUT SETTING===========//
    private void OnEnable()
    {
        //Keyboard
        // Abilita l'azione della zappa
        plant_Keyboard.action.Enable();
        plant_Keyboard.action.started += Plant_Keyboard;

        //Controller
        // Abilita l'azione della zappa
        plant_Controller.action.Enable();
        plant_Controller.action.started += Plant_Controller;
    }

    private void OnDisable()
    {
        //Keyboard
        plant_Keyboard.action.Disable();
        plant_Keyboard.action.started -= Plant_Keyboard;

        //Controller
        plant_Controller.action.Disable();
        plant_Controller.action.started -= Plant_Controller;
    }

    //===========INPUT KEYBOARD===========//
    private void Plant_Keyboard(InputAction.CallbackContext context)
    {
        PlantSelectedSeed();
    }

    //===========INPUT CONTROLLER===========//
    private void Plant_Controller(InputAction.CallbackContext context)
    {
        PlantSelectedSeed();
    }
}
