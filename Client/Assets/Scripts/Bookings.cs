
using System.Collections.Generic;
using UnityEngine;

public class Bookings : MonoBehaviour {
    
    public List<List<object>> bookings = new List<List<object>>();
    public List<List<object>> bookingsRemoved = new List<List<object>>();

    public void AddBooking(List<object> booking) {
        bookings.Add(booking);
    }

    public void RemoveBooking(List<object> booking) {
        bookings.Remove(booking);
        bookingsRemoved.Add(booking);
        print(booking[2]);
    }

    public void Remove() {
        bookingsRemoved = new List<List<object>>();
    }
}
