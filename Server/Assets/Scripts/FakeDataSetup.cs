using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDataSetup : MonoBehaviour
{
    public List<Events> events = new();

    [SerializeField]
    int daysInMonth = 30;
    [SerializeField]
    int courtNumber = 4;
    [SerializeField]
    int timeScale = 8;

    public List<Reservation> reservations = new();

    void Start()
    {
        events.Add(new Events("Family Doubles", 20, "Court 1", 16));
        events.Add(new Events("Tennis Clinic", 25, "Court 1", 20));

        for (int day = 0; day < daysInMonth; day++)
        {
            for (int court = 0; court < courtNumber; court++)
            {
                for (int time = 0; time < timeScale; time++)
                    reservations.Add(new Reservation(day, court, time));
            }
        }
    }

}
