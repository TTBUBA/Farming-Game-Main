using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class ToolEffectManager : MonoBehaviour
{
    [Header("Hoe")]
    public Tilemap tilemapTerrain; // Riferimento alla mappa dei tile
    public Tile Newtile; // Tile da impostare quando si utilizza la zappa
    public GameObject PointSpawn; // Punto di riferimento per il posizionamento degli oggetti

    public Player_Manager PlayerCollision; // Riferimento al gestore delle collisioni del giocatore
    public Slot_Tools[] SlotTools; // Array di slot degli strumenti
    public InventoryManager inventoryManager; // Riferimento al gestore dell'inventario
    public HotbarManager hotbarManager; // Riferimento al gestore della hotbar

    [Header("INPUT KEYBOARD")]
    public InputActionReference hole_Keyboard;

    [Header("INPUT CONTROLLER")]
    public InputActionReference hole_Controller;

    [Header("UI KEYBOARD")]
    public GameObject input_keyboard;

    [Header("UI CONTROLLER")]
    public GameObject input_Controller;

    private bool canUseTool = false; // Indica se lo strumento può essere usato
    private string currentToolName; // Nome dello strumento attualmente in mano
    public bool isUsingHoe = false;
    private bool isUsingPickAxe = false;
    private bool isUsingAxe = false;

    public PlayerInput playerInput;
    public GameObject Button_Plant;
    void Update()
    {
        CheckCurrentTool(); // Controlla quale strumento il giocatore ha in mano
        CheckToolAvailability(); // Verifica la possibilità di usare lo strumento
        CheakDevice();// Verifica quale dispositivo stai usando
    }

    // Controlla quale strumento il giocatore ha in mano
    private void CheckCurrentTool()
    {
        // Ottiene lo slot attualmente selezionato nella hotbar
        Slot_Tools currentSlot = hotbarManager.hotbarSlots[hotbarManager.selectedHotbarSlotIndex];

        // Ottiene il nome dello strumento attivo
        currentToolName = currentSlot.toolsData.NameTools;

        // Imposta i flag di utilizzo dello strumento in base al nome
        isUsingHoe = currentToolName == "hoe";
    }

    // Controlla se il giocatore può usare lo strumento attivo
    private void CheckToolAvailability()
    {
        // Ottiene lo slot selezionato dalla hotbar
        Slot_Tools slotTools = hotbarManager.hotbarSlots[hotbarManager.selectedHotbarSlotIndex];

        // Verifica se il giocatore è in un'area corretta per usare lo strumento selezionato
        if (PlayerCollision.CurrentCollisiontag == slotTools.toolsData.AreaUsing)
        {
            // Abilita l'uso dello strumento se l'area è corretta
            canUseTool = true;
        }
        else
        {
            // Disabilita l'uso dello strumento se l'area non è corretta
            canUseTool = false;
        }
    }

    private void CheakDevice()
    {
        Slot_Tools slotTools = hotbarManager.hotbarSlots[hotbarManager.selectedHotbarSlotIndex];

        // Verifica se il giocatore è in un'area corretta per usare lo strumento selezionato
        if (PlayerCollision.CurrentCollisiontag == slotTools.toolsData.AreaUsing)
        {
            Button_Plant.SetActive(true);
            foreach (var device in playerInput.devices)
            {
                if (device is Keyboard)
                {
                    input_keyboard.SetActive(true);
                    input_Controller.SetActive(false);
                    
                }
                else if (device is Gamepad)
                {
                    input_Controller.SetActive(true);
                    input_keyboard.SetActive(false);
                }
            }
        }
        else
        {
            input_Controller.SetActive(false);
            input_keyboard.SetActive(false);
            Button_Plant.SetActive(false);
        }

    }
    public void ChangeTile()
    {
        // Ottiene la cella attuale sulla mappa in base alla posizione del punto di riferimento
        Vector3Int currentCell = tilemapTerrain.WorldToCell(PointSpawn.transform.position);

        // Ottiene la pianta presente nella cella corrente, se esiste
        Plant plant = inventoryManager.GetPlantAtPosition(currentCell)?.GetComponent<Plant>();

        // Imposta il nuovo tile nella mappa
        tilemapTerrain.SetTile(currentCell, Newtile);

        // Se esiste una pianta, imposta la sua proprietà IsPlanting a true
        if (plant != null)
        {
            plant.IsPlanting = true;
        }
    }

    //===========INPUT SETTING===========//
    private void OnEnable()
    {
        //Keyboard
        // Abilita l'azione della zappa
        hole_Keyboard.action.Enable();
        hole_Keyboard.action.started += UseHole_Keyboard;

        //Controller
        // Abilita l'azione della zappa
        hole_Controller.action.Enable();
        hole_Controller.action.started += UseHole_Controller;
    }

    private void OnDisable()
    {
        //Keyboard
        // Disabilita l'azione della zappa
        hole_Keyboard.action.Disable();
        hole_Keyboard.action.started -= UseHole_Keyboard;

        //Controller
        // Abilita l'azione della zappa
        hole_Controller.action.Enable();
        hole_Controller.action.started += UseHole_Controller;
    }

    //===========INPUT KEYBOARD===========//
    private void UseHole_Keyboard(InputAction.CallbackContext context)
    {
        if (canUseTool && isUsingHoe)
        {
            ChangeTile();
        }
    }

    //===========INPUT CONTROLLER===========//
    private void UseHole_Controller(InputAction.CallbackContext context)
    {
        if (canUseTool && isUsingHoe)
        {
            ChangeTile();
        }
    }
}
