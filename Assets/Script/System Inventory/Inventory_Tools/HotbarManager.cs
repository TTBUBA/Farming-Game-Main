using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    public Slot_Tools[] hotbarSlots; // Hotbar con 4 slot
    public int selectedHotbarSlotIndex = 0; // Indice dello slot selezionato

    void Start()
    {
        hotbarSlots[selectedHotbarSlotIndex].Select();
    }
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

        Debug.Log(hotbarSlots[selectedHotbarSlotIndex].name);

        selectedHotbarSlotIndex = index;
        hotbarSlots[selectedHotbarSlotIndex].Select(); // Seleziona lo slot
    }

}
