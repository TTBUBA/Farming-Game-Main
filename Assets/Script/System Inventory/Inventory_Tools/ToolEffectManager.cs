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

    public void Update()
    {
        ApplyToolEffect();
    }
    public void ApplyToolEffect()
    {
        foreach (Slot_Tools slot in SlotTools)
        {
            if (PlayerCollision.CurrentCollisiontag == slot.toolsData.AreaUsing)
            {
                switch(slot.toolsData.NameTools)
                {
                    case "hoe":
                        ChangeTile();
                        Debug.Log("hoe using");
                        break;
                }
            }
        }

    }

    public void ChangeTile()
    {
        Vector3Int currentCell = tilemapTerrain.WorldToCell(PointSpawn.transform.position);

        if (Input.GetKeyDown(KeyCode.V))
        {
            tilemapTerrain.SetTile(currentCell, Newtile);
        }
        
    }
}
