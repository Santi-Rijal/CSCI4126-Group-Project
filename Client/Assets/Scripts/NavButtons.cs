using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a navigation button in the UI and manages its behavior and appearance.
/// </summary>
public class NavButtons : MonoBehaviour {

    [SerializeField] private GameObject canvas;
    
    /// <summary>
    /// Called when the navigation button is clicked. 
    /// Instructs the NavBar to change the selected button and activate the associated canvas.
    /// </summary>
    public void OnClick() {
        var nav = GetComponentInParent<NavBar>();
        nav.ChangeButton(this);
        nav.MakeActive(canvas);
    }
    
    /// <summary>
    /// Resets the visual appearance of the navigation button to its default state.
    /// </summary>
    public void ReturnToDefault() {
        var image = gameObject.GetComponent<Image>();
        image.color = new Color32(217, 239, 229, 255);

        var text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.color = Color.black;
    }

    /// <summary>
    /// Changes the visual appearance of the navigation button to indicate it is selected.
    /// </summary>
    public void Select() {
        var image = gameObject.GetComponent<Image>();
        image.color = new Color32(13, 25, 35, 255);

        var text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.color = Color.white;
    }
}
