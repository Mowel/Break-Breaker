using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private GameManager gm;

    public float rightScreenEdge;
    public float leftScreenEdge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            return;
        }
        
//        float horizontal = Input.acceleration.x * speed;
//        float horizontal = Input.GetAxis("Horizontal");
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
        if (transform.position.x < leftScreenEdge)
        {
            transform.position = new Vector2(leftScreenEdge, transform.position.y);
        }
        if (transform.position.x > rightScreenEdge)
        {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("extraLife"))
        {
            gm.UpdateLives(1);
            Destroy(other.gameObject);
        }   
    }
}
