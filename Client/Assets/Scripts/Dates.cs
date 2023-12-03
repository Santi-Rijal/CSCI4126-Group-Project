using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dates : MonoBehaviour {
   [SerializeField] private List<CourtItems> courtItems;
   [SerializeField] private List<ActivitiesItem> activitiesItems;
   
   public List<CourtItems> CourtItems {
      get => courtItems;
      set => courtItems = value;
   }
   
   public List<ActivitiesItem> ActivitiesItems {
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

   public override string ToString() {
      return gameObject.name;
   }

   public void AddCourtItem(CourtItems item) {
      courtItems.Add(item);
   }
   
   public void AddActivityItem(ActivitiesItem item) {
      activitiesItems.Add(item);
   }

   public void RemoveCourtItem(CourtItems item) {
      
      for (var i = 0; i < courtItems.Count; i++) {
         var arrItem = courtItems[i];

         if (arrItem.GetName().Equals(item.GetName())) {
            if (arrItem.GetTime().Equals(item.GetTime())) {
               courtItems.RemoveAt(i);
            }
         }
      }
   }
   
   public void RemoveActivityItem(ActivitiesItem item) {

      for (var i = 0; i < activitiesItems.Count; i++) {
         var arrItem = activitiesItems[i];

         if (arrItem.GetName().Equals(item.GetName())) {
            if (arrItem.GetLocation().Equals(item.GetLocation())) {
               if (arrItem.GetTime().Equals(item.GetTime())) {
                  if (arrItem.GetDesc().Equals(item.GetDesc())) {
                     activitiesItems.RemoveAt(i);
                  }
               }
            }
         }
      }
      
   }
}
