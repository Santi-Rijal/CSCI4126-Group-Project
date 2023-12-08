using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Riptide;

/// <summary>
/// Manages account information and displays bookings in the UI.
/// </summary>
public class Account : MonoBehaviour {

    private Bookings _bookings;
    
    [SerializeField] private Transform courtItem;
    [SerializeField] private Transform activityItem;
    [SerializeField] private Transform monthItem;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject bookingObject;

    private int _currentY = 0;
    private int _prevSize;
    
    /// <summary>
    /// Initializes bookings and displays them in the UI.
    /// </summary>
    private void Awake() {
        var booking = GameObject.Find("Bookings");
        _bookings = booking.GetComponent<Bookings>();
        _prevSize = _bookings.bookings.Count;
        DisplayBookings();
    }

    /// <summary>
    /// Updates the booking display if there are changes in bookings.
    /// </summary>
    private void Update() {
        if (_prevSize != _bookings.bookings.Count) {
            DisplayBookings();
            _prevSize = _bookings.bookings.Count;
        }
    }

    /// <summary>
    /// Displays the bookings in the UI.
    /// </summary>
    private void DisplayBookings() {
        _currentY = 0;
        
        foreach (var booking in _bookings.bookings) {
            var dateExitsInUI = container.Find(booking[1].ToString());
            
            if (dateExitsInUI == null) {
                var month = Instantiate(monthItem, container);
                
                month.gameObject.name = booking[1].ToString();
                month.GetComponentInChildren<TextMeshProUGUI>().text = booking[1].ToString();
                
                PositionItem(month.GetComponent<RectTransform>(), container.childCount - 1, true);
                
                if (booking[0].Equals("Court")) {
                    var item = (CourtItems) booking[2];
                    
                    var itemContainer = Instantiate(courtItem, month);
                    
                    itemContainer.gameObject.name = item.GetName() + " " + item.GetTime();
                    itemContainer.GetComponent<CourtItems>().SetItem(item);
                    
                    PositionItem(itemContainer.GetComponent<RectTransform>(), month.childCount - 1, false);
                }

                if (booking[0].Equals("Activity")) {
                    var item = (ActivitiesItem) booking[2];
                    
                    var itemContainer = Instantiate(activityItem, month);

                    itemContainer.gameObject.name = item.GetName() + " " + item.GetTime();
                    itemContainer.GetComponent<ActivitiesItem>().SetItem(item);
                    
                    PositionItem(itemContainer.GetComponent<RectTransform>(), month.childCount - 1, false);
                }

                var containerRact = month.GetComponent<RectTransform>();
                containerRact.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200 * month.childCount);
            }
            else {
                
                if (booking[0].Equals("Court")) {
                    
                    var item = (CourtItems) booking[2];
                    var exists = dateExitsInUI.Find(item.GetName() + " " + item.GetTime());

                    if (exists == null) {
                        var itemContainer = Instantiate(courtItem, dateExitsInUI);
                        
                        itemContainer.gameObject.name = item.GetName() + " " + item.GetTime();
                        itemContainer.GetComponent<CourtItems>().SetItem(item);
                    
                        PositionItem(itemContainer.GetComponent<RectTransform>(), dateExitsInUI.childCount - 1, false);
                    }
                }

                if (booking[0].Equals("Activity")) {
                    var item = (ActivitiesItem) booking[2];
                    var exists = dateExitsInUI.Find(item.GetName() + " " + item.GetTime());

                    if (exists == null) {
                        var itemContainer = Instantiate(activityItem, dateExitsInUI);
                        
                        itemContainer.gameObject.name = item.GetName() + " " + item.GetTime();
                        itemContainer.GetComponent<ActivitiesItem>().SetItem(item);
                    
                        PositionItem(itemContainer.GetComponent<RectTransform>(), dateExitsInUI.childCount - 1, false);
                    }
                }
                
                var containerRact = dateExitsInUI.GetComponent<RectTransform>();
                containerRact.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200 * dateExitsInUI.childCount);
            }
        }
    }

    /// <summary>
    /// Positions a UI item within the container.
    /// </summary>
    /// <param name="rect">The RectTransform of the item to position.</param>
    /// <param name="siblings">The number of siblings the item has within its parent container.</param>
    /// <param name="date">Whether the item represents a date or not.</param>
    private void PositionItem(RectTransform rect, int siblings, bool date) {
        var currentPosition = rect.anchoredPosition;

        if (date) {
            var currentHeight = 0f;

            for (var i = 0; i < siblings; i++) {
                currentHeight -= container.GetChild(i).GetComponent<RectTransform>().rect.height;
            }

            if (currentHeight != 0) {
                currentHeight -= 100f;
            }
            
            currentPosition.y = currentHeight;
        }
        else {
            currentPosition.y = siblings * -210;
        }
        
        rect.anchoredPosition = currentPosition;
    }

    /// <summary>
    /// Deletes a booking and updates the UI and server accordingly.
    /// </summary>
    /// <param name="type">The type of booking to delete ("activity" or "court").</param>
    /// <param name="item">The booking item to delete.</param>
    public void DeleteBooking(string type, object item) {
        Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.name);
        message.Add("Remove");

        if (type.Equals("activity")) {
            var aItem = (ActivitiesItem) item;

            var list = GetList(aItem.id);
            list[2] = aItem;
            bookingObject.GetComponent<Bookings>().RemoveBooking(list);
            
            var gameObject = container.Find(list[1].ToString()).Find(aItem.GetName() + " " + aItem.GetTime());

            if (gameObject != null) {
                Destroy(gameObject.gameObject);
            }

            message.Add(aItem.GetTime());
            message.Add(aItem.GetName());
        }
        else {
            var cItem = (CourtItems) item;

            var list = GetList(cItem.id);
            list[2] = cItem;
            bookingObject.GetComponent<Bookings>().RemoveBooking(list);
            
            var gameObject = container.Find(list[1].ToString()).Find(cItem.GetName() + " " + cItem.GetTime());

            if (gameObject != null) {
                Destroy(gameObject.gameObject);
            }
            
            message.Add(cItem.GetTime());
            message.Add(cItem.GetName());
        }
        NetworkManager.Singleton.Client.Send(message);
    }

    /// <summary>
    /// Retrieves a list of booking details based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the booking to find.</param>
    /// <returns>A list containing booking details, or null if not found.</returns>
    private List<object> GetList(int id) {
        foreach (var item in _bookings.bookings) {
            if (item[3].Equals(id)) return item;
        }

        return null;
    }

    /// <summary>
    /// Clears and reloads the booking display.
    /// </summary>
    private void Reload() {
        foreach (Transform item in container) {
            Destroy(item.gameObject);
        }
    }
}
