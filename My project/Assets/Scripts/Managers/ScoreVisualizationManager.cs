using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreVisualizationManager : MonoBehaviour
{
    public static ScoreVisualizationManager instance;

    [SerializeField] Text[] scoreText;
    [SerializeField] Text[] levelText;

    int score = 0;
    int level = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach(Text uiText in scoreText)
        {
            uiText.text = " POINTS: " + score.ToString();
        }

        foreach(Text uiText in levelText)
        {
            uiText.text = " LEVEL: " + level.ToString();
        }
    }

    public void AddPoint()
    {
        score += 1;
        
        foreach(Text uiText in scoreText)
        {
            uiText.text = " POINTS: " + score.ToString();
        }
    }
    
    public void AddLevelPoint()
    {
        level += 1;
        
        foreach(Text uiText in levelText)
        {
            uiText.text = " LEVEL: " + level.ToString();
        }
    }
}
