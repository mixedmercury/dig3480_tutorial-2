using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    public GameObject loseTextObject;
    public GameObject winTextObject;
    private int scoreValue = 0;
    private int livesValue = 3;
    Animator anim;
    private float yPos;
    private int AnimState;
    public AudioSource backingMusic;
    public AudioSource victoryMusic;

    // Start is called before the first frame update
    void Start()
    {
        backingMusic.Play();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        anim = GetComponent<Animator>();
        yPos = transform.position.y;
        AnimState = anim.GetInteger("State");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimState = anim.GetInteger("State");
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (AnimState != 4)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                anim.SetInteger("State", 2);            
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                anim.SetInteger("State", 3);
            }        
            if (Input.GetAxis("Horizontal") == 0)
            {
                anim.SetInteger("State", 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("State",4);
            }
        }
        yPos = transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(other.gameObject);
            if (scoreValue == 4)
            {
                transform.position = new Vector2(80,1);
                livesValue = 3;
                lives.text = "Lives: " + livesValue.ToString();
            }
            if (scoreValue == 8)
            {
                winTextObject.SetActive(true);
                backingMusic.Stop();
                victoryMusic.Play();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            if (scoreValue < 8)
            {
                livesValue -= 1;
                lives.text = "Lives: " + livesValue.ToString();
                if (livesValue < 1)
                {
                    gameObject.SetActive(false);
                    loseTextObject.SetActive(true);
                }
            }
        } 

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (AnimState == 4)
            {
                anim.SetInteger("State",5);
            }
            if (yPos == transform.position.y)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rd2d.AddForce(new Vector2(0, 8), ForceMode2D.Impulse); //the 3 is the player's "jumpforce,"
                }
            }
        }
    }
}
