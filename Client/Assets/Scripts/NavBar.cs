using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the navigation bar, handling the selection and activation of navigation buttons.
/// </summary>
public class NavBar : MonoBehaviour {

    [SerializeField] private NavButtons currentlySelected;

    [SerializeField] private GameObject activeCanvas;

    /// <summary>
    /// Sets the initially selected navigation button and activates the associated canvas on awake.
    /// </summary>
    private void Awake() {
        currentlySelected.Select();
        activeCanvas.SetActive(true);
    }
    
    /// <summary>
    /// Changes the currently selected navigation button.
    /// </summary>
    /// <param name="button">The navigation button to be selected.</param>
    public void ChangeButton(NavButtons button) {
        if (currentlySelected != null) {
            currentlySelected.ReturnToDefault();
        }
        
        currentlySelected = button;
        currentlySelected.Select();
    }

    /// <summary>
    /// Activates the specified canvas and deactivates the currently active canvas.
    /// </summary>
    /// <param name="canvas">The canvas to be made active.</param>
    public void MakeActive(GameObject canvas) {
        activeCanvas.SetActive(false);
        activeCanvas = canvas;
        activeCanvas.SetActive(true);
    }
}
