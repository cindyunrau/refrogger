using UnityEngine;

public class Home : MonoBehaviour
{
    public GameObject frog;

    public void OnEnable()
    {
        frog.SetActive(true);
    }
    public void OnDisable()
    {
        frog.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            enabled = true;
            FindAnyObjectByType<GameManager>().HomeOccupied();
        }
    }
}
