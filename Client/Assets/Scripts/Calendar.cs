using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour {

    [SerializeField] private Dates currentlySelected;

    private void Awake() {
        var image = currentlySelected.gameObject.GetComponent<Image>();
        image.color = new Color32(38, 106, 74, 255);

        var text = currentlySelected.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.color = Color.white;
    }

    public void ChangeDate(Dates date) {
        if (currentlySelected != null) {
            currentlySelected.ReturnToDefault();
        }
        
        print("change date");
        currentlySelected = date;
        currentlySelected.Select();
    }
}
