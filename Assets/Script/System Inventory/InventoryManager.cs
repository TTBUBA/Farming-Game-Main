using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    // Hotbar e slot dinamico
    public InventorySlot[] hotbarSlots;  // Hotbar fissa con 4 slot
    public InventorySlot dynamicSlot;  // Slot dinamico per semi o strumenti

    [SerializeField] private int selectedHotbarSlotIndex = 0;  // Indice dello slot selezionato nella hotbar
    public Transform plantPosition;  // Posizione dove piantare il seme

    // Dati di input per il controller/gamepad
    public InputActionReference Plating_Pad;  // Azione di piantare con gamepad
    public InputActionReference NextSlot_Pad;  // Slot successivo nella hotbar
    public InputActionReference PreviousSlot_Pad;  // Slot precedente nella hotbar

    // Gestione delle celle occupate dalle piante
    public Dictionary<Vector3Int, GameObject> occupiedTiles = new Dictionary<Vector3Int, GameObject>();

    // Lista dei prefab delle piante per posizionarle in game
    public GameObject[] plantGameObjects;

    // Riferimento al manager del giocatore
    public Player_Manager PlayerManager;

    // Inizializza le azioni del gamepad
    void OnEnable()
    {
        Plating_Pad.action.started += PlantSeedGamePad;
        Plating_Pad.action.Enable();

        NextSlot_Pad.action.started += NextSlot;
        PreviousSlot_Pad.action.started += PreviousSlot;
        NextSlot_Pad.action.Enable();
        PreviousSlot_Pad.action.Enable();
    }

    // Disabilita le azioni per evitare memory leak
    void OnDisable()
    {
        Plating_Pad.action.started -= PlantSeedGamePad;
        Plating_Pad.action.Disable();

        NextSlot_Pad.action.started -= NextSlot;
        PreviousSlot_Pad.action.started -= PreviousSlot;
        NextSlot_Pad.action.Disable();
        PreviousSlot_Pad.action.Disable();
    }

    // Metodo chiamato ad ogni frame per controllare l'input della tastiera
    void Update()
    {
        ChangeHotbarSlot();  // Controlla la selezione degli slot della hotbar
        DEBUG();  // Debug per il dizionario delle celle occupate
    }

    // Metodo per cambiare lo slot della hotbar con tastiera
    public void ChangeHotbarSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectHotbarSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectHotbarSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectHotbarSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectHotbarSlot(3);
    }

    // Seleziona uno slot della hotbar
    void SelectHotbarSlot(int index)
    {
        foreach (var slot in hotbarSlots)
        {
            slot.Deselect();
        }

        selectedHotbarSlotIndex = index;
        hotbarSlots[selectedHotbarSlotIndex].Select();
    }

    // Metodo per piantare un seme dallo slot selezionato
    public void PlantSelectedSeed()
    {
        InventorySlot selectedSlot = hotbarSlots[selectedHotbarSlotIndex];  // Ottieni lo slot selezionato

        if (selectedSlot.vegetableData != null && selectedSlot.vegetableData.quantity > 0)  // Controlla se ci sono semi disponibili
        {
            if (plantPosition != null)
            {
                Vector3Int cellPosition = Vector3Int.FloorToInt(plantPosition.position);

                // Se la cella non è occupata, pianta il seme
                if (!occupiedTiles.ContainsKey(cellPosition))
                {
                    selectedSlot.PlantSeed();  // Riduce la quantità di semi
                    Plant plant = GetPlantAtPosition(cellPosition)?.GetComponent<Plant>();

                    if (plant != null)
                    {
                        plant.StartGrowth(selectedSlot);  // Avvia la crescita della pianta
                        occupiedTiles[cellPosition] = plant.gameObject;  // Memorizza la pianta nella cella occupata
                    }
                }
                else
                {
                    Debug.Log("Cella occupata");
                }
            }
        }
    }

    // Metodo per ottenere la pianta corrispondente alla posizione
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

    // Metodo per mostrare il contenuto del dizionario delle celle occupate
    public void DEBUG()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log(string.Join(", ", occupiedTiles.Select(entry => $"{entry.Key}: {entry.Value.name}")));
        }
    }

    // Metodo per piantare un seme con il gamepad
    public void PlantSeedGamePad(InputAction.CallbackContext context)
    {
        if (PlayerManager.PuoiPiantare == true)
        {
            PlantSelectedSeed();
        }
    }

    // Metodo per selezionare lo slot successivo con il controller
    private void NextSlot(InputAction.CallbackContext context)
    {
        selectedHotbarSlotIndex = (selectedHotbarSlotIndex + 1) % hotbarSlots.Length;
        SelectHotbarSlot(selectedHotbarSlotIndex);
    }

    // Metodo per selezionare lo slot precedente con il controller
    private void PreviousSlot(InputAction.CallbackContext context)
    {
        selectedHotbarSlotIndex--;
        if (selectedHotbarSlotIndex < 0) selectedHotbarSlotIndex = hotbarSlots.Length - 1;
        SelectHotbarSlot(selectedHotbarSlotIndex);
    }

    // Metodo per mostrare lo slot dinamico quando necessario
    public void ShowDynamicSlot(List<VegetableData> availableSeeds)
    {
        dynamicSlot.gameObject.SetActive(true);  // Mostra lo slot dinamico
        // Logica per selezionare il seme o strumento disponibile
    }
}
