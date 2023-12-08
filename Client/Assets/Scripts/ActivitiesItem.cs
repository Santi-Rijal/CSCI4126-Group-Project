using TMPro;
using UnityEngine;

/// <summary>
/// Represents an activity item in the UI, including its details and functionality.
/// </summary>
public class ActivitiesItem : MonoBehaviour {

    [SerializeField] private string activityName;
    [SerializeField] private string activityLocation;
    [SerializeField] private string activityTime;
    [SerializeField] private string activityDesc;
    
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI location;
    [SerializeField] private TextMeshProUGUI desc;

    public int id;
    
    /// <summary>
    /// Initializes the activity item's UI elements with the corresponding data.
    /// </summary>
    private void Awake() {
        name.text = activityName;
        time.text = activityTime;
        location.text = activityLocation;
        desc.text = activityDesc;
    }

    /// <summary>
    /// Gets the activity name.
    /// </summary>
    /// <returns>The activity name.</returns>
    public string GetName() {
        return activityName;
    }

    /// <summary>
    /// Gets the activity time.
    /// </summary>
    /// <returns>The activity time.</returns>
    public string GetTime() {
        return activityTime;
    }
    
    /// <summary>
    /// Gets the activity location.
    /// </summary>
    /// <returns>The activity location.</returns>
    public string GetLocation() {
        return activityLocation;
    }

    /// <summary>
    /// Gets the activity description.
    /// </summary>
    /// <returns>The activity description.</returns>
    public string GetDesc() {
        return activityDesc;
    }
    
    /// <summary>
    /// Sets the item's details based on another <see cref="ActivitiesItem"/>.
    /// </summary>
    /// <param name="item">The <see cref="ActivitiesItem"/> to copy details from.</param>
    public void SetItem(ActivitiesItem item) {
        activityName = item.GetName();
        activityLocation = item.GetLocation();
        activityTime = item.GetTime();
        activityDesc = item.GetDesc();
        
        name.text = activityName;
        time.text = activityTime;
        location.text = activityLocation;
        desc.text = activityDesc;
    }

    /// <summary>
    /// Deletes this activity item.
    /// </summary>
    public void Delete() {
        var account = GameObject.Find("AccountCanvas").GetComponent<Account>();
        account.DeleteBooking("activity", this);
    }
}
