
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageHover_Animal : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public int CurrentIndex;
    public Shop_Manager_Animal ShopManagerAnimal;


    public void OnPointerEnter(PointerEventData eventData)
    {
        

        transform.localScale = new Vector2(1.1f ,1.1f);
        
         ShopManagerAnimal.currentIndex = CurrentIndex;
    }

    public void ResetUi()
    {
        transform.localScale = new Vector2(1f, 1f);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetUi();
    }
}
