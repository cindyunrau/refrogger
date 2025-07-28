using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Frog : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite leapSprite;
    public Sprite deadSprite;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public Vector3 spawnPosition;
    private float farthestRow;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Can only move if idle
        if (spriteRenderer.sprite == idleSprite)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                Move(Vector2.right);

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                Move(Vector2.left);

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Move(Vector2.up);

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                Move(Vector2.down);

            }
        }

    }

    private void Move(Vector3 direction)
    {
        Vector3 destination = transform.position + direction;

        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));

        if (barrier)
        {
            return;
        }

        if (platform)
        {
            print("set parent platform");
            transform.SetParent(platform.transform);
        }
        else
        {
            transform.SetParent(null);
        }

        if (obstacle && !platform)
        {
            print("killed on position");

            StartCoroutine(Leap(destination, true));
        }
        else
        {
            if(destination.y > farthestRow)
            {
                farthestRow = destination.y;
                FindObjectOfType<GameManager>().AdvancedRow();
            }

            StartCoroutine(Leap(destination, false));
        }
    }

    private IEnumerator Leap(Vector3 destination, bool died)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;
        float duration = 0.125f;

        spriteRenderer.sprite = leapSprite;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.sprite = idleSprite;
        transform.position = destination;

        if (died)
        {
            KillFrogger();
        }
    }

    public void KillFrogger()
    {
        print("DEAD");

        StopAllCoroutines();
        
        transform.rotation = Quaternion.identity;
        transform.SetParent(null);
        spriteRenderer.sprite = deadSprite;
        enabled = false;

        FindObjectOfType<GameManager>().Died();
    }

    public void Respawn()
    {
        StopAllCoroutines();

        gameObject.SetActive(true);
        transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        farthestRow = spawnPosition.y;
        spriteRenderer.sprite = idleSprite;
        enabled = true;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled && other.gameObject.layer == LayerMask.NameToLayer("Obstacle") && transform.parent == null)
        {
            print("killed on impact");
            KillFrogger();
        }

    }

}


