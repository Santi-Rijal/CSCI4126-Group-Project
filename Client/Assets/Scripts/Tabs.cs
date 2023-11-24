using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tabs : MonoBehaviour {

    [SerializeField] private GameObject court;
    [SerializeField] private GameObject courtCanvas;
    
    [SerializeField] private GameObject activity;
    [SerializeField] private GameObject activityCanvas;

    public void CourtClicked() {
        var image = activity.GetComponent<Image>();
        image.color = new Color32(208, 230, 221, 255);

        var text = activity.GetComponentInChildren<TextMeshProUGUI>();
        text.color = Color.black;
        
        var image1 = court.GetComponent<Image>();
        image1.color = new Color32(38, 106, 74, 255);

        var text1 = court.GetComponentInChildren<TextMeshProUGUI>();
        text1.color = Color.white;
        
        courtCanvas.SetActive(true);
        activityCanvas.SetActive(false);
    }
    
    public void ActivityClicked() {
        var image = court.GetComponent<Image>();
        image.color = new Color32(208, 230, 221, 255);
        
        var text = court.GetComponentInChildren<TextMeshProUGUI>();
        text.color = Color.black;
        
        var image1 = activity.GetComponent<Image>();
        image1.color = new Color32(38, 106, 74, 255);

        var text1 = activity.GetComponentInChildren<TextMeshProUGUI>();
        text1.color = Color.white;

        activityCanvas.SetActive(true);
        courtCanvas.SetActive(false);
    }
}
