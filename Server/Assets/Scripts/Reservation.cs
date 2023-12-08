using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a reservation for a court, handling its availability and reservation status.
/// </summary>
public class Reservation 
{
    public int courtNumber;
    public int timeScale;
    public bool available;

    /// <summary>
    /// Initializes a new reservation with the specified court number and time scale.
    /// </summary>
    /// <param name="num">The court number for the reservation.</param>
    /// <param name="time">The time scale of the reservation.</param>
    public Reservation(int num, int time)
    {
        courtNumber = num;
        timeScale = time;
        available = true;
    }

    /// <summary>
    /// Attempts to reserve the court.
    /// </summary>
    /// <returns>Returns true if the reservation was successful; otherwise, false.</returns>
    public bool Reserve()
    {
        if (available)
        {
            available = false;
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Attempts to cancel the reservation.
    /// </summary>
    /// <returns>Returns true if the cancellation was successful; otherwise, false.</returns>
    public bool Cancel()
    {
        if (!available)
        {
            available = true;
            return true;
        }
        else return false;
    }
}
