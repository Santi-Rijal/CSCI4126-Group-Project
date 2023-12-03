using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDataSetup : MonoBehaviour
{
    public static List<Events> events = new();

    [SerializeField]
    int courtNumber = 4;
    [SerializeField]
    int timeScale = 8;

    public static List<Reservation> reservations = new();

    void Start()
    {
        events.Add(new Events("Family Doubles", 10, "Court 1", 16));
        events.Add(new Events("Tennis Clinic", 25, "Court 2", 20));
        events.Add(new Events("Summer Camp", 28, "Court 3", 10));
        for (int court = 0; court < courtNumber; court++)
        {
            for (int time = 0; time < timeScale; time++)
                reservations.Add(new Reservation(court, time));
        }
    }

}
