using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxOxygenCapacity = 1;
    public static float oxygenTank = 0; // players health
    public bool onOxygenPad = false;

    public int score = 0;
    public Text scoreText;

    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    public int jumpForce = 1000;
    public Sprite[] rightSprites = new Sprite[3];
    public Sprite[] leftSprites = new Sprite[3];
    public Sprite centerSprite;
    public float speed = 3.0f;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        oxygenTank = maxOxygenCapacity;
        updateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0);
        var sprite = 0;
        float time = (Time.fixedTime % 1);
        if (time < 0.33)
        {
            sprite = 0;
        }
        else if (time < 0.66)
        {
            sprite = 1;
        }
        else sprite = 2;

        if (move.x > 0)
        {
            spriteRenderer.sprite = rightSprites[sprite];
        } else if (move.x < 0)
        {
            spriteRenderer.sprite = leftSprites[sprite];
        } else
        {
            spriteRenderer.sprite = centerSprite;
        }

        transform.position += move * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if(onOxygenPad)
        {
            if (oxygenTank < maxOxygenCapacity)
            {
                IncreaseOxygenOverTime();
            }
        } else
        {
            if (oxygenTank > 0)
            {
                LoseOxygenOverTime();
            } else
            {
                // Game Over
            }
        }

    }

    void LoseOxygenOverTime()
    {
        if(onOxygenPad == false && oxygenTank > 0)
        {
            oxygenTank -= 0.05f * Time.deltaTime;
        }
    }

    void IncreaseOxygenOverTime()
    {
        if (onOxygenPad == true && oxygenTank > 0)
        {
            oxygenTank += 0.05f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("OxygenPad"))
        {
            onOxygenPad = true;
        }

        if (collision.gameObject.tag == "Gem")
        {
            score += 10;
            updateScoreText();
            collision.gameObject.SetActive(false);
        }

        if(collision.gameObject.tag == "Enemy")
        {

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("OxygenPad"))
        {
            onOxygenPad = false;
        }
    }

    void updateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
