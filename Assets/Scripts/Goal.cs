using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("WE WON!");
            Score.CurrScore += 100;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
