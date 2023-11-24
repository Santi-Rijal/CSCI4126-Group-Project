using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBar : MonoBehaviour {

    [SerializeField] private GameObject[] canvasArray;

    private GameObject _activeCanvas;

    private void Awake() {
        _activeCanvas = canvasArray[1];
        if (!_activeCanvas.activeSelf) _activeCanvas.SetActive(true);
    }

    public void HomeClicked() {
        //_activeCanvas.gameObject.SetActive(false);
        _activeCanvas = canvasArray[0];
        _activeCanvas.SetActive(true);
    }
    
    public void BookClicked() {
        _activeCanvas.SetActive(false);
        _activeCanvas = canvasArray[1];
        print(_activeCanvas.name);
        _activeCanvas.SetActive(true);
        print(_activeCanvas.activeSelf);
    }
    
    public void AccountClicked() {
        //_activeCanvas.gameObject.SetActive(false);
        _activeCanvas = canvasArray[2];
        _activeCanvas.SetActive(true);
    }
}
