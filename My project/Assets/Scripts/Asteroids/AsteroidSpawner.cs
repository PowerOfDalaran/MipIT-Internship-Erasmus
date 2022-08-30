using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    float maxSpawnRate = 2.0f;
    [SerializeField]
    float minSpawnRate = 4.0f;
    [SerializeField]
    float minXSpawnPoint;
    [SerializeField]
    float maxXSpawnPoint;
    [SerializeField]
    float minYSpawnPoint;
    [SerializeField]
    float maxYSpawnPoint;

    [SerializeField]
    GameObject smallAsteroidPrefab;
    [SerializeField]
    GameObject mediumAsteroidPrefab;
    [SerializeField]
    GameObject bigAsteroidPrefab;
    [SerializeField]
    GameObject hugeAsteroidPrefab;

    void Awake()
    {
        AsteroidOnlyGM.asteroidSpawners.Add(this);
    }

    public void CallAsteroidWave()
    {
        float spawnDelay = Random.Range(minSpawnRate, maxSpawnRate);

        StartCoroutine("SpawnAsteroidsWithDelay", spawnDelay);
    }
    
    //Spawn asteroid
    void spawnAsteroid()
    {
        //Randomizing mass and choosing prefab
        GameObject randomizedPrefab = null;
        float mass = Random.Range(1, 4);

        switch(mass)
        {
            case 4:
                randomizedPrefab = hugeAsteroidPrefab;
                AsteroidOnlyGM.spawnedAsteroids += 15;
                break;
            case 3:
                randomizedPrefab = bigAsteroidPrefab;
                AsteroidOnlyGM.spawnedAsteroids += 7;
                break;
            case 2:
                randomizedPrefab = mediumAsteroidPrefab;
                AsteroidOnlyGM.spawnedAsteroids += 3;
                break;
            case 1:
                randomizedPrefab = smallAsteroidPrefab;
                AsteroidOnlyGM.spawnedAsteroids += 1;
                break;

            default:
                Debug.Log("An error has occured.");
                break;
        }

        float randomizedXPosition = Random.Range(minXSpawnPoint, maxXSpawnPoint);
        float randomizedYPosition = Random.Range(minYSpawnPoint, maxYSpawnPoint);

        Vector2 spawnPoint = new Vector2(randomizedXPosition, randomizedYPosition);

        //Randomizing the rotation of spawned asteroid
        float angle = Random.Range(-15f, 15f);
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        
        //Spawning asteroid and setting its mass
        AsteroidController spawnedAsteroid = Instantiate(randomizedPrefab, spawnPoint, rotation).GetComponent<AsteroidController>();
        spawnedAsteroid.gameObject.GetComponent<Rigidbody2D>().mass = mass;

        //Calculating the direction of generated asteroid and throwing it in chosen direction
        Vector2 direction = rotation * -spawnPoint;
        spawnedAsteroid.ShoveAtRandom(mass, direction);
    }

    IEnumerator SpawnAsteroidsWithDelay(float delay)
    {
        while(AsteroidOnlyGM.asteroidLimit > AsteroidOnlyGM.spawnedAsteroids)
        {
            spawnAsteroid();
            yield return new WaitForSeconds(delay);
        }
    }
}
