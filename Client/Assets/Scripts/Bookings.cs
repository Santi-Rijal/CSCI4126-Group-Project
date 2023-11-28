using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookings : MonoBehaviour {
    
    private List<List<string>> bookings = new List<List<string>>();
   
    public List<List<string>> GetBookings() {
        return bookings;
    }

    public void AddBooking(List<string> booking) {
        bookings.Add(booking);
    }

    public void RemoveBooking(List<string> booking) {
        bookings.Remove(booking);
    }
}
