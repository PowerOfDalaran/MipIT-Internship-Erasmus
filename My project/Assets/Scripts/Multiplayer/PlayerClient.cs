using RiptideNetworking;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClient : MonoBehaviour
{
    public static Dictionary<ushort, PlayerClient> list = new Dictionary<ushort, PlayerClient>();

    public ushort Id { get; private set; }
    public bool IsLocal { get; private set; }
    public bool IsAlive => health > 0f;

    [SerializeField] private float maxHealth;
    [SerializeField] private Transform camTransform;

    private string username;
    private float health;

    private void Start()
    {
        health = maxHealth;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        list.Remove(Id);
    }

    private void Move(Vector2 newPosition, Vector2 forward)
    {
        transform.position = newPosition;

        if(!IsLocal)
        {
            camTransform.forward = forward;
        }
    }

    public void SetHealth(float amount)
    {
        health = Mathf.Clamp(amount, 0f, maxHealth);
        UIManagerClient.Singleton.HealthUpdated(health, maxHealth, true);
    }

    public void Died(Vector2 position)
    {
        transform.position = position;
        health = 0f;
        model.SetActive(false);

        if (IsLocal)
            UIManagerClient.Singleton.HealthUpdated(health, maxHealth, true);
    }

    public void Respawned(Vector2 position)
    {
        transform.position = position;
        health = maxHealth;

        if (IsLocal)
            UIManagerClient.Singleton.HealthUpdated(health, maxHealth, false);
    }

    public static void Spawn(ushort id, string username, Vector3 position)
    {
        PlayerClient player;
        if (id == NetworkManagerClient.Singleton.Client.Id)
        {
            player = Instantiate(GameLogicClient.Singleton.LocalPlayerPrefab, position, Quaternion.identity).GetComponent<PlayerClient>();
            player.IsLocal = true;
        }
        else
        {
            player = Instantiate(GameLogicClient.Singleton.PlayerPrefab, position, Quaternion.identity).GetComponent<PlayerClient>();
            player.IsLocal = false;
        }

        player.name = $"Player {id} (username)";
        player.Id = id;
        player.username = username;

        list.Add(id, player);
    }

    [MessageHandler((ushort)ServerToClientId.playerSpawned)]
    private static void SpawnPlayer(Message message)
    {
        Spawn(message.GetUShort(), message.GetString(), message.GetVector2());
    }

    [MessageHandler((ushort)ServerToClientId.playerMovement)]

    private static void PlayerMovement(Message message)
    {
        if (list.TryGetValue(message.GetUShort(), out PlayerClient player))
            player.Move(message.GetVector2(), message.GetVector2());
    }
}