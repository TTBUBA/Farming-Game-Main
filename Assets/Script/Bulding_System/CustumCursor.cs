using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class CustumCursor : MonoBehaviour
{
    private void Update()
    {
        // si ottiene la posizione del mouse e aggiorna la posizione del cursore personalizzato
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = MousePosition;
    }
}
