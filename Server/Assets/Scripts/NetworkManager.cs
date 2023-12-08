using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Riptide;
using Riptide.Utils;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the network connection and operations on the server side.
/// </summary>
public class NetworkManager : MonoBehaviour 
{
    public Server Server { get; private set; }

    [SerializeField] private ushort port;   // Port for the server.
    [SerializeField] private ushort maxClientCount; // Maximum number of clients allowed.

    private GameObject _reservedTablesGameObject;
    private GameObject _tennisLesson;

    /// <summary>
    /// Initializes server-side components and finds required game objects.
    /// </summary>
    private void Awake() 
    {
        _reservedTablesGameObject = GameObject.Find("ReservedTable");
        _tennisLesson = GameObject.Find("Event (3)");
    }

    /// <summary>
    /// Starts the server and subscribes to necessary events.
    /// </summary>
    private void Start() 
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        Server = new Server();
        Server.Start(port, maxClientCount);
        Server.MessageReceived += ServerOnMessageReceived;
        Server.ClientDisconnected += ServerOnClientDisconnected;
        Server.ClientConnected += ServerOnClientConnected;
    }

    // A subscription method for client connected event.
    /// <summary>
    /// Handles the event when a client connects to the server.
    /// </summary>
    private void ServerOnClientConnected(object sender, ServerConnectedEventArgs e)
    {
        Debug.Log("Client connected");
    }

    /// <summary>
    /// Handles the event when a client disconnects from the server.
    /// </summary>
    private void ServerOnClientDisconnected(object sender, ServerDisconnectedEventArgs e)
    {
        Debug.Log("Client disconnected");
    }

    /// <summary>
    /// Processes messages received from clients.
    /// </summary>
    private void ServerOnMessageReceived(object sender, MessageReceivedEventArgs e) 
    {
        var type = e.Message.GetString();    
        var time = e.Message.GetString();
        var court = e.Message.GetString();

        if (type.Equals("Reserve")) 
        {
            HandleReserve(time, court);
        }
        else if (type.Equals("Remove")) 
        {
            if (time.Equals("9 - 1pm"))
            {
                DecrementRegisteredCount(_tennisLesson, time, court);
            }
            else {
                RemoveReservationForTimeInterval(time, court);
            }
        }
        else if (type.Equals("Activity")) 
        {
            if (court == "Tennis Lesson")
            {
                HandleReserve(time, "Court 2");
            }
            IncrementRegisteredCount(_tennisLesson);
        }
    }

    /// <summary>
    /// Increments the registered participant count for an event.
    /// </summary>
    private void IncrementRegisteredCount(GameObject parentGameObject)
    {
        Transform registeredTransform = parentGameObject.transform.Find("Registered");
        if (registeredTransform != null)
        {
            Text textComponent = registeredTransform.GetComponent<Text>();
            if (textComponent != null)
            {
                string currentText = textComponent.text;

                // Assuming the format is always "number/total Registered"
                string[] parts = currentText.Split(' ')[0].Split('/');
                int currentCount = int.Parse(parts[0]);
                int totalCount = int.Parse(parts[1]);

                currentCount++; // Increment the count

                // Update the text
                textComponent.text = currentCount.ToString() + "/" + totalCount.ToString() + " Registered";
            }
            else
            {
                Debug.LogError("Text component not found on Registered object!");
            }
        }
        else
        {
            Debug.LogError("Registered child not found!");
        }
    }

    /// <summary>
    /// Decrements the registered participant count for an event.
    /// </summary>
    private void DecrementRegisteredCount(GameObject parentGameObject, string time, string court)
    {
        Transform registeredTransform = parentGameObject.transform.Find("Registered");
        if (registeredTransform != null)
        {
            Text textComponent = registeredTransform.GetComponent<Text>();
            if (textComponent != null)
            {
                string currentText = textComponent.text;

                // Assuming the format is always "number/total Registered"
                string[] parts = currentText.Split(' ')[0].Split('/');
                int currentCount = int.Parse(parts[0]);
                int totalCount = int.Parse(parts[1]);

                currentCount--; // Decrement the count

                if (currentCount <= 0)
                {
                    currentCount = 0;
                    foreach (string interval in GetTimeIntervals(time, court)) {
                        Debug.Log(interval);
                        if(interval == "11 - 12pm")
                        {
                            RemoveReservationForTimeInterval(interval, "Court 3");
                        }
                        else
                        {
                            RemoveReservationForTimeInterval(interval, "Court 2");
                        }
                    }
                }

                // Update the text
                textComponent.text = currentCount.ToString() + "/" + totalCount.ToString() + " Registered";
            }
            else
            {
                Debug.LogError("Text component not found on Registered object!");
            }
        }
        else
        {
            Debug.LogError("Registered child not found!");
        }
    }

    /// <summary>
    /// Handles reservation actions based on the received message.
    /// </summary>
    private void HandleReserve(string time, string court)
    {
        int[] times = new int[] { 9, 10, 11, 12, 1, 2, 3, 4, 5 };

        string[] splitTime = time.Split('-');

        string firstTime = splitTime[0].Trim();
        string secondTime = splitTime[1].Trim();

        firstTime = Regex.Match(firstTime, @"\d+").Value;
        secondTime = Regex.Match(secondTime, @"\d+").Value;

        string amPm = Regex.Match(splitTime[1], "[AaPp][Mm]").Value;

        int firstTimeInt = int.Parse(firstTime);
        int secondTimeInt = int.Parse(secondTime);

        Debug.Log("First Time: " + firstTimeInt);
        Debug.Log("Second Time: " + secondTimeInt + amPm);

        int first = Array.IndexOf(times, firstTimeInt);
        int second = Array.IndexOf(times, secondTimeInt);

        if(second - first == 1)
        {
            Reserve(time, court);
        }
        else
        {
            for (int i = first; i < second; i++)
            {
                Debug.Log(court);
                string amOrPm = (i < 2) ? "am" : "pm";
                string timeInterval = times[i].ToString() + " - " + times[i + 1].ToString() + amOrPm;
                
                if (timeInterval == "11 - 12pm")
                {
                    Reserve(timeInterval, "Court 3");
                }
                else 
                {
                    Reserve(timeInterval, court);
                }
            }
        }
    }

    /// <summary>
    /// Handles reservation actions based on the received message.
    /// </summary>
    private void Reserve(string time, string court) 
    {
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

    /// <summary>
    /// Removes a reservation for a specific time interval and court.
    /// </summary>
    private void RemoveReservationForTimeInterval(string time, string court) 
    {
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

    /// <summary>
    /// Generates a list of time intervals based on the provided time and court.
    /// </summary>
    private IEnumerable<string> GetTimeIntervals(string time, string court)
    {
        List<string> intervals = new List<string>();
        
        int[] times = new int[] { 9, 10, 11, 12, 1, 2, 3, 4, 5 };

        string[] splitTime = time.Split('-');

        string firstTime = splitTime[0].Trim();
        string secondTime = splitTime[1].Trim();

        firstTime = Regex.Match(firstTime, @"\d+").Value;
        secondTime = Regex.Match(secondTime, @"\d+").Value;

        string amPm = Regex.Match(splitTime[1], "[AaPp][Mm]").Value;

        int firstTimeInt = int.Parse(firstTime);
        int secondTimeInt = int.Parse(secondTime);

        Debug.Log("First Time: " + firstTimeInt);
        Debug.Log("Second Time: " + secondTimeInt + amPm);

        int first = Array.IndexOf(times, firstTimeInt);
        int second = Array.IndexOf(times, secondTimeInt);

        for (int i = first; i < second; i++)
        {
            Debug.Log(court);
            string amOrPm = (i < 2) ? "am" : "pm";
            string timeInterval = times[i].ToString() + " - " + times[i + 1].ToString() + amOrPm;
            intervals.Add(timeInterval);
        }

        return intervals;
    }
    
    /// <summary>
    /// Updates the server at a fixed interval.
    /// </summary>
    private void FixedUpdate() 
    {
        Server.Update();
    }

    /// <summary>
    /// Stops the server when the application quits.
    /// </summary>
    private void OnApplicationQuit() 
    {
        Server.Stop();
    }
}
