using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Home[] homes;
    private Frog player;

    public GameObject gameOverMenu;
    public Text scoreUI;
    public GameObject livesUI;
    public GameObject timerUI;

    public Sprite timer25;
    public Sprite timer50;
    public Sprite timer75;
    public Sprite timer100;


    private int score;
    private int lives;
    private float time;

    private int TIMER_DURATION = 32;

    private int numTimeblocks;
    private float timeSegments;
    private int numActiveblocks;


    private void Awake()
    {
        homes = FindObjectsByType<Home>(FindObjectsSortMode.None);
        player = FindAnyObjectByType<Frog>();
    }

    private void Start()
    {
        NewGame();

        numTimeblocks = timerUI.transform.childCount;
        timeSegments = (float)TIMER_DURATION / (float)numTimeblocks;
        numActiveblocks = numTimeblocks;
    }

    private void Update()
    {
        if (time <= (numActiveblocks * timeSegments) && numActiveblocks > 0)
        {
            GameObject timeBlock = timerUI.transform.GetChild(numActiveblocks - 1).gameObject;
            float chunk = (numActiveblocks * timeSegments) - time;

            if (chunk <= 1 * (timeSegments / 4))
            {
                
            }
            else if (chunk <= 2 * (timeSegments / 4))
            {
                timeBlock.GetComponent<SpriteRenderer>().sprite = timer75;
            }
            else if (chunk <= 3 * (timeSegments / 4))
            {
                timeBlock.GetComponent<SpriteRenderer>().sprite = timer50;
            }
            else if (chunk <= 4 * (timeSegments / 4))
            {
                timeBlock.GetComponent<SpriteRenderer>().sprite = timer25;
            }
            else
            {
                timeBlock.SetActive(false);
                numActiveblocks--;
            }

        }
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

        foreach (Transform child in timerUI.transform)
        {
            child.gameObject.SetActive(true);
            child.gameObject.GetComponent<SpriteRenderer>().sprite = timer100;
        }

        StopAllCoroutines();
        StartCoroutine(Timer(TIMER_DURATION));
    }

    private IEnumerator Timer(int duration)
    {
        float interval = 0.01f;
        time = duration;
        numActiveblocks = numTimeblocks;

        while (time > 0)
        {
            yield return new WaitForSeconds(interval);

            time -= interval;
        }
        player.KillFrogger();
    }
    public void Died()
    {
        SetLives(lives - 1);
        if (lives > 0)
        {
            Invoke(nameof(Respawn), 2f);
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

        int bonusPoints = (int)(time * 20);
        AddScore(50 + bonusPoints);

        if (Cleared())
        {
            AddScore(1000);
            Invoke(nameof(NewLevel), 1f);
        }
        else
        {
            Invoke(nameof(Respawn), 1f);
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
        SetScore(this.score + score);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreUI.text = "" + this.score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;

        if (this.lives >= 3)
        {
            foreach (Transform child in livesUI.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            livesUI.transform.GetChild(lives).gameObject.SetActive(false);
        }
    }
}
