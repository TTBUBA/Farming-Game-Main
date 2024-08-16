using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform[] waypoints; // Punti di riferimento per la strada generale
    // public Transform[] rightTurnWaypoints; // Punti di riferimento per girare a destra
    // public Transform[] leftTurnWaypoints; // Punti di riferimento per girare a sinistra
    public float speed = 5f; // Velocità dell'auto
    private int currentWaypointIndex = 0; // Indice del punto di riferimento corrente

    void Start()
    {
        InvokeRepeating("MoveCar", 0f, 1f); // Chiama il metodo "MoveCar" ogni secondo (dopo 0 secondi di ritardo)
    }

    void MoveCar()
    {

        /* 
         * 
        //QUESTO CODICE SERVE PER FAR GIRARE LA MACCHINA IN UN TERMINATO  WAYPOINT//

        // Sceglie casualmente una direzione all'incrocio
        int randomDirection = Random.Range(0, 3); // 0: va dritto dritto, 1: gira a destra, 2: gira a sinistra

        // allora sceglie casualmente randomDirection se e  0 va dritto e cosi via dicendo
        switch (randomDirection)
        {
            case 0:
                // Nella direzione "andare dritto" usiamo gli stessi waypoints della strada generale
                waypoints = this.waypoints;
                break;
            case 1:
                waypoints = rightTurnWaypoints;
                break;
            case 2:
                waypoints = leftTurnWaypoints;
                break;
        }

        */

        // Imposta il primo punto di riferimento
        transform.position = waypoints[currentWaypointIndex].position;
    }
    void Update()
    {
        // in questo metodo controlla se l'auto ha raggiunto il punto di riferimento corrente che sarebbe currentWaypointIndex
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Passa al punto di riferimento successivo
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            // Se l'indice supera la lunghezza dell'array,  lo riporta al valore 0 che sarebbe la variabile currentWaypointIndex
            if (currentWaypointIndex == 0)
            {
                currentWaypointIndex = 0;
            }
        }

        // in questi due rige di codice l'auto si muove verso il punto di riferimento corrente
        Vector2 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

}
