using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBar : MonoBehaviour {

    [SerializeField] private Canvas[] canvasArray;

    private Canvas _activeCanvas;

    private void Awake() {
        _activeCanvas = canvasArray[0];
    }
}
