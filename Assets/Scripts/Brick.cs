using TMPro;
using UnityEngine;

public class Brick : MonoBehaviour
{
    // atrributes --------------------------------------------------
    // ------------------------------------------------------------
    private int lives = 2;
    private SpriteRenderer spriteRenderer;
    public BrickColor brickColor;
    private Sprite spriteIdle;
    private Sprite spriteBroken;
    public TMP_Text scoreText;
    public GameManager gameManager;

    // ------------------------------------------------------------

    private void Awake()
    {
        setBrickSprite();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // ------------------------------------------------------------

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitBrick(collision);
    }

    // set brick sprite when hit -----------------------------------
    void setBrickSprite()
    {
        switch (brickColor)
        {
            case BrickColor.purple:
                spriteIdle = Resources.Load<Sprite>("BrickPurple");
                spriteBroken = Resources.Load<Sprite>("BrickPurpleB");
                break;
            case BrickColor.blue:
                spriteIdle = Resources.Load<Sprite>("BrickBlue");
                spriteBroken = Resources.Load<Sprite>("BrickBlueB");
                break;
            case BrickColor.orange:
                spriteIdle = Resources.Load<Sprite>("BrickOrange");
                spriteBroken = Resources.Load<Sprite>("BrickOrangeB");
                break;
            case BrickColor.yellow:
                spriteIdle = Resources.Load<Sprite>("BrickYellow");
                spriteBroken = Resources.Load<Sprite>("BrickYellowB");
                break;
            case BrickColor.red:
                spriteIdle = Resources.Load<Sprite>("BrickRed");
                spriteBroken = Resources.Load<Sprite>("BrickRedB");
                break;
        }

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteIdle;
    }

    // brick is hit by ball -----------------------------------
    void hitBrick(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            lives--;
            if (lives <= 0)
            {
                setBrickPoints();
                FindAnyObjectByType<GameManager>().CheckLevelCompleted();
                Destroy(gameObject);
            }
            else
            {
                setBrickPoints();
                spriteRenderer.sprite = spriteBroken;
            }
        }
    }

    // quantity of points per brick -------------------------------
    void setBrickPoints()
    {
        int points = 0;
        switch (brickColor)
        {
            case BrickColor.purple:
                points = 300;
                break;
            case BrickColor.blue:
                points = 25;
                break;
            case BrickColor.orange:
                points = 150;
                break;
            case BrickColor.yellow:
                points = 50;
                break;
            case BrickColor.red:
                points = 100;
                break;
        }
        gameManager.increaseScore(points);
    }


    // type of bricks ---------------------------------------------
    public enum BrickColor
    {
        blue, orange, yellow, red, purple
    }
}