using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Array di waypoints per i punti attraverso cui l'NPC si muove
    public float speed = 2f;       // Velocità di movimento del NPC

    private int currentWaypointIndex = 0; // Indice del waypoint corrente su cui l'NPC si sta muovendo
    private int targetWaypointIndex;      // Indice del waypoint target scelto a caso per la prossima destinazione
    private bool isReturning = false;     // Bool per determinare se l'NPC sta tornando indietro
    private Animator animator;            // Riferimento al componente Animator per controllare le animazioni
    private Vector2 lastDirection;        // Per tenere traccia della direzione precedente dell'NPC

    void Start()
    {
        ChooseRandomWaypoint();        // Sceglie un waypoint casuale per iniziare
        animator = GetComponent<Animator>(); // Ottiene il componente Animator dell'NPC
        lastDirection = Vector2.zero; // Inizializza lastDirection
    }

    void Update()
    {
        MoveToWaypoint(); // Chiama la funzione per muovere l'NPC verso il waypoint
    }

    void ChooseRandomWaypoint()
    {
        // Sceglie un waypoint casuale tra quelli disponibili (escluso il primo)
        targetWaypointIndex = Random.Range(1, waypoints.Length);
    }

    void MoveToWaypoint()
    {
        // Ottiene il waypoint corrente
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        // Calcola la direzione verso il waypoint
        Vector2 direction = (targetWaypoint.position - transform.position).normalized;

        // Muove l'NPC verso il waypoint
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Aggiorna l'animazione dell'NPC in base alla direzione
        RotateNpc(direction);

        // Controlla se l'NPC è arrivato vicino al waypoint target
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Se l'NPC ha raggiunto il waypoint target e non sta tornando indietro, inizia a tornare indietro
            if (!isReturning && currentWaypointIndex == targetWaypointIndex)
            {
                isReturning = true;
            }
            // Se l'NPC ha raggiunto il punto di partenza mentre sta tornando indietro, sceglie un nuovo waypoint casuale
            else if (isReturning && currentWaypointIndex == 0)
            {
                isReturning = false;
                ChooseRandomWaypoint();
            }

            // Se sta tornando indietro, decrementa l'indice del waypoint corrente
            if (isReturning)
            {
                currentWaypointIndex--;
            }
            // Altrimenti, incrementa l'indice del waypoint corrente
            else
            {
                currentWaypointIndex++;
            }
        }
    }

    public void RotateNpc(Vector2 direction)
    {
        // Se la direzione e cambiata rispetto all'ultima direzione
        if (direction != lastDirection)
        {
            // Determina la direzione verticale o orizzontale
            if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            {
                // Direzione verticale
                if (direction.y > 0)
                {
                    SetAnimation("Up");  
                }
                else if (direction.y < 0)
                {
                    SetAnimation("Down"); 
                }
            }
            else
            {
                // Direzione orizzontale
                if (direction.x > 0)
                {
                    SetAnimation("Right"); 
                }
                else if (direction.x < 0)
                {
                    SetAnimation("Left"); 
                }
            }
            // Aggiorna l'ultima direzione
            lastDirection = direction;
        }
    }

    public void SetAnimation(string direction)
    {
        // Disattiva tutte le animazioni e poi attiva solo quella specificata
        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
        animator.SetBool("Right", false);
        animator.SetBool("Left", false);
        animator.SetBool(direction, true);
    }
}
