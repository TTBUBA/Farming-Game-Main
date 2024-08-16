using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;  // Array di slot dell'inventario
    private int selectedSlotIndex = 0;  // L'indice dello slot attualmente selezionato
    public Transform plantPosition;  // Posizione dove piantare il seme

    
    // Metodo chiamato all'inizio del gioco
    void Start()
    {
        
        UpdateSelectedSlot();  // Aggiorna lo slot selezionato all'inizio del gioco
    }

    // Metodo chiamato ad ogni frame per controllare l'input della tastiera
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);  // Se viene premuto '1', seleziona il primo slot
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);  // Se viene premuto '2', seleziona il secondo slot
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);  // Se viene premuto '3', seleziona il terzo slot
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);  // Se viene premuto '4', seleziona il quarto slot
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(4);  // Se viene premuto '5', seleziona il quinto slot
    }

    // Metodo per selezionare uno slot specifico
    void SelectSlot(int index)
    {
        selectedSlotIndex = index;  // Aggiorna l'indice dello slot selezionato
        UpdateSelectedSlot();  // Aggiorna la UI per riflettere la selezione
    }

    // Metodo per aggiornare lo stato di selezione degli slot
    void UpdateSelectedSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i == selectedSlotIndex)
            {
                slots[i].Select();  // Se lo slot è selezionato, imposta il background a rosso
            }
            else
            {
                slots[i].Deselect();  // Se lo slot non è selezionato, imposta il background a bianco
            }

        }
    }

    // Metodo per piantare un seme dallo slot selezionato
    public void PlantSelectedSeed()
    {
        InventorySlot selectedSlot = slots[selectedSlotIndex];

        if (selectedSlot.quantity > 0)  // Controlla se ci sono semi disponibili
        {
            selectedSlot.PlantSeed();  // Pianta un seme dallo slot selezionato

            // Instanzia il prefab del seme piantato nella posizione designata
            if (selectedSlot.seedPrefab != null && plantPosition != null)
            {
                GameObject plantseed =  Instantiate(selectedSlot.seedPrefab, plantPosition.position, Quaternion.identity);

                // Assicurati che la coroutine Grow inizi subito
                plant plantscript = plantseed.GetComponent<plant>();
                if(plantscript != null)
                {
                    plantscript.StartCoroutine(plantscript.Grow());
                }
            }
            else
            {
                Debug.LogError("Seed prefab or plant position is not assigned.");
            }
        }
    }
}
