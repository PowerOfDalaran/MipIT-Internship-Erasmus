using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] float maxSpawnRate = 1;
    [SerializeField] float minSpawnRate = 3;

    [SerializeField] AsteroidSpawnPoint[] possibleSpawnPoints;

    [SerializeField] GameObject asteroidPrefab;

    void Awake()
    {
        CallAsteroidWave();
    }

    //Randomizing spawn rate and calling new wave
    public void CallAsteroidWave()
    {
        float spawnDelay = Random.Range(minSpawnRate, maxSpawnRate);

        StartCoroutine("SpawnAsteroidsWithDelay", spawnDelay);
    }

    //Enumerator initializating asteroid spawning (with delay), while number of existing asteroids is smaller than chosen limit
    IEnumerator SpawnAsteroidsWithDelay(float delay)
    {
        while(AsteroidOnlyGM.asteroidLimit > AsteroidOnlyGM.existingAsteroids)
        {
            //Randomizing spawn range from the array and then again randomizing actual spawn point
            int randomizedSpawnPoint = Random.Range(0, possibleSpawnPoints.Length);

            float randomizedXPosition = Random.Range(possibleSpawnPoints[randomizedSpawnPoint].minXSpawnPoint, possibleSpawnPoints[randomizedSpawnPoint].maxXSpawnPoint);
            float randomizedYPosition = Random.Range(possibleSpawnPoints[randomizedSpawnPoint].minYSpawnPoint, possibleSpawnPoints[randomizedSpawnPoint].maxYSpawnPoint);

            Vector2 chosenSpawnPoint = new Vector2(randomizedXPosition, randomizedYPosition);

            //Spawning asteroid on randomized point and waiting the delay time
            SpawnAsteroid(chosenSpawnPoint);
            yield return new WaitForSeconds(delay);
        }
    }

    //Spawning asteroid in chosen point
    void SpawnAsteroid(Vector2 spawnPoint)
    {
        //Accessing id of the layer and randomizing mass
        int asteroidLayer = LayerMask.NameToLayer("NewAsteroid");
        int mass = Random.Range(1, 5);

        //Adding point to the counter of existing asteroids, randomizing angle and creating rotation for new asteroid
        AsteroidOnlyGM.existingAsteroids++;

        float angle = Random.Range(-15f, 15f);
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        
        //Instantiating new asteroid and calling its initializating method
        AsteroidController spawnedAsteroid = Instantiate(asteroidPrefab, spawnPoint, rotation).GetComponent<AsteroidController>();
        
        spawnedAsteroid.InitializateAsteroid(mass);

        //Setting asteroid layer and shoving it in calculated direction
        spawnedAsteroid.gameObject.layer = asteroidLayer;
        
        Vector2 direction = rotation * -spawnPoint;
        spawnedAsteroid.ShoveAtRandom(direction);
    }
}
