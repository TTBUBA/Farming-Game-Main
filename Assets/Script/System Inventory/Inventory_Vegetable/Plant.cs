using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public Sprite[] growSprites; // Array di sprite per le fasi di crescita
    public float timeStages; // Tempo di crescita per ciascuna fase
    public string ItemType; // Tipo di oggetto (es. ortaggio)
    public int CurrentStage = 0; // Fase attuale della crescita
    public bool IsPlanting; // Indica se è in fase di piantagione

    private SpriteRenderer SpriteRenderer; // Riferimento al componente SpriteRenderer
    public InventoryManager InventoryManager; // Riferimento al gestore dell'inventario
    public Vector3Int cellPositionPlant; // Posizione della cella in cui è piantata la pianta

    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Inizia la crescita del seme piantato
    public void StartGrowth(Slot_Vegetable selectedSlot)
    {
        // Ottieni i dati dallo slot selezionato
        VegetableData vegetableData = selectedSlot.vegetableData;

        growSprites = vegetableData.GrowSprites;
        timeStages = vegetableData.TimeStages;
        ItemType = vegetableData.ItemType;

        CurrentStage = 0;

        // Imposta immediatamente lo sprite iniziale
        if (growSprites != null && growSprites.Length > 0)
        {
            SpriteRenderer.sprite = growSprites[CurrentStage];
        }

        StartCoroutine(Grow()); // Inizia la coroutine per la crescita
    }

    // Coroutine per la crescita della pianta
    public IEnumerator Grow()
    {
        while (CurrentStage < growSprites.Length - 1) // Aggiungi -1 per evitare un out of bounds
        {
            yield return new WaitForSeconds(timeStages);
            CurrentStage++;
            SpriteRenderer.sprite = growSprites[CurrentStage];
        }
    }

    // Rileva se il player può raccogliere la pianta
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (CurrentStage >= 3) // Verifica che la pianta sia matura
        {
            if (other.gameObject.CompareTag("BoxPlayer"))
            {
                TrakingLocal trakingRaccolto = other.GetComponent<TrakingLocal>();

                if (trakingRaccolto != null)
                {
                    trakingRaccolto.CollectItem(ItemType);
                    InventoryManager.RemoveVegetableTile(cellPositionPlant);
                    ResetPlant();
                }
            }
        }
    }

    // Reset della pianta
    public void ResetPlant()
    {
        CurrentStage = 0;
        SpriteRenderer.sprite = null;
        growSprites = null;
        timeStages = 0;
    }
}
