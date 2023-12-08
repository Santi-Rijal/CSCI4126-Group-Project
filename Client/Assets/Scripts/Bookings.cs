using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the bookings and their states within the application.
/// </summary>
public class Bookings : MonoBehaviour {
    
    public List<List<object>> bookings = new List<List<object>>(); // List to store current bookings.
    public List<List<object>> bookingsRemoved = new List<List<object>>(); // List to store removed bookings.

    /// <summary>
    /// Adds a new booking to the list of current bookings.
    /// </summary>
    /// <param name="booking">The booking to be added.</param>
    public void AddBooking(List<object> booking) {
        bookings.Add(booking);
    }

    /// <summary>
    /// Removes a booking from the current bookings and adds it to the list of removed bookings.
    /// </summary>
    /// <param name="booking">The booking to be removed.</param>
    public void RemoveBooking(List<object> booking) {
        bookings.Remove(booking);
        bookingsRemoved.Add(booking);
        print(booking[2]); // Logs the removed booking details (for debugging purposes).
    }

    /// <summary>
    /// Clears the list of removed bookings.
    /// </summary>
    public void Remove() {
        bookingsRemoved = new List<List<object>>();
    }
}
