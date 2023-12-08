using System.Collections.Generic;
using UnityEngine;
using Riptide;

/// <summary>
/// Handles the interactions with court and activity items in the UI.
/// </summary>
public class ItemClicks : MonoBehaviour {

    [SerializeField] private GameObject unClicked;
    [SerializeField] private GameObject clicked;

    private Bookings _bookings;
    private Calendar _calendar;

    private int _id = 0;
    
    /// <summary>
    /// Initializes references to the Bookings and Calendar components.
    /// </summary>
    private void Awake() {
        var booking = GameObject.Find("Bookings");
        _bookings = booking.GetComponent<Bookings>();
        _calendar = GameObject.Find("CalendarCanvas").GetComponent<Calendar>();
    }

    /// <summary>
    /// Toggles the clicked and unclicked state of the item.
    /// </summary>
    public void ItemClicked() {
        unClicked.gameObject.SetActive(!unClicked.gameObject.activeSelf);
        clicked.gameObject.SetActive(!clicked.gameObject.activeSelf);
    }

    /// <summary>
    /// Handles the click event to cancel the current selection.
    /// </summary>
    public void CancelClicked() {
        ItemClicked();
    }

    /// <summary>
    /// Handles the click event to confirm a court booking.
    /// </summary>
    public void CourtConfirmClicked() {
        var item = GetComponent<CourtItems>();

        var booking = new List<object> {
            "Court",
            _calendar.GetDate(),
            item,
            _id
        };
        item.id = _id;
        
        _bookings.AddBooking(booking);
        _calendar.RemoveCourtItem(item);
        _calendar.ChangeDate(_calendar.GetDay());
        
        _id++;
        
        SendMessage(ClientToServerId.name, "Reserve", item.GetTime(), item.GetName());
    }
    
    /// <summary>
    /// Handles the click event to confirm an activity booking.
    /// </summary>
    public void ActivitiesConfirmClicked() {
        var item = GetComponent<ActivitiesItem>();
        
        var booking = new List<object> {
            "Activity",
            _calendar.GetDate(),
            item,
            _id
        };
        item.id = _id;
        
        _bookings.AddBooking(booking);
        _calendar.RemoveActivityItem(item);
        _calendar.ChangeDate(_calendar.GetDay());
        
        _id++;

        SendMessage(ClientToServerId.name, "Activity", item.GetTime(), item.GetName());
    }
    
    /// <summary>
    /// Sends a message to the server with booking details.
    /// </summary>
    /// <param name="id">The ID for the client to server message.</param>
    /// <param name="type">The type of booking (court or activity).</param>
    /// <param name="time">The time of the booking.</param>
    /// <param name="court">The name of the court or activity.</param>
    private void SendMessage(ClientToServerId id, string type, string time, string court) {
        Message message = Message.Create(MessageSendMode.Reliable, id);
        message.Add(type);
        message.Add(time);
        message.Add(court);
        NetworkManager.Singleton.Client.Send(message);
    }
}
