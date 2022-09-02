using UnityEngine;

//Class for defining spawn ranges for AsteroidSpawner
public class AsteroidSpawnPoint : MonoBehaviour
{
    [SerializeField] public float minXSpawnPoint;
    [SerializeField] public float maxXSpawnPoint;
    [SerializeField] public float minYSpawnPoint;
    [SerializeField] public float maxYSpawnPoint;
}
