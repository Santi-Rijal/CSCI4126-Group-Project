using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an event with details such as name, date, location, and capacity.
/// </summary>
public class Events
{
    public string eventName;
    public int eventDate;
    public string eventPlace;
    private int maxCapacity; // Maximum number of participants
    private int participants; // Current number of participants

    /// <summary>
    /// Constructor to initialize an event.
    /// </summary>
    /// <param name="name">Name of the event.</param>
    /// <param name="date">Date of the event.</param>
    /// <param name="place">Location of the event.</param>
    /// <param name="capacity">Maximum capacity of the event.</param>
    public Events(string name, int date, string place, int capacity)
    {
        eventName = name;
        eventDate = date;
        eventPlace = place;
        maxCapacity = capacity;
        participants = 0;
    }

    /// <summary>
    /// Adds a participant to the event if capacity allows.
    /// </summary>
    /// <returns>True if the participant was added successfully, false if the event is at full capacity.</returns>
    public bool AddParticipant()
    {
        if (participants < maxCapacity)
        {
            participants++;
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Removes a participant from the event if there are any.
    /// </summary>
    /// <returns>True if a participant was removed successfully, false if there are no participants to remove.</returns>
    public bool RemoveParticipant()
    {
        if (participants > 0)
        {
            participants--;
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Gets the remaining capacity of the event.
    /// </summary>
    /// <returns>The number of additional participants that can be accommodated.</returns>
    public int GetCapacity()
    {
        return maxCapacity - participants;
    }
}
