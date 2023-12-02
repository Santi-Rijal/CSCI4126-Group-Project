using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookings : MonoBehaviour {
    
    private List<List<object>> bookings = new List<List<object>>();
   
    public List<List<object>> GetBookings() {
        return bookings;
    }

    public void AddBooking(List<object> booking) {
        bookings.Add(booking);
    }

    public void RemoveBooking(List<object> booking) {
        bookings.Remove(booking);
    }
}
