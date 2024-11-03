// Rappresenta uno slot nella hotbar per un particolare strumento. Gestisce la visualizzazione dell'icona dello strumento 
// e fornisce metodi per selezionare e deselezionare lo slot, oltre a potenziali aggiornamenti dell'interfaccia utente.

using UnityEngine;
using UnityEngine.UI;

public class Slot_Tools : MonoBehaviour
{
    public ToolsData toolsData; // Dati dello strumento
    public Image slotImage; // Immagine dello slot
    public Text quantityText; // Testo per la quantità dello strumento

    private void Start()
    {
        slotImage.sprite = toolsData.icon; // Imposta l'immagine dello slot con l'icona dello strumento
    }

    public void SetSlot(ToolsData NewTools)
    {
        Debug.Log(NewTools.name); // Messaggio di debug per il nome del nuovo strumento
    }

    public void Select()
    {
        // Ingrandisce lo slot quando è selezionato
        this.gameObject.transform.localScale = new Vector3(1.10f, 1.10f, 1);
    }

    public void Deselect()
    {
        // Riporta lo slot alla dimensione normale quando non è selezionato
        this.gameObject.transform.localScale = Vector3.one;
    }

    // Metodo per aggiornare la UI (disattivato nel codice attuale)
    private void UpdateUI()
    {
        // quantityText.text = vegetableData.quantity.ToString(); // Aggiorna il testo della quantità attuale
    }
}
