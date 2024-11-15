using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageHover_Mouse : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector3 Size;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Size;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
