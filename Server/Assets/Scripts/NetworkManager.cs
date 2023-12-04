using System;
using TMPro;
using Riptide;
using Riptide.Utils;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class to manage the connection on the server side.
 */
public class NetworkManager : MonoBehaviour {

    public Server Server { get; private set; }

    [SerializeField] private ushort port;   // Port for the server.
    [SerializeField] private ushort maxClientCount; // Num of clients allowed.

    private GameObject _reservedTablesGameObject;

    private void Awake() {
        _reservedTablesGameObject = GameObject.Find("ReservedTable");
    }

    private void Start() {
        // Initialize riptide logs.
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();  // Create a new server.
        Server.Start(port, maxClientCount); // Start the server on port with max client count.

        Server.MessageReceived += ServerOnMessageReceived;  // Subscribe to message received event.
        Server.ClientDisconnected += ServerOnClientDisconnected;    // Subscribe to client disconnect event.
        Server.ClientConnected += ServerOnClientConnected;  // Subscribe to client connect event.
    }

    // A subscription method for client connected event.
    private void ServerOnClientConnected(object sender, ServerConnectedEventArgs e) {
        print("Connected");
    }

    // A subscription method for client disconnected event.
    private void ServerOnClientDisconnected(object sender, ServerDisconnectedEventArgs e) {
        print("Disconnected");
    }

    // A subscription method for message received event.
    private void ServerOnMessageReceived(object sender, MessageReceivedEventArgs e) {
        var type = e.Message.GetString();    
        var time = e.Message.GetString();
        var court = e.Message.GetString();

        if (type.Equals("Reserve")) {
            Reserve(time, court);
        }

        if (type.Equals("Remove")) {
            RemoveReserve(time, court);
        }
    }

    private void Reserve(string time, string court) {
        var timeGameObject = _reservedTablesGameObject.transform.Find(time);

        if (timeGameObject != null) {
            var courtGameObject = timeGameObject.Find(court);

            if (courtGameObject != null) {
                var imageComponent = courtGameObject.GetComponent<Image>();
                imageComponent.color = new Color32(31, 41, 96, 212);

                var textComponent = courtGameObject.GetComponentInChildren<Text>();
                textComponent.text = "RESERVED";
            }
        }
    }

    private void RemoveReserve(string time, string court) {
        var timeGameObject = _reservedTablesGameObject.transform.Find(time);

        if (timeGameObject != null) {
            var courtGameObject = timeGameObject.Find(court);

            if (courtGameObject != null) {
                var imageComponent = courtGameObject.GetComponent<Image>();
                imageComponent.color = new Color32(250, 249, 246, 125);

                var textComponent = courtGameObject.GetComponentInChildren<Text>();
                textComponent.text = "";
            }
        }
    }
    
    private void FixedUpdate() {
        Server.Update();    // Call the server's update method at a fixed update.
    }

    private void OnApplicationQuit() {
        Server.Stop();  // Stop the server on application quit.
    }
}
