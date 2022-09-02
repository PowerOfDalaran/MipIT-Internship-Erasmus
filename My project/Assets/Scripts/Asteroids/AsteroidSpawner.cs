using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] float maxSpawnRate = 1;
    [SerializeField] float minSpawnRate = 3;

    [SerializeField] AsteroidSpawnPoint[] possiblePositions;

    [SerializeField] GameObject smallAsteroidPrefab;
    [SerializeField] GameObject mediumAsteroidPrefab;
    [SerializeField] GameObject bigAsteroidPrefab;
    [SerializeField] GameObject hugeAsteroidPrefab;

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
    
    //Spawning asteroid
    void SpawnAsteroid()
    {
        //Randomizing mass and getting id of the layer
        GameObject randomizedPrefab = null;
        int asteroidLayer = LayerMask.NameToLayer("NewAsteroid");
        float mass = Random.Range(1, 4);

        //Adding one point to counter of existing asteroids
        AsteroidOnlyGM.existingAsteroids++;

        //Choosing prefab basing on randomized mass
        switch(mass)
        {
            case 4:
                randomizedPrefab = hugeAsteroidPrefab;
                break;
            case 3:
                randomizedPrefab = bigAsteroidPrefab;
                break;
            case 2:
                randomizedPrefab = mediumAsteroidPrefab;
                break;
            case 1:
                randomizedPrefab = smallAsteroidPrefab;
                break;
            default:
                Debug.Log("An error has occured.");
                break;
        }

        //Randomizing spawn range from the array and then again randomizing actual spawn point
        int randomizedPositions = Random.Range(0, possiblePositions.Length - 1);

        float randomizedXPosition = Random.Range(possiblePositions[randomizedPositions].minXSpawnPoint, possiblePositions[randomizedPositions].maxXSpawnPoint);
        float randomizedYPosition = Random.Range(possiblePositions[randomizedPositions].minYSpawnPoint, possiblePositions[randomizedPositions].maxYSpawnPoint);

        Vector2 spawnPoint = new Vector2(randomizedXPosition, randomizedYPosition);

        //Randomizing the rotation of spawned asteroid
        float angle = Random.Range(-15f, 15f);
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        
        //Spawning asteroid, setting its mass and layer
        AsteroidController spawnedAsteroid = Instantiate(randomizedPrefab, spawnPoint, rotation).GetComponent<AsteroidController>();
        
        spawnedAsteroid.gameObject.GetComponent<Rigidbody2D>().mass = mass;
        spawnedAsteroid.gameObject.layer = asteroidLayer;
        
        //Calculating the direction of generated asteroid and throwing it in chosen direction
        Vector2 direction = rotation * -spawnPoint;
        spawnedAsteroid.ShoveAtRandom(mass, direction);
    }

    //Enumerator initializating asteroid spawning (with delay), while number of existing asteroids is smaller than chosen limit
    IEnumerator SpawnAsteroidsWithDelay(float delay)
    {
        while(AsteroidOnlyGM.asteroidLimit > AsteroidOnlyGM.existingAsteroids)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(delay);
        }
    }
}
