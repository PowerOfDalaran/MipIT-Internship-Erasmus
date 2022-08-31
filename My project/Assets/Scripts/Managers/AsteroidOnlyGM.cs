using System.Collections.Generic;
using UnityEngine;

public class AsteroidOnlyGM : MonoBehaviour
{
    [SerializeField]
    public static int spawnedAsteroids = 0;
    [SerializeField]
    public static int asteroidLimit = 5;
    public static int pointsCounter;
    public int roundCounter;
    bool nextRoundReady = false;

    public static List<AsteroidSpawner> asteroidSpawners = new List<AsteroidSpawner>();

    void Update()
    {
        //Checking if player has destroyed all asteroids
        if(spawnedAsteroids == 0)
        {
            nextRoundReady = true;
        }

        //Increasing the number of asteroids and sending new wave
        if(nextRoundReady)
        {
            ScoreManager.instance.AddLevelPoint();

            roundCounter++;
            asteroidLimit += 10 * asteroidSpawners.Count;

            foreach(AsteroidSpawner spawner in asteroidSpawners)
            {
                spawner.CallAsteroidWave();
            }

            nextRoundReady = false;
        }
    }
}
