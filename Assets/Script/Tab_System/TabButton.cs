using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Assicura che il componente Image sia presente
[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    // Riferimento al gruppo di tab a cui appartiene
    public TabGroup tabGroup;
    // Riferimento all'immagine di background della tab
    public Image background;

    // Metodo chiamato all'inizio, una volta sola
    public void Start()
    {
        // Ottiene il componente Image
        background = GetComponent<Image>();
        // Si iscrive al gruppo di tab
        tabGroup.subscribe(this);
    }

    // Metodo chiamato quando si clicca sulla tab
    public void OnPointerClick(PointerEventData eventData)
    {
        // Notifica il gruppo che la tab è stata selezionata
        tabGroup.OnTabSelected(this);
    }

    // Metodo chiamato quando si passa sopra la tab con il mouse
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Notifica il gruppo che il mouse è entrato nella tab
        tabGroup.OnTabEnter(this);
    }

    // Metodo chiamato quando si esce dalla tab con il mouse
    public void OnPointerExit(PointerEventData eventData)
    {
        // Notifica il gruppo che il mouse è uscito dalla tab
        tabGroup.OnTabExit(this);
    }
}
