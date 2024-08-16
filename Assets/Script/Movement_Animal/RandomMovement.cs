using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float speed = 3f;
    public float changeDirectionTime = 3f;

    public Vector2 movementDirection;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirectionRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity =  movementDirection * speed;
    }
    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            changeDirection();
            yield return new WaitForSeconds(changeDirectionTime); 
        }
    }

    public void changeDirection()
    {
        float angle = Random.Range(0f, 360f);
        movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        changeDirection();
    }
}
