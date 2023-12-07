using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
    private GameObject _tennisLesson;

    private void Awake() {
        _reservedTablesGameObject = GameObject.Find("ReservedTable");
        _tennisLesson = GameObject.Find("Event (3)");
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

    private void RemoveReservationForTimeInterval(string time, string court) {
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
    
    private void FixedUpdate() {
        Server.Update();    // Call the server's update method at a fixed update.
    }

    private void OnApplicationQuit() {
        Server.Stop();  // Stop the server on application quit.
    }
}
