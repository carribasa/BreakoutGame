using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 300;
    private Vector2 velocity;
    private Vector2 startPosition;
    public AudioSource audioSource;
    public AudioClip playerSound, brickSound, wallSound, deadSound;
    private float magnitude;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float angle;
    public bool entrar = false;

    [SerializeField]
    private GameManager gameManager;

    void Start()
    {
        startPosition = transform.position;
        ResetBall();
    }

    private void Update()
    {
        angle = velocity.y;
        if(magnitude == 0 && rb.velocity != Vector2.zero)
        {
             magnitude = rb.velocity.magnitude;
        }
        currentSpeed = rb.velocity.magnitude;
        ThrowBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WallDeath"))
        {
            FindObjectOfType<GameManager>().LoseHealth();
            audioSource.clip = deadSound;
            audioSource.Play();
            ResetBall();
        }
        if (collision.gameObject.GetComponent<Player>())
        {
            audioSource.clip = playerSound;
            audioSource.Play();
            angle = Mathf.Abs(Vector2.Dot(rb.velocity.normalized, Vector2.up));
        }

        if (collision.gameObject.GetComponent<Brick>())
        {
            audioSource.clip = brickSound;
            audioSource.Play();
            angle = Mathf.Abs(Vector2.Dot(rb.velocity.normalized, Vector2.up));

            // When hits a Brick
            if (entrar)
            {
                
            }
        }
        if (collision.gameObject.CompareTag("Walls"))
        {
            audioSource.clip = wallSound;
            audioSource.Play();
        }

        CollisionToPlayer(collision);
    }

    public void ResetBall()
    {
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
    }

    private void CollisionToPlayer(Collision2D collisionObject)
    {
        if (collisionObject.gameObject.CompareTag("Player"))
        {
            float widthPaddle = collisionObject.transform.localScale.x;
            Vector2 hitPosition = transform.position - collisionObject.transform.position;
         
            float normalizedValue = Mathf.Clamp((hitPosition.x + (widthPaddle / 2)) / widthPaddle, 0, 1);

            float angleVectorPaddleInDegrees = Mathf.Lerp(150, 30, normalizedValue);
            var paddleDirection = Quaternion.AngleAxis(angleVectorPaddleInDegrees, Vector3.forward);

            var newPaddleDirection = paddleDirection * Vector2.right * 10;

            var newVelocity = (rb.velocity + new Vector2(newPaddleDirection.x, newPaddleDirection.y)).normalized * rb.velocity.magnitude;

            rb.velocity = newVelocity.normalized * magnitude;
        }
    }
    void ThrowBall()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity == Vector2.zero)
        {
            velocity.x = Random.Range(-1f, 1f);
            velocity.y = 1;
            rb.AddForce(velocity.normalized * speed);

        }
    }
}
