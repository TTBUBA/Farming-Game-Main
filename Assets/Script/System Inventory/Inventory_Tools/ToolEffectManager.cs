// Gestisce gli effetti degli strumenti (zappa e piccone) in base allo strumento selezionato nella hotbar del giocatore. 
// Cambia i tile della mappa o esegue azioni specifiche a seconda dello strumento e della posizione del giocatore.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    public void Update()
    {
        ApplyToolEffect(); // Applica l'effetto dello strumento ogni frame
    }

    public void ApplyToolEffect()
    {
        // Ottiene lo slot selezionato dalla hotbar
        Slot_Tools slotTools = hotbarManager.hotbarSlots[hotbarManager.selectedHotbarSlotIndex];

        // Controlla se il tag corrente del giocatore corrisponde all'area di utilizzo dello strumento
        if (PlayerCollision.CurrentCollisiontag == slotTools.toolsData.AreaUsing)
        {
            switch (slotTools.toolsData.NameTools) // Determina quale strumento è selezionato
            {
                case "hoe": // Se lo strumento è la zappa
                    ChangeTile();
                    break;

                case "pickaxe": // Se lo strumento è il piccone
                    PickAxe_Test(); 
                    break;
            }
        }
    }

    public void ChangeTile()
    {
        // Ottiene la cella attuale sulla mappa in base alla posizione del punto di riferimento
        Vector3Int currentCell = tilemapTerrain.WorldToCell(PointSpawn.transform.position);

        // Controlla se il tasto G è premuto
        if (Input.GetKeyDown(KeyCode.V))
        {
            // Ottiene la pianta presente nella cella corrente, se esiste
            Plant plant = inventoryManager.GetPlantAtPosition(currentCell)?.GetComponent<Plant>();

            // Imposta il nuovo tile nella mappa
            tilemapTerrain.SetTile(currentCell, Newtile);

            // Se esiste una pianta, imposta la sua proprietà IsPlanting a true
            if (plant != null)
            {
                plant.IsPlanting = true;
            }

            Debug.Log("Hoe Using"); // Messaggio di debug per l'uso della zappa
        }
    }

    public void PickAxe_Test()
    {
        //Piccone meccanica
    }
}
