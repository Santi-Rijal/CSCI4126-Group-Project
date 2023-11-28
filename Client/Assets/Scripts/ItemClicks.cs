using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemClicks : MonoBehaviour {

    [SerializeField] private GameObject unClicked;
    [SerializeField] private GameObject clicked;
    [SerializeField] private Bookings bookings;

    public void ItemClicked() {
        unClicked.gameObject.SetActive(!unClicked.gameObject.activeSelf);
        clicked.gameObject.SetActive(!clicked.gameObject.activeSelf);
    }

    public void CancelClicked() {
        ItemClicked();
    }

    public void CourtConfirmClicked() {
        var container = clicked.transform.Find("Details");
        var courtNumber = container.Find("Court").GetComponent<TextMeshProUGUI>().text;
        var time = container.Find("Time").GetComponent<TextMeshProUGUI>().text;

        var booking = new List<string>();
        booking.Add("Court");
        booking.Add(courtNumber);
        booking.Add(time);
        
        bookings.AddBooking(booking);
        
        unClicked.gameObject.SetActive(false);
        clicked.gameObject.SetActive(false);
    }
    
    public void ActivitiesConfirmClicked() {
        var container = clicked.transform.Find("Details");
        var activityTitle = container.Find("GameObject").Find("Title").GetComponent<TextMeshProUGUI>().text;
        var activityTime = container.Find("GameObject").Find("Time").GetComponent<TextMeshProUGUI>().text;
        var location = container.Find("Location").GetComponent<TextMeshProUGUI>().text;
        var desc = container.Find("Desc").GetComponent<TextMeshProUGUI>().text;

        var booking = new List<string>();
        booking.Add("Activity");
        booking.Add(activityTitle);
        booking.Add(activityTime);
        booking.Add(location);
        booking.Add(desc);
        
        bookings.AddBooking(booking);
        
        unClicked.gameObject.SetActive(false);
        clicked.gameObject.SetActive(false);
    }
}
