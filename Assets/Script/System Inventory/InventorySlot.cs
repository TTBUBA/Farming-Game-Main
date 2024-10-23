//Questo script gestisce il singolo slot dell'inventario, che può contenere ortaggi o strumenti

using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public VegetableData vegetableData;
    public ToolsData toolData;

    [Header("Ui Invetatario")]
    public GameObject background;  // L'immagine di background dello slot
    public Text quantityText;  // Il testo che mostra la quantità di semi

    private void Start()
    {
        quantityText.text = vegetableData.quantity.ToString();

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

    // Metodo per impostare i dati dell'ortaggio
    public void SetVegetableData(VegetableData data)
    {
        vegetableData = data;
    }

    // Metodo per impostare i dati dei tools
    public void SetToolData(ToolsData data)
    {
        toolData = data;
    }

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
        if (vegetableData.quantity > 0)  // Controlla se ci sono semi disponibili
        {
            vegetableData.quantity--;  // Riduce la quantità di uno
            UpdateUI();  // Aggiorna la UI per riflettere la nuova quantità
        }
    }

    // incrementa la quandita di semi 
    public void IncreaseSeedQuantity(int Quantity)
    {
        vegetableData.quantity += Quantity;
        UpdateUI();
    }

    // dimunuisce la quandita di semi
    public void DecreaseSeedQuantity(int Quantity)
    {
        vegetableData.quantity -= Quantity;
        UpdateUI();

    }

    // Metodo privato per aggiornare il testo della quantità nella UI
    private void UpdateUI()
    {
        quantityText.text = vegetableData.quantity.ToString();  // Aggiorna il testo con la quantità attuale
    }
}
