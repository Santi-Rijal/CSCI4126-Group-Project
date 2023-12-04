using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Riptide;

public class Account : MonoBehaviour {

    private Bookings _bookings;
    
    [SerializeField] private Transform courtItem;
    [SerializeField] private Transform activityItem;
    [SerializeField] private Transform monthItem;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject bookingObject;

    private int _currentY = 0;
    private int _prevSize;
    
    private void Awake() {
        var booking = GameObject.Find("Bookings");
        _bookings = booking.GetComponent<Bookings>();
        _prevSize = _bookings.bookings.Count;
        DisplayBookings();
    }

    private void Update() {

        if (_prevSize != _bookings.bookings.Count) {
            DisplayBookings();
            _prevSize = _bookings.bookings.Count;
        }
    }

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
                    
                    itemContainer.gameObject.name = item.GetName();
                    itemContainer.GetComponent<CourtItems>().SetItem(item);
                    
                    PositionItem(itemContainer.GetComponent<RectTransform>(), month.childCount - 1, false);
                }

                if (booking[0].Equals("Activity")) {
                    var item = (ActivitiesItem) booking[2];
                    
                    var itemContainer = Instantiate(activityItem, month);

                    itemContainer.gameObject.name = item.GetName();
                    itemContainer.GetComponent<ActivitiesItem>().SetItem(item);
                    
                    PositionItem(itemContainer.GetComponent<RectTransform>(), month.childCount - 1, false);
                }

                var containerRact = month.GetComponent<RectTransform>();
                containerRact.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 70 * month.childCount);
            }
            else {
                
                if (booking[0].Equals("Court")) {
                    
                    var item = (CourtItems) booking[2];
                    var exists = dateExitsInUI.Find(item.GetName());

                    if (exists == null) {
                        var itemContainer = Instantiate(courtItem, dateExitsInUI);
                        itemContainer.GetComponent<CourtItems>().SetItem(item);
                    
                        PositionItem(itemContainer.GetComponent<RectTransform>(), dateExitsInUI.childCount - 1, false);
                    }
                }

                if (booking[0].Equals("Activity")) {
                    var item = (ActivitiesItem) booking[2];
                    var exists = dateExitsInUI.Find(item.GetName());

                    if (exists == null) {
                        var itemContainer = Instantiate(activityItem, dateExitsInUI);
                        itemContainer.GetComponent<ActivitiesItem>().SetItem(item);
                    
                        PositionItem(itemContainer.GetComponent<RectTransform>(), dateExitsInUI.childCount - 1, false);
                    }
                }
                
                var containerRact = dateExitsInUI.GetComponent<RectTransform>();
                containerRact.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 70 * dateExitsInUI.childCount);
            }
        }
    }

    private void PositionItem(RectTransform rect, int siblings, bool date) {
        var currentPosition = rect.anchoredPosition;

        if (date) {
            var currentHeight = 0f;

            for (var i = 0; i < siblings; i++) {
                currentHeight -= container.GetChild(i).GetComponent<RectTransform>().rect.height;
            }

            currentHeight -= 10f;
            
            currentPosition.y = currentHeight;
        }
        else {
            currentPosition.y = siblings * -70;
        }
        
        rect.anchoredPosition = currentPosition;
    }

    public void DeleteBooking(string type, object item) {
        Reload();

        if (type.Equals("activity")) {
            var aItem = (ActivitiesItem) item;

            var list = GetList(aItem.id);
            list[2] = aItem;
            bookingObject.GetComponent<Bookings>().RemoveBooking(list);
        }
        else {
            var cItem = (CourtItems) item;

            var list = GetList(cItem.id);
            list[2] = cItem;
            bookingObject.GetComponent<Bookings>().RemoveBooking(list);
            
            Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.name);
            message.Add("Remove");
            message.Add(cItem.GetTime());
            message.Add(cItem.GetName());
            NetworkManager.Singleton.Client.Send(message);
        }
    }

    private List<object> GetList(int id) {
        foreach (var item in _bookings.bookings) {
            if (item[3].Equals(id)) return item;
        }

        return null;
    }

    private void Reload() {
        foreach (Transform item in container) {
            Destroy(item.gameObject);
        }
    }
}
