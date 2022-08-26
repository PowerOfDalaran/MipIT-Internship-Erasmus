using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public AsteroidController asteroids;
    float spawnRate = 2.0f;
    float spawnDistance = 14f;

    void Start()
    {
        InvokeRepeating("spawn", 0f, spawnRate);
    }

    //Method spawning asteroids, randomizing their mass and rotation, calculating their direction and setting them into motion
    void spawnAsteroid()
    {
        //Choosing position for spawning the asteroid
        Vector2 spawnPoint = Random.insideUnitCircle.normalized * spawnDistance;

        //Randomizing the position of spawned asteroid
        float angle = Random.Range(-15f, 15f);

        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        AsteroidController theAsteroids = Instantiate(asteroids, spawnPoint, rotation);

        //Calculating the direction of generated asteroid
        Vector2 direction = rotation * -spawnPoint;

        //Randomizing the mass of asteroid and throwing it in chosen direction
        float mass = Random.Range(0.8f, 1.4f);
        theAsteroids.ShoveAtRandom(mass, direction);
    }
}
