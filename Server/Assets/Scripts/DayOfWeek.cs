using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

/// <summary>
/// Updates and displays the current day of the week.
/// </summary>
public class DayOfWeek : MonoBehaviour
{
    public Text dayText;

    /// <summary>
    /// Called on start to update the day of the week text.
    /// </summary>
    void Start()
    {
        UpdateDayOfWeek();
    }

    /// <summary>
    /// Updates the text component with the current day of the week.
    /// </summary>
    void UpdateDayOfWeek()
    {
        dayText.text = DateTime.Now.ToString("dddd", CultureInfo.InvariantCulture).ToUpper();
    }
}
