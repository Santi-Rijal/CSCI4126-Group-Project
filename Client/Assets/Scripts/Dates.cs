using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a date in the calendar and manages the associated court and activity items.
/// </summary>
public class Dates : MonoBehaviour {
   [SerializeField] private List<CourtItems> courtItems;
   [SerializeField] private List<ActivitiesItem> activitiesItems;
   
   /// <summary>
   /// Gets or sets the list of court items for this date.
   /// </summary>
   public List<CourtItems> CourtItems {
      get => courtItems;
      set => courtItems = value;
   }
   
   /// <summary>
   /// Gets or sets the list of activity items for this date.
   /// </summary>
   public List<ActivitiesItem> ActivitiesItems {
      get => activitiesItems;
      set => activitiesItems = value;
   }

   /// <summary>
   /// Called when the date is clicked. Informs the calendar to change to this date.
   /// </summary>
   public void OnClick() {
      var calendar = GetComponentInParent<Calendar>();
      calendar.ChangeDate(this);
   }
   
   /// <summary>
   /// Resets the visual appearance of the date to its default state.
   /// </summary>
   public void ReturnToDefault() {
      var image = gameObject.GetComponent<Image>();
      image.color = new Color32(255, 255, 255, 255);

      var text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
      text.color = Color.black;
   }

   /// <summary>
   /// Changes the visual appearance of the date to indicate it is selected.
   /// </summary>
   public void Select() {
      var image = gameObject.GetComponent<Image>();
      image.color = new Color32(38, 106, 74, 255);

      var text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
      text.color = Color.white;
   }

   /// <summary>
   /// Returns the name of the date object.
   /// </summary>
   /// <returns>The name of the GameObject.</returns>
   public override string ToString() {
      return gameObject.name;
   }

   /// <summary>
   /// Adds a court item to this date.
   /// </summary>
   /// <param name="item">The court item to add.</param>
   public void AddCourtItem(CourtItems item) {
      courtItems.Add(item);
   }
   
   /// <summary>
   /// Adds an activity item to this date.
   /// </summary>
   /// <param name="item">The activity item to add.</param>
   public void AddActivityItem(ActivitiesItem item) {
      activitiesItems.Add(item);
   }

   /// <summary>
   /// Removes a specified court item from this date.
   /// </summary>
   /// <param name="item">The court item to remove.</param>
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
   
   /// <summary>
   /// Removes a specified activity item from this date.
   /// </summary>
   /// <param name="item">The activity item to remove.</param>
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
