using System;
using Riptide;
using Riptide.Utils;
using UnityEngine;

/// <summary>
/// Enumeration for client to server message IDs.
/// </summary>
public enum ClientToServerId : ushort {
    name = 1,
}

/// <summary>
/// Manages the network connection on the client side.
/// </summary>
public class NetworkManager : MonoBehaviour {

    private static NetworkManager _singleton;   // Singleton instance of this class.

    /// <summary>
    /// Gets or privately sets the singleton instance of <see cref="NetworkManager"/>.
    /// Creates a new singleton if it doesn't exist, else destroys the duplicate.
    /// </summary>
    public static NetworkManager Singleton {
        get => _singleton;

        private set {
            if (_singleton == null) {
                _singleton = value;
            }
            else if (_singleton != value) {
                Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying duplicate.");
                Destroy(value);
            }
        }
    }

    public Client Client { get; private set; }  // Client get and set methods.

    [SerializeField] private ushort port = 7777;   // Port to run on.

    /// <summary>
    /// Initializes the singleton instance.
    /// </summary>
    private void Awake() {
        _singleton = this;
    }

    /// <summary>
    /// Starts the network manager, initializes logs, and connects the client.
    /// </summary>
    private void Start() {
        // Initialize Riptide logs.
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Client = new Client();  // Create a new client.
        Connect();

        // Subscribe to connection events.
        Client.ConnectionFailed += FailedToConnect; // Subscribe to connection failed event.
        Client.Disconnected += DidDisconnect;   // Subscribe to disconnected event.
    }

    /// <summary>
    /// Updates the client at a fixed interval.
    /// </summary>
    private void FixedUpdate() {
        Client.Update();    // Call the client's update method at a fixed interval.
    }

    /// <summary>
    /// Disconnects the client when the application quits.
    /// </summary>
    private void OnApplicationQuit() {
        Client.Disconnect();
    }

    /// <summary>
    /// Connects the client to the server using the specified IP and port.
    /// </summary>
    public void Connect() {
        Client.Connect($"{LoadScene.IP}:{port}"); // Call the connect method of the client with the IP and port.
    }

    /// <summary>
    /// Handles the event when connection to the server fails.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void FailedToConnect(object sender, EventArgs e) {
        // Handle failed to connect event.
        Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.name);
        message.Add("Failed to connect");
        Client.Send(message);
    }
    
    /// <summary>
    /// Handles the event when the client disconnects from the server.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void DidDisconnect(object sender, EventArgs e) {
        // Disconnection event.
    }
}
