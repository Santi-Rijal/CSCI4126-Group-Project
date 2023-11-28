using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account : MonoBehaviour {

    private List<List<string>> bookings;
    [SerializeField] private Bookings items;

    [SerializeField] private Transform courtItem;
    [SerializeField] private Transform activityItem;
    [SerializeField] private Transform container;
    
    private void Start() {
        bookings = items.GetBookings();
        DisplayBookings();
    }

    private void DisplayBookings() {
        foreach (var booking in bookings) {
            if (booking[0].Equals("Court")) {
                Instantiate(courtItem, container);
            }

            if (booking[0].Equals("Activity")) {
                Instantiate(activityItem, container);
            }
        }
    }
}
