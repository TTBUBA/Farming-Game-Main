using UnityEngine;
using UnityEngine.UI;

public class Slot_Tools : MonoBehaviour
{
    public ToolsData toolsData; // Dati dell'ortaggio
    public Image slotImage; // Immagine dello slot
    public Text quantityText;

    private void Start()
    {
        slotImage.sprite = toolsData.icon;
    }

    public void SetSlot(ToolsData NewTools)
    {
        Debug.Log(NewTools.name);
    }

    public void Select()
    {
       this.gameObject.transform.localScale = new Vector3(1.10f, 1.10f, 1);
    }

    public void Deselect()
    {
        this.gameObject.transform.localScale = Vector3.one;
    }

    // Metodo per aggiornare la Ui 
    private void UpdateUI()
    {
        //quantityText.text = vegetableData.quantity.ToString();  // Aggiorna il testo con la quantità attuale
    }
}
