using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    //tiene traccia delle celle occupate 
    public Dictionary<Vector3Int, GameObject> occupiedTiles = new Dictionary<Vector3Int, GameObject>();

    public InventorySlot[] slots;  // Array degli slot dell'inventario
    private int selectedSlotIndex = 0;  // Indice dello slot attualmente selezionato
    public Transform plantPosition;  // Posizione nel mondo dove piantare il seme

    public Button PlatingSeed;  // Riferimento al bottone per piantare un seme tramite UI

    // Input GamePad
    public InputActionReference Plating_Pad;  // Riferimento all'azione del gamepad per piantare un seme
    public InputActionReference NextSlot_Pad;  // Riferimento all'azione del gamepad per selezionare lo slot successivo
    public InputActionReference PreviousSlot_Pad;  // Riferimento all'azione del gamepad per selezionare lo slot precedente

    //PlayerManager
    public Player_Manager PlayerManager;
    // Metodo chiamato all'inizio del gioco
    void Start()
    {
        UpdateSelectedSlot();  // Aggiorna lo slot selezionato all'inizio del gioco per riflettere la selezione iniziale
    }

    // Metodo chiamato ad ogni frame per controllare l'input della tastiera
    void Update()
    {
        // Controllo input da tastiera per selezionare uno specifico slot usando i tasti numerici
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(4);

        //Debug per vedere cosa contiene il dizionario
        //Debug.Log(string.Join(", ", occupiedTiles.Select(entry => $"{entry.Key}: {entry.Value.name}")));
    }

    // Metodo per selezionare uno slot specifico dato il suo indice
    void SelectSlot(int index)
    {
        selectedSlotIndex = index;  // Aggiorna l'indice dello slot selezionato
        UpdateSelectedSlot();  // Aggiorna la UI per riflettere il cambiamento
    }

    // Metodo per aggiornare la UI per mostrare lo slot selezionato
    void UpdateSelectedSlot()
    {
        for (int i = 0; i < slots.Length; i++)  // Itera su tutti gli slot
        {
            if (i == selectedSlotIndex)
            {
                slots[i].Select();  // Se lo slot è selezionato, applica lo stato selezionato
            }
            else
            {
                slots[i].Deselect();  // Se lo slot non è selezionato, applica lo stato deselezionato
            }
        }
    }

    // Metodo per piantare un seme dallo slot attualmente selezionato
    public void PlantSelectedSeed()
    {
        InventorySlot selectedSlot = slots[selectedSlotIndex];  // Ottieni il riferimento allo slot selezionato

        if (selectedSlot.quantity > 0)  // Controlla se ci sono semi disponibili in questo slot
        {
            

            // Controlla che il prefab del seme e la posizione siano assegnati
            if (selectedSlot.seedPrefab != null && plantPosition != null)
            {
                // Converte la posizione di piantagione in una cella (ad esempio su una Tilemap)
                Vector3Int cellPosition = Vector3Int.FloorToInt(plantPosition.position);

                // Controlla se la cella è già occupata
                if (!occupiedTiles.ContainsKey(cellPosition))
                {
                    selectedSlot.PlantSeed();  // Pianta un seme e riduci la quantità nello slot
                    // Instanzia il prefab del seme nella posizione designata
                    GameObject plantseed = Instantiate(selectedSlot.seedPrefab, plantPosition.position, Quaternion.identity);
                    occupiedTiles[cellPosition] = plantseed;
                    


                    // verifica che la coroutine Grow del seme inizia subito
                    plant plantscript = plantseed.GetComponent<plant>();
                    if (plantscript != null)
                    {
                        plantscript.cellPositionPlant = cellPosition;
                        plantscript.InventoryManager = this;
                        plantscript.StartCoroutine(plantscript.Grow());  // Avvia la crescita del seme
                    }
                    
                }
                else
                {
                    Debug.Log("Cella occupata");
                }

            }

        }
    }

    // Metodo per liberare la tile dal dizionario
    public void RemoveVegetableTile(Vector3Int cellPosition)
    {
        if (occupiedTiles.ContainsKey(cellPosition))
        {
            occupiedTiles.Remove(cellPosition);
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
            
            PlatingSeed.onClick.Invoke();  // Simula un clic sul bottone della UI
            PlantSelectedSeed();  // Esegue la piantagione del seme
        }
      
    }

    // Metodo per selezionare lo slot successivo con il controller
    private void NextSlot(InputAction.CallbackContext context)
    {
        selectedSlotIndex = (selectedSlotIndex + 1) % slots.Length;  // Incrementa l'indice e torna a zero se supera il numero di slot
        UpdateSelectedSlot();  // Aggiorna la UI per riflettere il nuovo slot selezionato
    }

    // Metodo per selezionare lo slot precedente con il controller
    private void PreviousSlot(InputAction.CallbackContext context)
    {
        selectedSlotIndex--;  // Decrementa l'indice dello slot
        if (selectedSlotIndex < 0) selectedSlotIndex = slots.Length - 1;  // Se l'indice diventa negativo, torna all'ultimo slot
        UpdateSelectedSlot();  // Aggiorna la UI per riflettere il nuovo slot selezionato
    }
}
