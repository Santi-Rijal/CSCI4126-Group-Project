using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets up a fake dataset for testing purposes, including events and reservations.
/// </summary>
public class FakeDataSetup : MonoBehaviour
{
    /// <summary>
    /// A static list of events for global access.
    /// </summary>
    public static List<Events> events = new();

    [SerializeField]
    private int courtNumber = 4;
    [SerializeField]
    private int timeScale = 8;

    /// <summary>
    /// A static list of reservations for global access.
    /// </summary>
    public static List<Reservation> reservations = new();

    /// <summary>
    /// Initializes the dataset with predefined events and reservations on start.
    /// </summary>
    void Start()
    {
        // Adds predefined events to the list.
        events.Add(new Events("Family Doubles", 10, "Court 1", 16));
        events.Add(new Events("Tennis Clinic", 25, "Court 2", 20));
        events.Add(new Events("Summer Camp", 28, "Court 1", 10));

        // Creates reservations for each court and time slot.
        for (int court = 0; court < courtNumber; court++)
        {
            for (int time = 0; time < timeScale; time++)
                reservations.Add(new Reservation(court, time));
        }
    }
}
