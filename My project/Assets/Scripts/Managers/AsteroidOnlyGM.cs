using System.Collections.Generic;
using UnityEngine;

public class AsteroidOnlyGM : MonoBehaviour
{
    [SerializeField] public static int existingAsteroids = 0;
    [SerializeField] public static int asteroidLimit = 2;

    public static int pointsCounter;
    public int roundCounter = 1;

    bool nextRoundReady = false;

    [SerializeField] public AsteroidSpawner asteroidSpawner;

    void Update()
    {
        //Checking if player has destroyed all asteroids
        if(existingAsteroids == 0)
        {
            nextRoundReady = true;
        }

        //Increasing the number of asteroids and sending new wave
        if(nextRoundReady)
        {
            //Increasing level counters
            ScoreVisualizationManager.instance.AddLevelPoint();
            roundCounter++;

            //Increasing number of asteroids and spawning new ones
            asteroidLimit += 10;

            asteroidSpawner.CallAsteroidWave();

            nextRoundReady = false;
        }
    }
}
