using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Move_Player : MonoBehaviour, IData
{
    // Componenti
    private Rigidbody2D rb;
    private Animator animator;

    // Movimento
    private Vector2 movementInput;
    public float lastHorizontal;
    public float lastVertical;
    public float speed = 7f;

    // Input System
    public InputActionReference MovePlayer_KeyBoard;
    public InputActionReference MovePlayer_Controller;

    // Stato di movimento tramite pulsanti mobili
    private Vector2 mobileMovementDirection = Vector2.zero;
    private bool isMobileMovementPressed = false;

    // Oggetti e Tilemap
    public Tilemap Terrainmap;
    public GameObject PointSpawn;
    public GameObject Axe;
    public bool ActivePointPlant = false;

    // Posizioni delle celle
    public Vector3Int CellPosition; // Posizione della cella corrente
    public Vector3Int NextCellPosition; // Posizione della prossima cella da raggiungere

    public float test;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void LoadData(GameData data)
    {
        transform.position = data.PositionPlayer;
    }

    public void SaveData(GameData data)
    {
        data.PositionPlayer = transform.position;
    }

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

    void Update()
    {
        //MovePointSpawn();
        HandleInput();
        HandleMovement();
        HandleAnimation();
        HandleAxePosition();
    }

    private void HandleInput()
    {
        // Legge gli input da tastiera e controller e li combina
        Vector2 keyboardMovement = MovePlayer_KeyBoard.action.ReadValue<Vector2>();
        Vector2 controllerMovement = MovePlayer_Controller.action.ReadValue<Vector2>();

        movementInput = (keyboardMovement + controllerMovement).normalized;

        // Se c'è movimento mobile attivo, sovrascrive gli input di tastiera/controller
        if (isMobileMovementPressed)
        {
            movementInput = mobileMovementDirection;
        }

        // Salva l'ultimo movimento se il giocatore si sta muovendo
        if (movementInput.magnitude > 0)
        {
            lastHorizontal = movementInput.x;
            lastVertical = movementInput.y;
        }
    }

    private void HandleMovement()
    {
        // Muovi il personaggio
        rb.velocity = movementInput * speed;

        // Muovi PointSpawn se necessario
        if (ActivePointPlant)
        {
           MovePointSpawn();
        }
    }

    private void HandleAnimation()
    {
        // Imposta i parametri di movimento per l'animatore
        animator.SetFloat("Horizontal", lastHorizontal);
        animator.SetFloat("Vertical", lastVertical);

        // Se il personaggio è fermo, mantieni l'ultima direzione per l'animazione di Idle
        if (movementInput.magnitude == 0)
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
        else
        {
            // Resetta Idle se il giocatore si sta muovendo
            ResetIdleAnimation();
        }
    }

    private void HandleAxePosition()
    {
        // Imposta la posizione e la scala dell'ascia in base al movimento
        if (lastHorizontal > 0)
        {
            Axe.transform.localPosition = new Vector2(0.3f, -0.08f);
            Axe.transform.localScale = new Vector2(0.5f, 0.5f);
        }
        else if (lastHorizontal < 0)
        {
            Axe.transform.localPosition = new Vector2(-0.3f, -0.08f);
            Axe.transform.localScale = new Vector2(-0.5f, 0.5f);        
        }
    }

    // fa  muovere l'oggetto PointSpawn sulla Tilemap
    private void MovePointSpawn()
    {
        // Ottiene la posizione del player sulla griglia
        Vector3Int playerCell = Terrainmap.WorldToCell(transform.position);
        Vector3 centerOfPlayerCell = Terrainmap.GetCellCenterWorld(playerCell);

        // Calcola la cella di destinazione in base alla direzione
        Vector3Int targetCell = playerCell;


        //========DEBUG========//
        //test += Time.deltaTime;

        if(test >3)
        {
            //Debug.Log(targetCell);
            //Debug.Log(centerOfPlayerCell);
            test = 0;
        }
        
        // Disegna una croce al centro della cella
        Debug.DrawLine(centerOfPlayerCell + Vector3.left * 0.5f, centerOfPlayerCell + Vector3.right * 0.5f, Color.red);
        Debug.DrawLine(centerOfPlayerCell + Vector3.up * 0.5f, centerOfPlayerCell + Vector3.down * 0.5f, Color.red);

        // Se il player è abbastanza vicino al centro della cella
        if (Vector3.Distance(transform.position , centerOfPlayerCell) < 0.5f)
        { 
            if (lastHorizontal > 0)
            {
                targetCell.x += 1;  // Destra
                centerOfPlayerCell.x += 1;
            }
            else if (lastHorizontal < 0)
            {
                targetCell.x -= 1;  // Sinistra
                centerOfPlayerCell.x -= 1;
            }
            else if (lastVertical > 0)
            {
                targetCell.y += 1;  // Su
                centerOfPlayerCell.y += 1;
            }
            else if (lastVertical < 0)
            {
                targetCell.y -= 1;  // Giù
                centerOfPlayerCell.y -= 1;
            }
        }


        // Muovi il PointSpawn alla cella di destinazione
        Vector3 targetPosition = Terrainmap.GetCellCenterWorld(targetCell);
        PointSpawn.transform.position = targetPosition;
    }


    private void ResetIdleAnimation()
    {
        animator.SetBool("IdleDown", false);
        animator.SetBool("IdleUp", false);
        animator.SetBool("IdleLeft", false);
        animator.SetBool("IdleRight", false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terreno"))
        {
            ActivePointPlant = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terreno"))
        {
            ActivePointPlant = false;
        }
    }
    private Direction DetermineDirection(float horizontal, float vertical)
    {
        if (vertical < 0)
        {
            return Direction.Down;
        }
        else if (vertical > 0)
        {
            return Direction.Up;
        }
        else if (horizontal < 0)
        {
            return Direction.Left;
        }
        else
        {
            return Direction.Right;
        }
    }
    private enum Direction
    {
        Down,
        Up,
        Left,
        Right
    }
    
    /* Movimento per  mobile (pulsanti su/giù/sinistra/destra) 
  
    public void MovementMobile_Down()
    {
        SetMobileMovement(0f, -1f);
    }

    public void MovementMobile_Down_Up()
    {
        StopMobileMovement();
    }

    public void MovementMobile_Up()
    {
        SetMobileMovement(0f, 1f);
    }

    public void MovementMobile_Up_Up()
    {
        StopMobileMovement();
    }

    public void MovementMobile_Left()
    {
        SetMobileMovement(-1f, 0f);
    }

    public void MovementMobile_Left_Up()
    {
        StopMobileMovement();
    }

    public void MovementMobile_Right()
    {
        SetMobileMovement(1f, 0f);
    }

    public void MovementMobile_Right_Up()
    {
        StopMobileMovement();
    }

    private void SetMobileMovement(float x, float y)
    {
        mobileMovementDirection = new Vector2(x, y).normalized;
        isMobileMovementPressed = true;
    }

    private void StopMobileMovement()
    {
        isMobileMovementPressed = false;
    }
    */

}
