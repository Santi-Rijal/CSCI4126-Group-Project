using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CourtItems : MonoBehaviour {

    [SerializeField] private string courtName;
    [SerializeField] private string timeSlot;

    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI time;

    private void Awake() {
        name.text = courtName;
        time.text = timeSlot;
    }

    public string GetName() {
        return courtName;
    }

    public string GetTime() {
        return timeSlot;
    }

    public void SetItem(CourtItems item) {
        courtName = item.GetName();
        timeSlot = item.GetTime();
        
        name.text = courtName;
        time.text = timeSlot;
    }
}
