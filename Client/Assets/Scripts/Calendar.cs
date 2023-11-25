using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour {

    [SerializeField] private Dates currentlySelected;
    [SerializeField] private TextMeshProUGUI monthText;

    private void Awake() {
        var currentDate = DateTime.Now;
        var month = currentDate.ToString("MMMM");
        var day = currentDate.ToString("dd");

        currentlySelected = GameObject.Find(day).GetComponent<Dates>();

        monthText.text = month;
        
        currentlySelected.Select();
    }

    public void ChangeDate(Dates date) {
        if (currentlySelected != null) {
            currentlySelected.ReturnToDefault();
        }
        
        currentlySelected = date;
        currentlySelected.Select();
    }
}
