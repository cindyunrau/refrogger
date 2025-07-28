using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour
{
    public Rigidbody2D rb;

    public float minSpeed = 8f;
    public float maxSpeed = 12f;
    public float speed = 1f;

    void FixedUpdate()
    {
        Vector2 forward = new(transform.right.x, transform.right.y);
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * forward);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("entityDeleter"))
        {
            Destroy(gameObject);
        }
    }
}
