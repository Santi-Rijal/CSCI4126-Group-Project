using TMPro;
using UnityEngine;

public class CourtItems : MonoBehaviour {

    [SerializeField] private string courtName;
    [SerializeField] private string timeSlot;

    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI time;

    public int id;

    private void Awake() {
        name.text = courtName;
        time.text = timeSlot;
    }

    public string GetName() {
        return courtName;
    }

    public string GetTime() {
        return timeSlot;
    }

    public void SetItem(CourtItems item) {
        print(item);
        courtName = item.GetName();
        timeSlot = item.GetTime();
        
        name.text = courtName;
        time.text = timeSlot;
    }
    
    public void Delete() {
        var account = GameObject.Find("AccountCanvas").GetComponent<Account>();
        account.DeleteBooking("court", this);
        
    }
}
