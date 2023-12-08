using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Riptide;

/// <summary>
/// Manages the calendar display and functionality within the application.
/// </summary>
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
    
    /// <summary>
    /// Initializes the calendar with the current date and updates the display.
    /// </summary>
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
    
    /// <summary>
    /// Updates the calendar display and handles removed bookings.
    /// </summary>
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

    /// <summary>
    /// Loads and displays the deleted bookings from the account.
    /// </summary>
    /// <param name="list">The list of deleted bookings.</param>
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

    /// <summary>
    /// Changes the currently selected date in the calendar.
    /// </summary>
    /// <param name="date">The new date to select.</param>
    public void ChangeDate(Dates date) {
        if (currentlySelected != null) {
            currentlySelected.ReturnToDefault();
        }
        
        currentlySelected = date;
        currentlySelected.Select();
        _selectedDate = date.ToString();
        
        Display(tabs.GetActiveCanvasName());
    }

    /// <summary>
    /// Gets the currently selected date in string format.
    /// </summary>
    /// <returns>The currently selected date as a string.</returns>
    public string GetDate() {
        var date = _currentMonth + " " + _selectedDate;
        return date;
    }

    /// <summary>
    /// Gets the currently selected <see cref="Dates"/> object.
    /// </summary>
    /// <returns>The currently selected <see cref="Dates"/> object.</returns>
    public Dates GetDay() {
        return currentlySelected;
    }

    /// <summary>
    /// Displays the appropriate items based on the selected date and tab.
    /// </summary>
    /// <param name="itemType">The type of item to display ("Courts" or "Activities").</param>
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

    /// <summary>
    /// Removes a specified court item from the current date.
    /// </summary>
    /// <param name="items">The court item to remove.</param>
    public void RemoveCourtItem(CourtItems items) {
        currentlySelected.RemoveCourtItem(items);
    }
    
    /// <summary>
    /// Removes a specified activity item from the current date.
    /// </summary>
    /// <param name="items">The activity item to remove.</param>
    public void RemoveActivityItem(ActivitiesItem items) {
        currentlySelected.RemoveActivityItem(items);
    }
}
