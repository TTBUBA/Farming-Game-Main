using UnityEngine;
using UnityEngine.UI;

public class Slot_Vegetable : MonoBehaviour
{
    public VegetableData vegetableData; // Dati dell'ortaggio
    public Image slotImage; // Immagine dello slot
    public Text quantityText; // Testo che mostra la quantit� di semi

    private void Start()
    {
        // Inizializza la UI dello slot con i dati dell'ortaggio
        quantityText.text = vegetableData.quantity.ToString(); // Mostra la quantit� iniziale
        slotImage.sprite = vegetableData.IconVegetable; // Imposta l'icona dell'ortaggio nello slot
    }

    // Imposta i dati per questo slot
    public void SetSlot(VegetableData newVegetableData)
    {
        vegetableData = newVegetableData; // Aggiorna i dati dell'ortaggio
        slotImage.sprite = newVegetableData.GrowSprites[0]; // Mostra l'immagine del primo sprite
        UpdateUI(); // Aggiorna l'interfaccia utente con i nuovi dati
    }

    // Riduci la quantit� di semi quando si pianta
    public void PlantSeed()
    {
        if (vegetableData != null && vegetableData.quantity > 0) // Controlla se ci sono semi disponibili
        {
            vegetableData.quantity--; // Riduci la quantit�
            UpdateUI(); // Aggiorna l'interfaccia utente
        }
    }

    // Incrementa la quantit� di semi
    public void IncreaseSeedQuantity(int Quantity)
    {
        vegetableData.quantity += Quantity; // Aggiungi la quantit� specificata
        UpdateUI(); // Aggiorna l'interfaccia utente
    }

    // Decrementa la quantit� di semi
    public void DecreaseSeedQuantity(int Quantity)
    {
        vegetableData.quantity -= Quantity; // Sottrai la quantit� specificata
        UpdateUI(); // Aggiorna l'interfaccia utente
    }

    // Metodo privato per aggiornare il testo della quantit� nella UI
    private void UpdateUI()
    {
        quantityText.text = vegetableData.quantity.ToString(); // Aggiorna il testo con la quantit� attuale
    }
}
