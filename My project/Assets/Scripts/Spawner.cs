using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Asteroids asteroids;
    float spawnRate = 2.0f;
    float spawnDistance = 14f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", 0f, spawnRate);
    }

    void spawn()
    {
        //spawn point 
        Vector2 spawnPoint = Random.insideUnitCircle.normalized * spawnDistance;

        //rotation
        float angle = Random.Range(-15f, 15f);

        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        Asteroids theAsteroids = Instantiate(asteroids, spawnPoint, rotation);

        //size/direction
        Vector2 direction = rotation * -spawnPoint;
        float mass = Random.Range(0.8f, 1.4f);
        theAsteroids.kick(mass, direction);
    }


}
