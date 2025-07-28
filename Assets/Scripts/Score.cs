using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int CurrScore = 0;

    public Text scoreText;

    void Start()
    {
        scoreText.text = CurrScore.ToString();
    }
}
