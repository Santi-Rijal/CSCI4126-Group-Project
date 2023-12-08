using TMPro;
using UnityEngine;

/// <summary>
/// Represents a court item in the UI, including its details and functionality.
/// </summary>
public class CourtItems : MonoBehaviour {

    [SerializeField] private string courtName;
    [SerializeField] private string timeSlot;

    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI time;

    public int id;

    /// <summary>
    /// Initializes the court item's UI elements with the corresponding data.
    /// </summary>
    private void Awake() {
        name.text = courtName;
        time.text = timeSlot;
    }

    /// <summary>
    /// Gets the court name.
    /// </summary>
    /// <returns>The court name.</returns>
    public string GetName() {
        return courtName;
    }

    /// <summary>
    /// Gets the time slot of the court booking.
    /// </summary>
    /// <returns>The time slot.</returns>
    public string GetTime() {
        return timeSlot;
    }

    /// <summary>
    /// Sets the item's details based on another <see cref="CourtItems"/>.
    /// </summary>
    /// <param name="item">The <see cref="CourtItems"/> to copy details from.</param>
    public void SetItem(CourtItems item) {
        courtName = item.GetName();
        timeSlot = item.GetTime();
        
        name.text = courtName;
        time.text = timeSlot;
    }
    
    /// <summary>
    /// Deletes this court item.
    /// </summary>
    public void Delete() {
        var account = GameObject.Find("AccountCanvas").GetComponent<Account>();
        account.DeleteBooking("court", this);
    }
}
