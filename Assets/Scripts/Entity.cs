using UnityEngine;

//public abstract class Entity : MonoBehaviour
public class Entity : MonoBehaviour
{
    public string Type { get; private set; }

    public Rigidbody2D rb;
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
