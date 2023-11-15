using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    private float inputValue;
    public float moveSpeed = 25;
    private Vector2 direction;
    private Vector2 startPosition;

    public void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Aqui se almacenará un valor de -1, 0 ó 1 dependiendo de la dirección
        inputValue = Input.GetAxisRaw("Horizontal");

        switch (inputValue)
        {
            case -1:
                direction = Vector2.left;
                break;
            case 0:
                direction = Vector2.zero;
                break;
            case 1:
                direction = Vector2.right;
                break;
        }

        rb.AddForce(direction * moveSpeed * Time.deltaTime * 100);
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
    }

}
