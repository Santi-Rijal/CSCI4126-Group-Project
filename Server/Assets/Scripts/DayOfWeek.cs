using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class DayOfWeek : MonoBehaviour
{
    public Text dayText;

    void Start()
    {
        UpdateDayOfWeek();
    }

    void UpdateDayOfWeek()
    {
        dayText.text = DateTime.Now.ToString("dddd", CultureInfo.InvariantCulture).ToUpper();
    }
}
