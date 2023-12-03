
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NavButtons : MonoBehaviour {

    [SerializeField] private GameObject canvas;
    
    public void OnClick() {
        var nav = GetComponentInParent<NavBar>();
        nav.ChangeButton(this);
        nav.MakeActive(canvas);
    }
    
    public void ReturnToDefault() {
        var image = gameObject.GetComponent<Image>();
        image.color = new Color32(217, 239, 229, 255);

        var text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.color = Color.black;
    }

    public void Select() {
        var image = gameObject.GetComponent<Image>();
        image.color = new Color32(13, 25, 35, 255);

        var text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.color = Color.white;
    }
}
