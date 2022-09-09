using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;

public class PlayerServer : MonoBehaviour
{
    public static Dictionary<ushort, PlayerServer> list = new Dictionary<ushort, PlayerServer>();
    public ushort Id { get; private set; }
    public string Username { get; private set; }
    public CharacterControllerServer Movement => movement;

    [SerializeField] private CharacterControllerServer movement;

    Rigidbody2D rigidBody2D;

    private void OnDestroy()
    {
        list.Remove(Id);
    }

    public static void Spawn(ushort id, string username)
    {
        foreach (PlayerServer otherPlayer in list.Values)
            otherPlayer.SendSpawned(id);

        PlayerServer player = Instantiate(GameLogicServer.Singleton.PlayerPrefab, new Vector2(0f, 0f), Quaternion.identity).GetComponent<PlayerServer>();
        player.name = $"Player {id} {(string.IsNullOrEmpty(username) ? "Guest" : username)}";
        player.Id = id;
        player.Username = string.IsNullOrEmpty(username) ? $"Guest {id}" : username;

        player.SendSpawned();
        list.Add(id, player);
    }

    private void SendSpawned()
    {
        NetworkManagerServer.Singleton.Server.SendToAll(AddSpawnData(Message.Create(MessageSendMode.reliable, ServerToClientId.playerSpawned)));
    }

    private void SendSpawned(ushort toClientId)
    {
        NetworkManagerServer.Singleton.Server.Send(AddSpawnData(Message.Create(MessageSendMode.reliable, ServerToClientId.playerSpawned)), toClientId);
    }

    private Message AddSpawnData(Message message)
    {
        message.AddUShort(Id);
        message.AddString(Username);
        message.AddVector2(transform.position);
        return message;
    }

    //public void TakeDamage()
    //{
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "DeathZone")
        {
            //impact02.SetActive(true);
            StartCoroutine(DelayeRespawned());
            SendDied();
        }
    }

    private IEnumerator DelayeRespawned()
    {
        yield return new WaitForSeconds(2);
        KillPlayer();
    }

    private void KillPlayer()
    {
        transform.position = new Vector2(0f, 0f);
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.angularVelocity = 0f;
    }

    [MessageHandler((ushort)ClientToServerId.name)]
    private static void Name(ushort fromClientId, Message message)
    {
        Spawn(fromClientId, message.GetString());
    }

    [MessageHandler((ushort)ClientToServerId.input)]
    private static void Input(ushort fromClientId, Message message)
    {
        Debug.Log("input");
        if (list.TryGetValue(fromClientId, out PlayerServer player))
            player.Movement.SetInput(message.GetBools(4));
    }

    private void SendDied()
    {
        Message message = Message.Create(MessageSendMode.reliable, ServerToClientId.playerDied);
        message.AddUShort(Id);
        message.AddVector3(transform.position);
        NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
}
