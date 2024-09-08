using UnityEngine;
using UnityEngine.UI;

    public enum ItemType 
    {
        Carota,
        Patata,
        Grano,
        Cavolo
    }
public class Item : MonoBehaviour
{
    public enum ItemType 
    {
        Carota,
        Patata,
        Grano,
        Cavolo
    }
    public ItemType itemType;
    public string NameItem;

    public int CurrentQuantity;
    public int RequiredQuantity;

    public Text Text;

    public Tracking_Item trackingItem;

    public void Start()
    {
        UpdateUiItem();
    }

    public void SetRequiredQuantity(int Quantity)
    {
        RequiredQuantity = Quantity;
    }

    public void SetCurrentQuantity(int quantity)
    {
        CurrentQuantity = quantity;
    }

    public void UpdateUiItem()
    {
        CurrentQuantity = GetCurrentHaveAmount();
        Text.text = CurrentQuantity.ToString() + "/" + RequiredQuantity.ToString();
    }

    public int GetCurrentHaveAmount()
    {
        switch (itemType)
        {
            case ItemType.Carota:
                return trackingItem.RaccoltoCarote;
            case ItemType.Patata:
                return trackingItem.RaccoltoPatate;
            case ItemType.Grano:
                return trackingItem.RaccoltoGrano;
            case ItemType.Cavolo:
                return trackingItem.Raccoltocavolo;
            default:
                return 0;
        }
    }
}
