using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    string eventName;
    int eventDate;
    string eventPlace;
    int maxCapacity; // set up once instantiated
    int participants;

    public Events(string name, int date, string place, int capacity)
    {
        eventName = name;
        eventDate = date;
        eventPlace = place;
        maxCapacity = capacity;
        participants = 0;
    }

    // returns true if valid
    public bool AddParticipant()
    {
        if (participants < maxCapacity)
        {
            participants++;
            return true;
        }
        else return false;
    }

    // returns true if valid
    public bool RemoveParticipant()
    {
        if (participants > 0)
        {
            participants--;
            return true;
        }
        else return false;
    }

    // displays remaining positions
    public int GetCapacity()
    {
        return maxCapacity - participants;
    }
}
