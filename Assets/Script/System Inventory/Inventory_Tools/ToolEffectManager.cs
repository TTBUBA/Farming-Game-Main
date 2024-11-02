using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolEffectManager : MonoBehaviour
{
    [Header("Hoe")]
    public Tilemap tilemapTerrain;
    public Tile Newtile;
    public GameObject PointSpawn;



    public Player_Manager PlayerCollision;
    public Slot_Tools[] SlotTools;
    public InventoryManager inventoryManager;
    public HotbarManager hotbarManager;
    public void Update()
    {
        ApplyToolEffect();
    }
    public void ApplyToolEffect()
    {
        // Ottiene lo slot selezionato
        Slot_Tools slotTools = hotbarManager.hotbarSlots[hotbarManager.selectedHotbarSlotIndex];

        //Debug.Log(slotTools.toolsData.NameTools);
        
        if (PlayerCollision.CurrentCollisiontag == slotTools.toolsData.AreaUsing)
        {
            switch(slotTools.toolsData.NameTools)
            {
                case "hoe":
                   ChangeTile();
                   break;

                case "pickaxe":
                    PickAxe_Test(); 
                    break;
            }
        }
        

    }

    public void ChangeTile()
    {
        Vector3Int currentCell = tilemapTerrain.WorldToCell(PointSpawn.transform.position);

        if (Input.GetKeyDown(KeyCode.V))
        {
            Plant plant = inventoryManager.GetPlantAtPosition(currentCell)?.GetComponent<Plant>();

            tilemapTerrain.SetTile(currentCell, Newtile);

            if(plant != null)
            {
                plant.IsPlanting = true;
            }

            Debug.Log("Hoe Using");
        }
    }

    public void PickAxe_Test()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
           Debug.Log("PickAxe Using");
        }

    }
}
