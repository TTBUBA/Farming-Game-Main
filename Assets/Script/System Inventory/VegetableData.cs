using UnityEngine;

[CreateAssetMenu(fileName = "Vegetable" , menuName = "Vegetable")]
public class VegetableData : ScriptableObject
{
    public string NameVegetable;
    public int quantity;
    public Sprite[] GrowSprites;
    public float TimeStages;
    public string ItemType;
    //public string seedType;
}
