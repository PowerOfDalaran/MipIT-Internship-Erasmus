using RiptideNetworking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerClient : MonoBehaviour
{
    [SerializeField] private Transform camTransform;

    private bool[] inputs;

    private void Start()
    {
        inputs = new bool[4];
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            inputs[0] = true;
            Debug.Log("no te w dziala");
        }

        if (Input.GetKey(KeyCode.A))
            inputs[1] = true;

        if (Input.GetKey(KeyCode.D))
            inputs[2] = true;

        if (Input.GetKey(KeyCode.Space))
            inputs[3] = true;

    }

    private void FixedUpdate()
    {
        SendInput();

        for (int i = 0; i < inputs.Length; i++)
            inputs[i] = false;
    }

    #region Messages
    private void SendInput()
    {
        Debug.Log("te krystiana gowno tez dzial");
        Message message = Message.Create(MessageSendMode.unreliable, ClientToServerId.input);
        message.AddBools(inputs, false);
        message.AddVector2(camTransform.forward);
        NetworkManagerClient.Singleton.Client.Send(message);
    }
    #endregion
}