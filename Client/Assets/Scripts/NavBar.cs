using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NavBar : MonoBehaviour {

    [SerializeField] private NavButtons currentlySelected;

    [SerializeField] private GameObject activeCanvas;

    private void Awake() {
        currentlySelected.Select();
        activeCanvas.SetActive(true);
    }
    
    public void ChangeButton(NavButtons button) {
        if (currentlySelected != null) {
            currentlySelected.ReturnToDefault();
        }
        
        currentlySelected = button;
        currentlySelected.Select();
    }

    public void MakeActive(GameObject canvas) {
        activeCanvas.SetActive(false);
        activeCanvas = canvas;
        activeCanvas.SetActive(true);
    }
}
