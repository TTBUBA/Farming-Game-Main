using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


public class Move_Player : MonoBehaviour, IData
{
    private float horizontal;
    private float vertical;

    public float lastHorizontal;
    public float lastVertical;


    private Rigidbody2D rb;
    public float speed = 7f;
    public Animator animator;

    private bool isMovingRight;
    private bool isMovingLeft;
    private bool isMovingUp;

    public Tilemap Terrainmap; // La Tilemap su cui muoversi
    public GameObject PointSpawn; // L'oggetto da muovere
    public bool ActivePointPlant = false;
    // Variabili che servono per memorizzare le posizioni delle celle
    private Vector3Int CellPosition; // Posizione della cella corrente
    private Vector3Int NextCellPosition; // Posizione della prossima cella da raggiungere

    // Input System
    public InputActionReference MovePlayer_KeyBoard;
    public InputActionReference MovePlayer_Controller;

    public PlayerInput playerInput;

    public GameObject Axe;


    public Vector2 Movement;
    public Vector2 keyboardMovement;
    public Vector2 controllerMovement;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    //Input Controller//
    private void OnEnable()
    {
        MovePlayer_KeyBoard.action.Enable();
        MovePlayer_Controller.action.Enable();
    }
    private void OnDisable()
    {
        MovePlayer_KeyBoard.action.Disable();
        MovePlayer_Controller.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {


        keyboardMovement = MovePlayer_KeyBoard.action.ReadValue<Vector2>();
        controllerMovement = MovePlayer_Controller.action.ReadValue<Vector2>();


        Movement = (keyboardMovement + controllerMovement).normalized;

        horizontal = Movement.x;
        vertical = Movement.y;

       // Debug.Log(horizontal);
        // Il player rimane nella posizione in qui era prima
        if (horizontal != 0 || vertical != 0)
        {
            lastHorizontal = horizontal;
            lastVertical = vertical;
        }

        animator.SetBool("IdleDown", false);
        animator.SetBool("IdleUp", false);
        animator.SetBool("IdleLeft", false);
        animator.SetBool("IdleRight", false);

        animator.SetFloat("Horizontal", lastHorizontal);
        animator.SetFloat("Vertical", lastVertical);

        if(horizontal == 0 && vertical == 0)
        {
            switch (DetermineDirection(lastHorizontal, lastVertical))
            {
                case Direction.Down:
                    animator.SetBool("IdleDown", true);
                    break;

                case Direction.Up:
                    animator.SetBool("IdleUp", true);
                    break;

                case Direction.Left:
                    animator.SetBool("IdleLeft", true);
                    break;

                case Direction.Right:
                    animator.SetBool("IdleRight", true);
                    break;

            }
                


        }

        Vector2 direction = new Vector2(horizontal, vertical);

        if (direction.magnitude > 0)
        {
            direction.Normalize();
        }

        rb.velocity = direction * speed;


        // Controlla la direzione in orizzontale del movimento dell ascia & PointSpawn
        switch (lastHorizontal)
        {
            case > 0:
                //Movimento ascia
                Axe.transform.localPosition = new Vector2(0.3f, -0.08f);
                Axe.transform.localScale = new Vector2(0.5f, 0.5f);

                //Movimento PointSpawn 
                PointSpawn.transform.localPosition = new Vector2(1, 0);
                break;
            case < 0:
                //Movimento ascia
                Axe.transform.localPosition = new Vector2(-0.3f, -0.08f);
                Axe.transform.localScale = new Vector2(-0.5f, 0.5f);

                //Movimento PointSpawn 
                PointSpawn.transform.localPosition = new Vector2(-1, 0);
                break;
        }

        // Controlla la direzione in verticale del movimento del PointSpawn degli ortaggi
        switch (lastVertical)
        {
            case > 0:
                PointSpawn.transform.localPosition = new Vector2(0, 1);
                break;
            case < 0:
                PointSpawn.transform.localPosition = new Vector2(0, -1);
                break;
        }

        if (ActivePointPlant == true)
        {
            MovimentoPointSpawn();
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terreno"))
        {
            if (gameObject.CompareTag("BoxPlayer") || gameObject.CompareTag("Player"))
            {
                ActivePointPlant = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terreno") || gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("BoxPlayer") || gameObject.CompareTag("Player"))
            {
                ActivePointPlant = true;
            }
        }
    }
    // Funzione che serve per far muovere l'oggetto PointSpawn sulla Tilemap
    public void MovimentoPointSpawn()
    {
        // Ottieni la posizione della cella corrente in cui si trova il giocatore
        CellPosition = Terrainmap.WorldToCell(transform.position);

        // Ottieni il centro della cella corrente
        Vector3 CellCenter = Terrainmap.GetCellCenterWorld(CellPosition);

        switch (horizontal)
        {
            case > 0:
                // Calcola la posizione della prossima cella aumentando di uno l'indice x rispetto alla cella corrente
                NextCellPosition = new Vector3Int(CellPosition.x + 1, CellPosition.y);
                ;
                // Sposta l'oggetto PointSpawn al centro della prossima cella calcolata da NextCellPosition
                PointSpawn.transform.position = Terrainmap.GetCellCenterWorld(NextCellPosition);
                break;

            case < 0:
                // Calcola la posizione della prossima cella aumentando di uno l'indice x rispetto alla cella corrente
                NextCellPosition = new Vector3Int(CellPosition.x - 1, CellPosition.y);

                // Sposta l'oggetto PointSpawn al centro della prossima cella calcolata da NextCellPosition
                PointSpawn.transform.position = Terrainmap.GetCellCenterWorld(NextCellPosition);
                break;
        }

        switch (vertical)
        {
            case > 0:
                // Calcola la posizione della prossima cella aumentando di uno l'indice x rispetto alla cella corrente
                NextCellPosition = new Vector3Int(CellPosition.x, CellPosition.y + 1);

                // Sposta l'oggetto PointSpawn al centro della prossima cella calcolata da NextCellPosition
                PointSpawn.transform.position = Terrainmap.GetCellCenterWorld(NextCellPosition);
                break;

            case < 0:
                // Calcola la posizione della prossima cella aumentando di uno l'indice x rispetto alla cella corrente
                NextCellPosition = new Vector3Int(CellPosition.x, CellPosition.y - 1);

                // Sposta l'oggetto PointSpawn al centro della prossima cella calcolata da NextCellPosition
                PointSpawn.transform.position = Terrainmap.GetCellCenterWorld(NextCellPosition);
                break;
        }
    }

    private enum Direction 
     { 
        Down, 
        Up, 
        Left, 
        Right,
     }

    private Direction DetermineDirection(float Horizontal , float Vertical)
    {
        if (lastVertical < 0)
        {
            return Direction.Down;
        }
        else if (lastVertical > 0)
        {
            return Direction.Up;
        }
        else if (lastHorizontal < 0)
        {
            return Direction.Left;
        }
        else if(lastHorizontal > 0)
        {
             return Direction.Right;
        }
        else
        {
            return Direction.Down;
        }
    }

  
}