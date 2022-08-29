using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour
{
    public AsteroidController asteroids;
    float spawnRate = 4.0f;
    float spawnDistance = 14f;
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
        InvokeRepeating("spawnAsteroid", 0f, spawnRate);
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

        //Choosing position for spawning the asteroid
        //Vector2 spawnPoint = Random.insideUnitCircle.normalized * spawnDistance;
        //Vector2 spawnPoint = new Vector2(-41, 37);

        Vector2 spawnPoint = GameObject.Find("AsteroidSpawner1").transform.position;

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
}
