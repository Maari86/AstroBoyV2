using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreCounter : MonoBehaviour
{
    public static int scoreValue = 0;
    private TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    private ScoreCounter scoreCounter;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
        int highScoreValue = PlayerPrefs.GetInt("HighScore", 0);
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        scoreCounter = FindObjectOfType<ScoreCounter>();

    }

    // Update is called once per frame
    void Update()
    {
        score.text = " " + scoreValue;
        if (scoreValue > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", scoreValue);
            highScore.text = scoreValue.ToString();
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        score.text = "0";
    }
}