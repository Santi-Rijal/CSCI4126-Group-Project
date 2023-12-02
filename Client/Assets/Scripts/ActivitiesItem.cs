using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActivitiesItem : MonoBehaviour {

    [SerializeField] private string activityName;
    [SerializeField] private string activityLocation;
    [SerializeField] private string activityTime;
    [SerializeField] private string activityDesc;
    
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI location;
    [SerializeField] private TextMeshProUGUI desc;
    
    private void Awake() {
        name.text = activityName;
        time.text = activityTime;
        location.text = activityLocation;
        desc.text = activityDesc;
    }

    public string GetName() {
        return activityName;
    }

    public string GetTime() {
        return activityTime;
    }
    
    public string GetLocation() {
        return activityLocation;
    }

    public string GetDesc() {
        return activityDesc;
    }
    
    public void SetItem(ActivitiesItem item) {
        activityName = item.GetName();
        activityLocation = item.GetLocation();
        activityTime = item.GetTime();
        activityDesc = item.GetDesc();
        
        name.text = activityName;
        time.text = activityTime;
        location.text = activityLocation;
    }
}
