using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SeedSelectionManager : MonoBehaviour
{
    public GameObject seedSelectionPanel; // Pannello di selezione semi
    public InventorySlot[] seedSlots; // Slot per gli ortaggi
    public InventoryManager inventoryManager;

    public Image ImageSeedSelect;
    public Text QuantitySeedSelect;
    void Start()
    {
        //seedSelectionPanel.SetActive(false); // Nascondi il pannello di selezione
    }

    public void OpenSeedSelection()
    {
        seedSelectionPanel.SetActive(true); // Mostra il pannello di selezione
        UpdateSeedSlots(); // Aggiorna gli slot con i dati degli ortaggi
    }

    public void CloseSeedSelection()
    {
        seedSelectionPanel.SetActive(false); // Nasconde il pannello di selezione
    }

    void UpdateSeedSlots()
    {
        for (int i = 0; i < seedSlots.Length; i++)
        {
            var vegetableData = GetVegetableData(i);
            if (vegetableData != null)
            {
                seedSlots[i].SetSlot(vegetableData);
            }
        }
    }

    VegetableData GetVegetableData(int index)
    {
        // Restituisci l'ortaggio corrispondente in base all'indice
        return /* Dati ortaggio, ad esempio: */ null;
    }

    public void SelectSeed(InventorySlot selectedSlot)
    {
        //Debug.Log(selectedSlot.vegetableData.NameVegetable + ":" + selectedSlot.vegetableData.quantity);
        //Debug.Log(selectedSlot.vegetableData);
       // Debug.Log(selectedSlot.vegetableData.ItemType);

        ImageSeedSelect.sprite = selectedSlot.vegetableData.IconVegetable;
        QuantitySeedSelect.text = selectedSlot.vegetableData.quantity.ToString();

        inventoryManager.SetCurrentSelectedSlot(selectedSlot);// Aggiorna lo slot selezionato nell'inventario

        //CloseSeedSelection(); // Chiudi il pannello di selezione
    }
}
