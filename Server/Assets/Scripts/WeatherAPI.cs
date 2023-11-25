using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeatherAPIScript : MonoBehaviour
{
    public string city = "Halifax, Canada";
    private string apiKey = "bd08346cc82a086fd4a9861b555d1b89";
    private string apiUrl = "http://api.weatherstack.com/current";
    public UnityEngine.UI.Text temperatureText;

    void Start()
    {
        StartCoroutine(GetTemperature());
    }

    IEnumerator GetTemperature()
    {
        string fullUrl = $"{apiUrl}?access_key={apiKey}&query={city}";
        UnityWebRequest request = UnityWebRequest.Get(fullUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Process the result
            string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
            WeatherData weatherData = JsonUtility.FromJson<WeatherData>(jsonResult);

            // Set the temperature text
            temperatureText.text = weatherData.current.temperature + "°C";
        }
    }

    [System.Serializable]
    public class WeatherData
    {
        public Current current;
    }

    [System.Serializable]
    public class Current
    {
        public int temperature;
    }
}
