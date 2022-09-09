using System.Collections;
using RiptideNetworking;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicServer : MonoBehaviour
{
    private static GameLogicServer _singleton;
    public static GameLogicServer Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(GameLogicServer)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    public GameObject PlayerPrefab => playerPrefab;
    public GameObject BulletPrefab => bulletPrefab;

    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        Singleton = this;
    }
}
