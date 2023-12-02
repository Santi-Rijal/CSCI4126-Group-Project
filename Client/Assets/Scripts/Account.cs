using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Account : MonoBehaviour {

    private List<List<object>> _bookings;
    
    [SerializeField] private Transform courtItem;
    [SerializeField] private Transform activityItem;
    [SerializeField] private Transform monthItem;
    [SerializeField] private Transform container;

    private int _currentY = 0;
    
    private void Awake() {
        var bookings = GameObject.Find("Bookings").GetComponent<Bookings>();
        _bookings = bookings.GetBookings();
        DisplayBookings();
    }

    private void DisplayBookings() {
        foreach (Transform item in container) {
            Destroy(item.gameObject);
        }
        
        foreach (var booking in _bookings) {
            var dateExitsInUI = container.Find(booking[1].ToString());
            
            if (dateExitsInUI == null) {
                var month = Instantiate(monthItem, container);
                month.gameObject.name = booking[1].ToString();
                month.GetComponentInChildren<TextMeshProUGUI>().text = booking[1].ToString();
                PositionItem(month.GetComponent<RectTransform>());
                
                if (booking[0].Equals("Court")) {
                    var item = Instantiate(courtItem, month);
                    item.GetComponent<CourtItems>().SetItem((CourtItems) booking[2]);
                    
                    PositionItem(item.GetComponent<RectTransform>());
                }

                if (booking[0].Equals("Activity")) {
                    var item = Instantiate(activityItem, month);
                    item.GetComponent<ActivitiesItem>().SetItem((ActivitiesItem) booking[2]);
                    
                    PositionItem(item.GetComponent<RectTransform>());
                }

                var containerRact = month.GetComponent<RectTransform>();
                containerRact.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 70 * month.childCount);
            }
            else {
                if (booking[0].Equals("Court")) {
                    var item = Instantiate(courtItem, dateExitsInUI);
                    item.GetComponent<CourtItems>().SetItem((CourtItems) booking[2]);
                    
                    PositionItem(item.GetComponent<RectTransform>());
                }

                if (booking[0].Equals("Activity")) {
                    var item = Instantiate(activityItem, dateExitsInUI);
                    item.GetComponent<ActivitiesItem>().SetItem((ActivitiesItem) booking[2]);
                    
                    PositionItem(item.GetComponent<RectTransform>());
                }
            }
        }
    }

    private void PositionItem(RectTransform rect) {
        var currentPosition = rect.anchoredPosition;
        currentPosition.y = _currentY;
        rect.anchoredPosition = currentPosition;
        _currentY -= 70;
    }
}
