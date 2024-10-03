using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Shop shop;
    public int SlotIndex; 
    

    public void OnPointerEnter(PointerEventData eventData)
    {

        transform.DOScale(new Vector3(1.05f, 1.05f, 1f), 0.2f).SetEase(Ease.InBounce);
        shop.currentIndex = SlotIndex;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InBounce);
    }

   
}
