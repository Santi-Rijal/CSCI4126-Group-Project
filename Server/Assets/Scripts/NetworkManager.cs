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

    private IEnumerable<string> GetTimeSlots(string timeRange) 
    {
        var parts = timeRange.Split('-').Select(t => t.Trim()).ToArray();

        if (parts.Length != 2) {
            Debug.LogError("Invalid time format: " + timeRange);
            yield break;
        }

        // Extract the start and end time parts
        string startHourStr = parts[0];
        string endHourStr = parts[1];

        // Identify the AM/PM part from the end time
        string amPm = endHourStr.Substring(endHourStr.Length - 2).ToLower();
        endHourStr = endHourStr.Substring(0, endHourStr.Length - 2).Trim();

        // Safely parse the hours
        if (!int.TryParse(startHourStr, out int startHour) || !int.TryParse(endHourStr, out int endHour)) {
            Debug.LogError("Invalid time format: " + timeRange);
            yield break;
        }

        // Convert to 24-hour format for calculation
        startHour = ConvertTo24HourFormat(startHour, amPm);
        endHour = ConvertTo24HourFormat(endHour, amPm);

        // Generate time slots
        for (int hour = startHour; hour < endHour; hour++) {
            int nextHour = hour + 1;
            yield return $"{ConvertTo12HourFormat(hour)} - {ConvertTo12HourFormat(nextHour)}";
        }
    }

    private int ConvertTo24HourFormat(int hour, string amPm) {
        if (amPm == "pm" && hour != 12) {
            return hour + 12;
        } else if (amPm == "am" && hour == 12) {
            return 0;
        }
        return hour;
    }

    private string ConvertTo12HourFormat(int hour) {
        int displayHour = hour % 12;
        displayHour = displayHour == 0 ? 12 : displayHour;
        string amPm = hour < 12 || hour == 24 ? "am" : "pm";
        return $"{displayHour}{amPm}";
    }

    /// <summary>
    /// Reserves a court for a given time slot.
    /// </summary>
    /// <param name="timeGameObject">The time slot GameObject.</param>
    /// <param name="court">The court to be reserved.</param>
    private void ReserveCourt(GameObject timeGameObject, string court) {
        var courtGameObject = timeGameObject.transform.Find(court);
        if (courtGameObject != null) {
            var imageComponent = courtGameObject.gameObject.GetComponent<Image>();
            imageComponent.color = new Color32(31, 41, 96, 212);

            var textComponent = courtGameObject.gameObject.GetComponentInChildren<Text>();
            textComponent.text = "RESERVED";
        }
    }

    /// <summary>
    /// Removes reservation from a court for a given time slot.
    /// </summary>
    /// <param name="timeGameObject">The time slot GameObject.</param>
    /// <param name="court">The court to remove the reservation from.</param>
    private void RemoveReservationFromCourt(GameObject timeGameObject, string court) {
        var courtGameObject = timeGameObject.transform.Find(court);
        if (courtGameObject != null) {
            var imageComponent = courtGameObject.gameObject.GetComponent<Image>();
            imageComponent.color = new Color32(250, 249, 246, 125);

            var textComponent = courtGameObject.gameObject.GetComponentInChildren<Text>();
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
