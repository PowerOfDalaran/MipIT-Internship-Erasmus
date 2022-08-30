using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text levelText;

    int score = 0;
    int level = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText.text = score.ToString() + " POINTS";
        levelText.text = "LEVEL: " + level.ToString();
    }

    public void AddPoint()
    {
        score += 1;
        scoreText.text = score.ToString() + " POINTS";
    }
    
    public void AddLevelPoint()
    {
        level += 1;
        levelText.text = "LEVEL: " + level.ToString();
    }
}
