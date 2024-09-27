using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class VegetableData
{
    public Sprite[] GrowSprites { get; private set; }
    public float TimeStages { get; private set; }
    public string ItemType { get; private set; }

    public VegetableData(Sprite[] growSprites, float timeStages, string itemType)
    {
        GrowSprites = growSprites;
        TimeStages = timeStages;
        ItemType = itemType;
    }
}
public class InventorySlot : MonoBehaviour
{
    public Sprite[] GrowSprites;
    public float timeStages;
    public string itemType;

    [Header("Ui Invetatario")]
    public GameObject background;  // L'immagine di background dello slot
    public Text quantityText;  // Il testo che mostra la quantità di semi

    public int quantity;  // La quantità di semi nello slot
    public string seedType;

    private void Start()
    {
        quantityText.text = quantity.ToString();

        /* DEBUG DATA 
        Sprite[] sprites;
        float timestage;
        string type;

        GetData(out sprites, out timestage, out type);

        Debug.Log(sprites.Length);
        Debug.Log(timestage);
        Debug.Log( type);
        */
        
    }

    // Metodo per ottenere i dati dell ortaggio
    public VegetableData GetVegetableData()
    {
        return new VegetableData(GrowSprites, timeStages, itemType);
    }

    /*// Metodo per inizializzare lo slot con una quantità di semi e un'immagine del seme
    public void InitializeSeed(int initialQuantity, Sprite seedSprite, GameObject seedPrefab)
    {
        quantity = initialQuantity;
        if (seedSprite != null)
        {
            quantity = initialQuantity;
            //seedImage.sprite = seedSprite;  // Assegna l'immagine del seme
            this.seedPrefab = seedPrefab;  // Assegna il prefab del seme
            UpdateUI();
        }
        
        UpdateUI();
    }
    */

    // Metodo per selezionare lo slot, cambia il colore di background a rosso
    public void Select()
    {
        background.SetActive(true);
    }

    // Metodo per deselezionare lo slot, cambia il colore di background a bianco
    public void Deselect()
    {
        background.SetActive(false);
    }

    // Metodo per piantare un seme, riduce la quantità e aggiorna la UI
    public void PlantSeed()
    {
        if (quantity > 0)  // Controlla se ci sono semi disponibili
        {
            quantity--;  // Riduce la quantità di uno
            UpdateUI();  // Aggiorna la UI per riflettere la nuova quantità
        }
    }

    // incrementa la quandita di semi 
    public void IncreaseSeedQuantity(int Quantity)
    {
        quantity += Quantity;
        UpdateUI();
    }

    // dimunuisce la quandita di semi
    public void DecreaseSeedQuantity(int Quantity)
    {
        quantity -= Quantity;
        UpdateUI();

    }

    // Metodo privato per aggiornare il testo della quantità nella UI
    private void UpdateUI()
    {
        quantityText.text = quantity.ToString();  // Aggiorna il testo con la quantità attuale
    }
}
