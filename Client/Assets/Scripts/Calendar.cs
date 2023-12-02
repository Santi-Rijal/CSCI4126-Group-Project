using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour {

    [SerializeField] private Dates currentlySelected;
    [SerializeField] private TextMeshProUGUI monthText;
    [SerializeField] private Transform courtItemsContainer;
    [SerializeField] private Transform activityItemsContainer;
    [SerializeField] private Tabs tabs;

    private string _currentMonth;
    private string _selectedDate;

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

    private void Display(string itemType) {
        var courtItems = currentlySelected.CourtItems;
        var activityItems = currentlySelected.ActivitiesItems;

        var numUICourtItems = courtItemsContainer.childCount;
        var numUIActivityItems = activityItemsContainer.childCount;

        if (itemType.Equals("Courts")) {

            if (courtItems.Length != numUICourtItems) {
                foreach (Transform item in courtItemsContainer) {
                    Destroy(item.gameObject);
                }
                
                foreach (var item in courtItems) {
                    Instantiate(item, courtItemsContainer);
                }
            }
        }
        else {
            if (activityItems.Length != numUIActivityItems) {
                foreach (Transform item in activityItemsContainer) {
                    Destroy(item.gameObject);
                }
                
                foreach (var item in activityItems) {
                    Instantiate(item, activityItemsContainer);
                }
            }
        }
    }
}
