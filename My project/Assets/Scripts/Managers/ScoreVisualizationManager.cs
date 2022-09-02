using UnityEngine;
using UnityEngine.UI;

public class ScoreVisualizationManager : MonoBehaviour
{
    int score = 0;
    int level = 1;

    public static ScoreVisualizationManager instance;

    [SerializeField] Text[] scoreText;
    [SerializeField] Text[] levelText;

    private void Awake()
    {
        //Setting the static "instance" variable to this instance of the class
        instance = this;
    }

    void Start()
    {
        //Setting all text variables of ui objects to proper values
        foreach(Text uiText in scoreText)
        {
            uiText.text = " POINTS: " + score.ToString();
        }

        foreach(Text uiText in levelText)
        {
            uiText.text = " LEVEL: " + level.ToString();
        }
    }

    //Incrementing the number of points and updating all ui objects
    public void AddPoint()
    {
        score += 1;
        
        foreach(Text uiText in scoreText)
        {
            uiText.text = " POINTS: " + score.ToString();
        }
    }
    
    //Incrementing the number of level and updating all ui objects
    public void AddLevelPoint()
    {
        level += 1;
        
        foreach(Text uiText in levelText)
        {
            uiText.text = " LEVEL: " + level.ToString();
        }
    }
}
