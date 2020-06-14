using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public float speed;
    [SerializeField] private Transform explosion;
    [SerializeField] private Transform powerUp;
    [SerializeField] private GameManager gm;
    public Transform paddle;
    public bool inPlay;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            return;
        }
        if (!inPlay)
        {
            transform.position = paddle.position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !inPlay) 
//        if (Input.GetButtonDown("Jump")&& !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bottom"))
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("brick"))
        {
            Brick brick = other.gameObject.GetComponent<Brick>();
            if (brick.hitsToBreak > 1)
            {
                brick.BreakBrick();
            }
            else
            {
                int randChance = Random.Range(1, 101);
                if (randChance < 50)
                {
                    Instantiate(powerUp, other.transform.position, other.transform.rotation);
                }
            
                Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(newExplosion.gameObject, 2.5f);
            
                gm.UpdateScore(brick.points);
                gm.UpdateNumberOfBricks();
            
                Destroy(other.gameObject);
            }
        }
    }
}
