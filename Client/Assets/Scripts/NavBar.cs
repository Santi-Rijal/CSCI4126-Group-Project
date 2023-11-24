using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBar : MonoBehaviour {

    [SerializeField] private Canvas[] canvasArray;

    private Canvas _activeCanvas;

    private void Awake() {
        _activeCanvas = canvasArray[0];
        if (!_activeCanvas.gameObject.activeSelf) _activeCanvas.gameObject.SetActive(true);
    }

    public void HomeClicked() {
        //_activeCanvas.gameObject.SetActive(false);
        _activeCanvas = canvasArray[0];
        _activeCanvas.gameObject.SetActive(true);
    }
    
    public void BookClicked() {
        //_activeCanvas.gameObject.SetActive(false);
        _activeCanvas = canvasArray[1];
        _activeCanvas.gameObject.SetActive(true);
    }
    
    public void AccountClicked() {
        //_activeCanvas.gameObject.SetActive(false);
        _activeCanvas = canvasArray[2];
        _activeCanvas.gameObject.SetActive(true);
    }
}
