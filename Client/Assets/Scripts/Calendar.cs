using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour {

    [SerializeField] private Dates currentlySelected;

    private void Awake() {
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
