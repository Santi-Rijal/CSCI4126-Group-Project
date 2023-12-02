using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemClicks : MonoBehaviour {

    [SerializeField] private GameObject unClicked;
    [SerializeField] private GameObject clicked;

    private Bookings _bookings;
    private Calendar _calendar;

    private void Awake() {
        _bookings = GameObject.Find("Bookings").GetComponent<Bookings>();
        _calendar = GameObject.Find("CalendarCanvas").GetComponent<Calendar>();
    }

    public void ItemClicked() {
        unClicked.gameObject.SetActive(!unClicked.gameObject.activeSelf);
        clicked.gameObject.SetActive(!clicked.gameObject.activeSelf);
    }

    public void CancelClicked() {
        ItemClicked();
    }

    public void CourtConfirmClicked() {
        var item = GetComponent<CourtItems>();

        var booking = new List<object>();
        booking.Add("Court");
        booking.Add(_calendar.GetDate());
        booking.Add(item);
        
        _bookings.AddBooking(booking);
        
        unClicked.gameObject.SetActive(true);
        clicked.gameObject.SetActive(false);
        
        gameObject.SetActive(false);
    }
    
    public void ActivitiesConfirmClicked() {
        var item = GetComponent<ActivitiesItem>();

        var booking = new List<object>();
        booking.Add("Activity");
        booking.Add(_calendar.GetDate());
        booking.Add(item);
        
        _bookings.AddBooking(booking);
        
        unClicked.gameObject.SetActive(true);
        clicked.gameObject.SetActive(false);
        
        gameObject.SetActive(false);
    }
}
