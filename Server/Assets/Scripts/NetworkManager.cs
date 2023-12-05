using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Riptide;
using Riptide.Utils;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages server-side connections and reservation functionality.
/// </summary>
public class NetworkManager : MonoBehaviour {

    public Server Server { get; private set; }

    [SerializeField] private ushort port;   // Port for the server.
    [SerializeField] private ushort maxClientCount; // Number of clients allowed.

    private GameObject _reservedTablesGameObject;

    private void Awake() {
        _reservedTablesGameObject = GameObject.Find("ReservedTable");
    }

    private void Start() {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        Server = new Server();
        Server.Start(port, maxClientCount);

        Server.MessageReceived += ServerOnMessageReceived;
        Server.ClientDisconnected += ServerOnClientDisconnected;
        Server.ClientConnected += ServerOnClientConnected;
    }

    private void ServerOnClientConnected(object sender, ServerConnectedEventArgs e) {
        print("Connected");
    }

    private void ServerOnClientDisconnected(object sender, ServerDisconnectedEventArgs e) {
        print("Disconnected");
    }

    /// <summary>
    /// Processes incoming messages from clients and executes reservation commands.
    /// </summary>
    private void ServerOnMessageReceived(object sender, MessageReceivedEventArgs e) {
        var type = e.Message.GetString();    
        var time = e.Message.GetString();
        var court = e.Message.GetString();

        if (type.Equals("Reserve")) {
            Reserve(time, court);
        } else if (type.Equals("Remove")) {
            RemoveReserve(time, court);
        }
    }

    /// <summary>
    /// Reserves time slots for a specified court.
    /// </summary>
    /// <param name="time">The time range for the reservation.</param>
    /// <param name="court">The court to be reserved.</param>
    private void Reserve(string time, string court) {
        foreach (var timeSlot in GetTimeSlots(time)) {
            var timeGameObject = _reservedTablesGameObject.transform.Find(timeSlot);
            if (timeGameObject != null) {
                ReserveCourt(timeGameObject, court);
            }
        }
    }

    /// <summary>
    /// Removes reservation from time slots for a specified court.
    /// </summary>
    /// <param name="time">The time range for which the reservation will be removed.</param>
    /// <param name="court">The court to remove the reservation from.</param>
    private void RemoveReserve(string time, string court) {
        foreach (var timeSlot in GetTimeSlots(time)) {
            var timeGameObject = _reservedTablesGameObject.transform.Find(timeSlot);
            if (timeGameObject != null) {
                RemoveReservationFromCourt(timeGameObject, court);
            }
        }
    }

    /// <summary>
    /// Parses a time range string into individual time slots.
    /// </summary>
    /// <param name="timeRange">The time range string to parse.</param>
    /// <returns>An enumerable of time slot strings.</returns>
    private IEnumerable<string> GetTimeSlots(string timeRange) {
        var parts = timeRange.Split('-').Select(t => t.Trim()).ToArray();
        var startTime = int.Parse(parts[0].Split(' ')[0]);
        var endTime = int.Parse(parts[1].Split(' ')[0]);
        var amPm = parts[1].Substring(parts[1].Length - 2);

        for (int hour = startTime; hour < endTime; hour++) {
            yield return $"{hour} - {hour + 1}{amPm}";
        }
    }

    /// <summary>
    /// Reserves a court for a given time slot.
    /// </summary>
    /// <param name="timeGameObject">The time slot GameObject.</param>
    /// <param name="court">The court to be reserved.</param>
    private void ReserveCourt(GameObject timeGameObject, string court) {
        var courtGameObject = timeGameObject.Find(court);
        if (courtGameObject != null) {
            var imageComponent = courtGameObject.GetComponent<Image>();
            imageComponent.color = new Color32(31, 41, 96, 212);

            var textComponent = courtGameObject.GetComponentInChildren<Text>();
            textComponent.text = "RESERVED";
        }
    }

    /// <summary>
    /// Removes reservation from a court for a given time slot.
    /// </summary>
    /// <param name="timeGameObject">The time slot GameObject.</param>
    /// <param name="court">The court to remove the reservation from.</param>
    private void RemoveReservationFromCourt(GameObject timeGameObject, string court) {
        var courtGameObject = timeGameObject.Find(court);
        if (courtGameObject != null) {
            var imageComponent = courtGameObject.GetComponent<Image>();
            imageComponent.color = new Color32(250, 249, 246, 125);

            var textComponent = courtGameObject.GetComponentInChildren<Text>();
            textComponent.text = "";
        }
    }

    private void FixedUpdate() {
        Server.Update();
    }

    private void OnApplicationQuit() {
        Server.Stop();
    }
}
