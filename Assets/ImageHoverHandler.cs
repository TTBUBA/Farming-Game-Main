
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageHoverHandler : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    private UI_Controller uIController;
    private int ImageIndex;

    void Start()
    {
        uIController = FindObjectOfType<UI_Controller>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //uIController.SetCurrentIndex(ImageIndex);

        transform.localScale = new Vector2(1.1f ,1.1f);
        

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
