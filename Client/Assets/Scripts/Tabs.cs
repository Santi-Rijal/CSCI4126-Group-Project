using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the tabs for switching between court and activity views.
/// </summary>
public class Tabs : MonoBehaviour {

    [SerializeField] private GameObject court;
    [SerializeField] private GameObject courtCanvas;
    
    [SerializeField] private GameObject activity;
    [SerializeField] private GameObject activityCanvas;

    /// <summary>
    /// Handles the click event on the court tab, updating UI elements to reflect the current selection.
    /// </summary>
    public void CourtClicked() {
        UpdateTabVisuals(activity, new Color32(208, 230, 221, 255), Color.black);
        UpdateTabVisuals(court, new Color32(38, 106, 74, 255), Color.white);
        
        courtCanvas.SetActive(true);
        activityCanvas.SetActive(false);
    }
    
    /// <summary>
    /// Handles the click event on the activity tab, updating UI elements to reflect the current selection.
    /// </summary>
    public void ActivityClicked() {
        UpdateTabVisuals(court, new Color32(208, 230, 221, 255), Color.black);
        UpdateTabVisuals(activity, new Color32(38, 106, 74, 255), Color.white);

        activityCanvas.SetActive(true);
        courtCanvas.SetActive(false);
    }

    /// <summary>
    /// Returns the name of the currently active canvas.
    /// </summary>
    /// <returns>The name of the active canvas ("Courts" or "Activities").</returns>
    public string GetActiveCanvasName() {
        return courtCanvas.activeSelf ? "Courts" : "Activities";
    }

    /// <summary>
    /// Updates the visual appearance of a tab.
    /// </summary>
    /// <param name="tab">The tab GameObject to update.</param>
    /// <param name="backgroundColor">The background color for the tab.</param>
    /// <param name="textColor">The text color for the tab.</param>
    private void UpdateTabVisuals(GameObject tab, Color32 backgroundColor, Color textColor) {
        var image = tab.GetComponent<Image>();
        image.color = backgroundColor;

        var text = tab.GetComponentInChildren<TextMeshProUGUI>();
        text.color = textColor;
    }
}
