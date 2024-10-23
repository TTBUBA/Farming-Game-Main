using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class Plant : MonoBehaviour
{
    public Sprite[] growSprites;
    public float timeStages;
    public string ItemType;
    public int CurrentStage = 0;

  
    private SpriteRenderer SpriteRenderer;
    public InventoryManager InventoryManager;
    public Vector3Int cellPositionPlant;

    // Start is called before the first frame update
    void Start()
    {
        InventorySlot inventorySlot = GetComponent<InventorySlot>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartGrowth(InventorySlot selectedSlot)
    {
        // Ottieni i dati dallo slot selezionato
        VegetableData vegetableData = selectedSlot.vegetableData;

        growSprites = vegetableData.GrowSprites;
        timeStages = vegetableData.TimeStages;
        ItemType = vegetableData.ItemType;


        // Debug.Log("Dati" + selectedSlot.seedPrefab.name + "tempo" + timeStages);

        CurrentStage = 0;

        StartCoroutine(Grow());
    }
 
   public IEnumerator Grow()
    {
        while (CurrentStage < growSprites.Length)
        {
            yield return new WaitForSeconds(timeStages);
            CurrentStage++;
            SpriteRenderer.sprite = growSprites[CurrentStage];
        }

        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (CurrentStage >= 3)
        {
            if (other.gameObject.CompareTag("BoxPlayer"))
            {
                TrakingLocal trakingRaccolto = other.GetComponent<TrakingLocal>();

                if (trakingRaccolto != null)
                {
                    trakingRaccolto.CollectItem(ItemType);
                    InventoryManager.(cellPositionPlant);
                    ResetPlant();

                    //Debug.Log("Collisione Avvenuta e raccolto aggiunto");
                }

            }

        }
    }

    public void ResetPlant()
    {
        CurrentStage = 0;
        SpriteRenderer.sprite = null;
        growSprites = null;
        timeStages = 0;
    }
}
