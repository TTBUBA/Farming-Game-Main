using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;


public class InventoryManager : MonoBehaviour
{
    //tiene traccia delle celle occupate 
    public Dictionary<Vector3Int, GameObject> occupiedTiles = new Dictionary<Vector3Int, GameObject >();

    public GameObject[] plantGameObjects;
    public InventorySlot[] slots;  // Array degli slot dell'inventario
    [SerializeField] private int selectedSlotIndex = 0;  // Indice dello slot attualmente selezionato
    public Transform plantPosition;  // Posizione nel mondo dove piantare il seme

    // Input GamePad
    public InputActionReference Plating_Pad;  // Riferimento all'azione del gamepad per piantare un seme
    public InputActionReference NextSlot_Pad;  // Riferimento all'azione del gamepad per selezionare lo slot successivo
    public InputActionReference PreviousSlot_Pad;  // Riferimento all'azione del gamepad per selezionare lo slot precedente

    //PlayerManager
    public Player_Manager PlayerManager;

    // Metodo chiamato ad ogni frame per controllare l'input della tastiera
    void Update()
    {
        ChangeSlot();
        DEBUG();
    }
    public void ChangeSlot()
    {
        // Controllo input da tastiera per selezionare uno specifico slot usando i tasti numerici
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(4);
    }

    // Metodo per selezionare uno slot specifico dato il suo indice
    void SelectSlot(int index)
    {
        // Deseleziona tutti gli slot
        foreach (var slot in slots)
        {
            slot.Deselect();
        }

        // Aggiorna l'indice dello slot selezionato
        selectedSlotIndex = index;

        // Seleziona il nuovo slot
        slots[selectedSlotIndex].Select();
    }

    public void PlantSelectedSeed()
    {
        InventorySlot selectedSlot = slots[selectedSlotIndex];  // Ottieni il riferimento allo slot selezionato

        if (selectedSlot.quantity > 0)  // Controlla se ci sono semi disponibili in questo slot
        {
            // Controlla che la posizione di piantagione sia assegnata
            if (plantPosition != null)
            {
                // Converte la posizione di piantagione in una cella
                Vector3Int cellPosition = Vector3Int.FloorToInt(plantPosition.position);

                // Trova il GameObject della pianta corrispondente
                GameObject selectedPlantObject = GetPlantAtPosition(cellPosition);

                // Controlla se la cella è già occupata
                if (!occupiedTiles.ContainsKey(cellPosition))
                {
                    // Riduci la quantità nello slot e aggiorna la cella
                    selectedSlot.PlantSeed();

                    // Ottieni il GameObject attuale su cui piantare
                    GameObject selectedTile = plantPosition.gameObject;
                 
                    Plant vegetable = selectedPlantObject.GetComponent<Plant>();

                    if (vegetable != null)
                    {
                        // Passa i dati dallo slot alla pianta
                        vegetable.StartGrowth(selectedSlot);

                        // Memorizza la pianta nella cella occupata
                        occupiedTiles[cellPosition] = selectedTile;
                        vegetable.cellPositionPlant = cellPosition;
                        vegetable.InventoryManager = this;
                        
                    }


                }
                else
                {
                    Debug.Log("Cella occupata");
                }
            }
        }
    }

    // Metodo per ottenere il GameObject di una pianta in base alla posizione
    private GameObject GetPlantAtPosition(Vector3Int position)
    {
        // Cicla attraverso tutti i GameObject che rappresentano le piante
        foreach (GameObject plant in plantGameObjects)
        {
            // Confronta la posizione della pianta con la posizione data (convertita in un Vector3Int)
            if (Vector3Int.FloorToInt(plant.transform.position) == position)
            {
                // Se la posizione combacia, ritorna il GameObject della pianta
                return plant;
            }
        }
        // Se non c'è nessuna pianta alla posizione specificata, ritorna null
        return null;
    }

    // Metodo per liberare la tile dal dizionario
    public void RemoveVegetableTile(Vector3Int cellPosition)
    {
        if (occupiedTiles.ContainsKey(cellPosition))
        {
            occupiedTiles.Remove(cellPosition);
        }
    }

    public void DEBUG()
    {
        //Debug per vedere cosa contiene il dizionario
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log(string.Join(", ", occupiedTiles.Select(entry => $"{entry.Key}: {entry.Value.name}")));
        }
    }


    //======INPUT CONTROLLER======//
    void OnEnable()
    {
        // Collega l'evento di piantare un seme all'azione del gamepad
        Plating_Pad.action.started += PlantSeedGamePad;
        Plating_Pad.action.Enable();  // Abilita l'azione del gamepad per piantare

        // Collega gli eventi per navigare tra gli slot con il gamepad
        NextSlot_Pad.action.started += NextSlot;
        PreviousSlot_Pad.action.started += PreviousSlot;
        NextSlot_Pad.action.Enable();  // Abilita l'azione per lo slot successivo
        PreviousSlot_Pad.action.Enable();  // Abilita l'azione per lo slot precedente
    }
    void OnDisable()
    {
        // Scollega gli eventi per evitare memory leak o chiamate non volute
        Plating_Pad.action.started -= PlantSeedGamePad;
        Plating_Pad.action.Disable();  // Disabilita l'azione del gamepad per piantare

        NextSlot_Pad.action.started -= NextSlot;
        PreviousSlot_Pad.action.started -= PreviousSlot;
        NextSlot_Pad.action.Disable();  // Disabilita l'azione per lo slot successivo
        PreviousSlot_Pad.action.Disable();  // Disabilita l'azione per lo slot precedente
    }
    // Metodo per piantare un seme utilizzando il gamepad
    public void PlantSeedGamePad(InputAction.CallbackContext Obj)
    {
        if (PlayerManager.PuoiPiantare == true)
        {
            PlantSelectedSeed();  // Esegue la piantagione del seme
        }
      
    }

    // Metodo per selezionare lo slot successivo con il controller
    private void NextSlot(InputAction.CallbackContext context)
    {
        selectedSlotIndex = (selectedSlotIndex + 1) % slots.Length;  // Incrementa l'indice e torna a zero se supera il numero di slot
        SelectSlot(selectedSlotIndex);  // Aggiorna la UI per riflettere il nuovo slot selezionato
    }

    // Metodo per selezionare lo slot precedente con il controller
    private void PreviousSlot(InputAction.CallbackContext context)
    {
        selectedSlotIndex--;  // Decrementa l'indice dello slot
        if (selectedSlotIndex < 0) selectedSlotIndex = slots.Length - 1;  // Se l'indice diventa negativo, torna all'ultimo slot
        SelectSlot(selectedSlotIndex);  // Aggiorna la UI per riflettere il nuovo slot selezionato
    }
}
