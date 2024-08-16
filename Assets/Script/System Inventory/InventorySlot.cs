using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Ui Invetatario")]
    public GameObject background;  // L'immagine di background dello slot
    public Text quantityText;  // Il testo che mostra la quantità di semi
    public Image seedImage;  // L'immagine che rappresenta il seme
    public GameObject seedPrefab;  // Prefab del seme da piantare 

    public int ortaggioID;
    public int quantity;  // La quantità di semi nello slot
    public string seedType;

    private void Start()
    {
        quantityText.text = quantity.ToString();
    }
    // Metodo per inizializzare lo slot con una quantità di semi e un'immagine del seme
    public void Initialize(int initialQuantity, Sprite seedSprite, GameObject seedPrefab)
    {
        quantity = initialQuantity;
        if (seedSprite != null)
        {
            quantity = initialQuantity;
            seedImage.sprite = seedSprite;  // Assegna l'immagine del seme
            this.seedPrefab = seedPrefab;  // Assegna il prefab del seme
            UpdateUI();
        }
        
        UpdateUI();
    }

    // Metodo per selezionare lo slot, cambia il colore di background a rosso
    public void Select()
    {

        background.SetActive(true);
        //background.color = Color.red;
    }

    // Metodo per deselezionare lo slot, cambia il colore di background a bianco
    public void Deselect()
    {
        background.SetActive(false);
        //background.color = Color.white;
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
