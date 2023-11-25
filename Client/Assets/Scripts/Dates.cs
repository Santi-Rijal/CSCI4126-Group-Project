using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dates : MonoBehaviour {
   [SerializeField] private CourtItems[] courtItems;
   [SerializeField] private ActivitiesItem[] activitiesItems;
   
   public CourtItems[] CourtItems {
      get => courtItems;
      set => courtItems = value;
   }
   
   public ActivitiesItem[] ActivitiesItems {
      get => activitiesItems;
      set => activitiesItems = value;
   }

   public void OnClick() {
      var calender = GetComponentInParent<Calendar>();
      calender.ChangeDate(this);
   }
   
   public void ReturnToDefault() {
      var image = gameObject.GetComponent<Image>();
      image.color = new Color32(255, 255, 255, 255);

      var text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
      text.color = Color.black;
   }

   public void Select() {
      var image = gameObject.GetComponent<Image>();
      image.color = new Color32(38, 106, 74, 255);

      var text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
      text.color = Color.white;
   }
}
