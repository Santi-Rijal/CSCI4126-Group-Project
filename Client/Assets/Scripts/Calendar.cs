using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Riptide;


public class Calendar : MonoBehaviour {

    [SerializeField] private Dates currentlySelected;
    [SerializeField] private TextMeshProUGUI monthText;
    [SerializeField] private Transform courtItemsContainer;
    [SerializeField] private Transform activityItemsContainer;
    [SerializeField] private Tabs tabs;
    [SerializeField] private Bookings bookingsObject;

    private string _currentMonth;
    private string _selectedDate;
    private string _day;
    
    private void Awake() {
        
        var currentDate = DateTime.Now;
        var month = currentDate.ToString("MMMM");
        var day = currentDate.ToString("dd");

        currentlySelected = GameObject.Find(day).GetComponent<Dates>();

        monthText.text = month;
        _currentMonth = month;
        
        currentlySelected.Select();
        _selectedDate = day;
        
        Display(tabs.GetActiveCanvasName());
    }
    
    private void Update() {
        Display(tabs.GetActiveCanvasName());

        var bookingObject = GameObject.Find("Bookings").GetComponent<Bookings>();
        var booking = bookingsObject.bookingsRemoved;

        print("count " + booking.Count);
        if (booking.Count != 0) {
            LoadDeletedFromAccount(booking);
            bookingObject.Remove();
        }
    }

    private void LoadDeletedFromAccount(List<List<object>> list) {

        foreach (var booking in list) {
            var fullDate = booking[1].ToString();
            
            var dateFormat = DateTime.ParseExact(fullDate, "MMMM dd", System.Globalization.CultureInfo.InvariantCulture);
            var dayPart = dateFormat.ToString("dd");
            var day = GameObject.Find(dayPart);
            
            if (day != null) {
                var comp = day.GetComponent<Dates>();

                if (booking[0].ToString().Equals("Court")) {
                    comp.AddCourtItem((CourtItems) booking[2]);
                }

                if (booking[0].ToString().Equals("Activity")) {
                    comp.AddActivityItem((ActivitiesItem) booking[2]);
                    
                }
            }
        }

        bookingsObject.bookingsRemoved = new List<List<object>>();
    }

    public void ChangeDate(Dates date) {
        if (currentlySelected != null) {
            currentlySelected.ReturnToDefault();
        }
        
        currentlySelected = date;
        currentlySelected.Select();
        _selectedDate = date.ToString();
        
        Display(tabs.GetActiveCanvasName());
    }

    public string GetDate() {
        var date = _currentMonth + " " + _selectedDate;
        return date;
    }

    public Dates GetDay() {
        return currentlySelected;
    }

    private void Display(string itemType) {
        var courtItems = currentlySelected.CourtItems;
        var activityItems = currentlySelected.ActivitiesItems;

        var numUICourtItems = courtItemsContainer.childCount;
        var numUIActivityItems = activityItemsContainer.childCount;

        if (itemType.Equals("Courts")) {

            if (courtItems.Count != numUICourtItems) {
                foreach (Transform item in courtItemsContainer) {
                    Destroy(item.gameObject);
                }
                
                foreach (var item in courtItems) {
                    Instantiate(item, courtItemsContainer);
                }
            }
        }
        else {
            if (activityItems.Count != numUIActivityItems) {
                foreach (Transform item in activityItemsContainer) {
                    Destroy(item.gameObject);
                }
                
                foreach (var item in activityItems) {
                    Instantiate(item, activityItemsContainer);
                }
            }
        }
    }

    public void RemoveCourtItem(CourtItems items) {
        currentlySelected.RemoveCourtItem(items);
    }
    
    public void RemoveActivityItem(ActivitiesItem items) {
        currentlySelected.RemoveActivityItem(items);
    }
}
