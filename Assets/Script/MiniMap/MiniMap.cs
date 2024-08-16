using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    // Riferimento al transform del giocatore
    public Transform Player;

    private void LateUpdate()
    {
        // Copia la posizione del giocatore nella variabile newpositionY, mantenendo solo l'asse y
        Vector3 newpositionY = Player.position;
        newpositionY.y = transform.position.y;
        // Assegna la nuova posizione della mappa mantenendo invariata l'altezza
        transform.position = newpositionY;

        // Copia la posizione del giocatore nella variabile newpositionx, mantenendo solo l'asse x
        Vector3 newpositionx = Player.position;
        newpositionx.x = transform.position.x;
        // Assegna la nuova posizione della mappa mantenendo invariata l'altezza e la coordinata y
        transform.position = newpositionx;
    }
}
