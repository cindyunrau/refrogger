using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private Home[] homes;
    private Frog player;

    public GameObject gameOverMenu;

    private int score;
    private int lives;
    private int time;

    private void Awake()
    {
        homes = FindObjectsByType<Home>(FindObjectsSortMode.None);
        player = FindAnyObjectByType<Frog>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        gameOverMenu.SetActive(false);
        SetScore(0);
        SetLives(3);
        NewLevel();
    }

    private void NewLevel()
    {
        for (int i = 0; i < homes.Length; i++)
        {
            homes[i].enabled = false;
        }
        Respawn();
    }

    private void Respawn()
    {
        player.Respawn();

        StopAllCoroutines();
        StartCoroutine(Timer(30));
    }

    private IEnumerator Timer(int duration)
    {
        time = duration;

        while (time > 0)
        {
            yield return new WaitForSeconds(1);

            time--;
        }
        player.KillFrogger();
    }
    public void Died()
    {
        SetLives(lives - 1);
        if (lives > 0)
        {
            Invoke(nameof(Respawn), 1f);
        }
        else
        {
            Invoke(nameof(GameOver), 1f);
        }
    }

    private void GameOver()
    {
        player.gameObject.SetActive(false);
        gameOverMenu.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(PlayAgain());
    }
    private IEnumerator PlayAgain()
    {
        bool playAgain = false;

        while (!playAgain)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                playAgain = true;
            }

            yield return null;
        }

        NewGame();
    }
    public void AdvancedRow()
    {
        AddScore(10);
    }

    public void HomeOccupied()
    {
        player.gameObject.SetActive(false);

        int bonusPoints = time * 20;
        AddScore(50+ bonusPoints);

        if (Cleared())
        {
            AddScore(1000); 
            Invoke(nameof(NewLevel),1f);
        }
        else {
            Invoke(nameof(Respawn),1f);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < homes.Length; i++)
        {
            if (!homes[i].enabled)
            {
                return false;
            }
        }
        return true;
    }

    private void AddScore(int score)
    {
        this.score += score;
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }
}
