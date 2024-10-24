using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    public InventorySlot[] hotbarSlots; // Hotbar con 4 slot
    private int selectedHotbarSlotIndex = 0; // Indice dello slot selezionato

    void Update()
    {
        ChangeHotbarSlot(); // Controlla la selezione degli slot della hotbar
    }

    public void ChangeHotbarSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectHotbarSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectHotbarSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectHotbarSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectHotbarSlot(3);
    }

    void SelectHotbarSlot(int index)
    {
        foreach (var slot in hotbarSlots)
        {
            slot.Deselect(); // Deseleziona gli slot
        }

        selectedHotbarSlotIndex = index;
        hotbarSlots[selectedHotbarSlotIndex].Select(); // Seleziona lo slot
    }

    public InventorySlot GetSelectedSlot()
    {
        return hotbarSlots[selectedHotbarSlotIndex]; // Restituisce lo slot selezionato
    }
}
