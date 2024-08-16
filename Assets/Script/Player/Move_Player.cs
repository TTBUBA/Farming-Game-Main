using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Move_Player : MonoBehaviour , IData
{
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    public float speed = 7f;
    public Animator animator;
    
    private bool isMovingRight;
    private bool isMovingLeft;

    public Tilemap Terrainmap; // La Tilemap su cui muoversi
    public GameObject PointSpawn; // L'oggetto da muovere

    // Variabili che servono per memorizzare le posizioni delle celle
    private Vector3Int CellPosition; // Posizione della cella corrente
    private Vector3Int NextCellPosition; // Posizione della prossima cella da raggiungere


    public GameObject Axe;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        NextCellPosition = new Vector3Int(CellPosition.x + 1 , CellPosition.y + 1);
        PointSpawn.transform.position = Terrainmap.GetCellCenterWorld(NextCellPosition);
        animator = GetComponent<Animator>();
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.PositionPlayer;
    }

    public void SaveData(GameData data)
    {
        data.PositionPlayer = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(horizontal , vertical);

        if(direction.magnitude  > 0)
        {
            direction.Normalize();
        }

        rb.velocity = direction * speed;

        // Controlla la direzione del movimento
        if(horizontal > 0)
        {
            isMovingRight = true;
            isMovingLeft = false;
            Axe.transform.localPosition = new Vector2(1, 0);
            Axe.transform.localScale = new Vector2(0.6f, 0.6f);

            transform.localScale = new Vector2(0.8f, 0.8f);
        }
        else if (horizontal < 0)
        {
            isMovingRight = false;
            isMovingLeft = true;
            Axe.transform.localPosition = new Vector2(1, 0);
            Axe.transform.localScale = new Vector2(-0.6f, 0.6f);

            //transform.localScale = new Vector2(-0.8f, 0.8f);
        }
        else
        {
            isMovingRight = false;
            isMovingLeft = false;
        }

        if(isMovingLeft == true && isMovingRight == false)
        {
           transform.localScale = new Vector2(-0.8f, 0.8f);
        }

        if (isMovingRight == true && isMovingLeft == false)
        {
            transform.localScale = new Vector2(0.8f, 0.8f);
        }

        animator.SetBool("Left", isMovingLeft);
        animator.SetBool("Right", isMovingRight);

        MovimentoPointSpawn();
    }

    // Funzione che serve per far muovere l'oggetto PointSpawn sulla Tilemap
    public void MovimentoPointSpawn()
    {
        // Facendo cosi si ottiene la  posizione della cella corrente in cui si trova PointSpawn
        CellPosition = Terrainmap.WorldToCell(transform.position);

        // Viene effetuato un controllo, se PointSpawn ha raggiunto una nuova cella
        if (NextCellPosition != CellPosition)
        {
            // Calcola la posizione della prossima cella aumentando di uno l'indice x rispetto alla cella corrente
            NextCellPosition = new Vector3Int(CellPosition.x + 1, CellPosition.y);

            // Sposta l'oggetto PointSpawn al centro della prossima cella calcolata da NextCellPosition
            PointSpawn.transform.position = Terrainmap.GetCellCenterWorld(NextCellPosition);
        }
    }

}
