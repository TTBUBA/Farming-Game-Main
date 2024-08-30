using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Shop shop;
    public int SlotIndex; 
    



    public void OnPointerEnter(PointerEventData eventData)
    {

        transform.transform.DOScale(new Vector2(1.05f, 1.05f), 0.2f).SetEase(Ease.InBounce);
        shop.currentIndex = SlotIndex;

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        transform.DOScale(Vector2.one, 0.2f).SetEase(Ease.InBounce);
    }

   
}
