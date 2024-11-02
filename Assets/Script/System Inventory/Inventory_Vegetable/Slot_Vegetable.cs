using UnityEngine;
using UnityEngine.UI;

public class Slot_Vegetable : MonoBehaviour
{
    public VegetableData vegetableData; // Dati dell'ortaggio
    public Image slotImage; // Immagine dello slot
    public Text quantityText;

    private void Start()
    {
        quantityText.text = vegetableData.quantity.ToString();
        slotImage.sprite = vegetableData.IconVegetable; 
    }

    public void SetSlot(VegetableData newVegetableData)
    {
        quantityText.text = vegetableData.quantity.ToString();
        vegetableData = newVegetableData;
        slotImage.sprite = newVegetableData.GrowSprites[0]; // Mostra l'immagine del primo sprite
    }

    public void PlantSeed()
    {
        if (vegetableData != null && vegetableData.quantity > 0)
        {
            vegetableData.quantity--; // Riduci la quantità
            quantityText.text = vegetableData.quantity.ToString();
            //Debug.Log(vegetableData.ItemType);
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
