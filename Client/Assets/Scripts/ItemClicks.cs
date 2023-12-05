
using System.Collections.Generic;
using UnityEngine;
using Riptide;

public class ItemClicks : MonoBehaviour {

    [SerializeField] private GameObject unClicked;
    [SerializeField] private GameObject clicked;

    private Bookings _bookings;
    private Calendar _calendar;

    private int _id = 0;
    
    private void Awake() {
        var booking = GameObject.Find("Bookings");
        _bookings = booking.GetComponent<Bookings>();
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
        booking.Add(_id);
        item.id = _id;
        
        _bookings.AddBooking(booking);
        
        _calendar.RemoveCourtItem(item);
        _calendar.ChangeDate(_calendar.GetDay());
        
        _id++;
        
        SendMessage(ClientToServerId.name, "Reserve", item.GetTime(), item.GetName());
    }
    
    public void ActivitiesConfirmClicked() {
        
        var item = GetComponent<ActivitiesItem>();
        
        var booking = new List<object>();
        booking.Add("Activity");
        booking.Add(_calendar.GetDate());
        booking.Add(item);
        booking.Add(_id);
        item.id = _id;
        
        _bookings.AddBooking(booking);
        
        _calendar.RemoveActivityItem(item);
        _calendar.ChangeDate(_calendar.GetDay());
        
        _id++;

        SendMessage(ClientToServerId.name, "Activity", item.GetTime(), item.GetName());
    }
    
    private void SendMessage(ClientToServerId id, string type, string time, string court) {
        Message message = Message.Create(MessageSendMode.Reliable, id);
        message.Add(type);
        message.Add(time);
        message.Add(court);
        NetworkManager.Singleton.Client.Send(message);
    }
}
