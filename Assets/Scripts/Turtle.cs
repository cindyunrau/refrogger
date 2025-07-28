using UnityEngine;
using System.Collections;

public class Turtle : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 1f;
    public bool sinking = false;

    private void Start()
    {
        Vector2 forward = new(transform.right.x, transform.right.y);
        rb.velocity = forward * speed;
    }
    //void FixedUpdate()
    //{
        
    //    rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * forward);
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("entityDeleter"))
        {
            Destroy(gameObject);
        }
    }
}
