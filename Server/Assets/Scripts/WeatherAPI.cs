using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class WeatherAPIScript : MonoBehaviour
{
    public string city = "Halifax, Canada";
    private string apiKey = "bd08346cc82a086fd4a9861b555d1b89";
    private string apiUrl = "http://api.weatherstack.com/current";
    public UnityEngine.UI.Text temperatureText;
    public UnityEngine.UI.Text windSpeedText; // UI Text for wind speed

    public GameObject hailObject, rainyObject, snowyObject, sunnyCloudsObject, thunderObject;
    private Dictionary<string, GameObject> weatherAnimations;

    public float updateInterval = 1800f; // Time in seconds for each weather update (e.g., 1800 seconds = 30 minutes)

    private float timer;

    void Start()
    {
        InitializeWeatherAnimations();
        StartCoroutine(GetWeatherData());
        timer = updateInterval;
    }

    void Update()
    {
        // Update timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            StartCoroutine(GetWeatherData());
            timer = updateInterval;
        }
    }

    void InitializeWeatherAnimations()
    {
        weatherAnimations = new Dictionary<string, GameObject>
        {
            { "Hail", hailObject },
            { "Rain", rainyObject },
            { "Snow", snowyObject },
            { "Sunny", sunnyCloudsObject },
            { "Thunderstorm", thunderObject }
        };

        DeactivateAllAnimations();
    }

    IEnumerator GetWeatherData()
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

            // Update UI elements
            temperatureText.text = weatherData.current.temperature + "°C";
            windSpeedText.text = weatherData.current.wind_speed + " KM/H";

            // Trigger the correct animation
            TriggerWeatherAnimation(weatherData.current.weather_descriptions[0]);
        }
    }

    void TriggerWeatherAnimation(string weatherDescription)
    {
        Debug.Log("Weather Description: " + weatherDescription);

        // Deactivate all animations first
        DeactivateAllAnimations();

        // Determine which animation to play
        if (weatherDescription.Contains("Hail") || weatherDescription.Contains("Sleet"))
            weatherAnimations["Hail"].SetActive(true);
        else if (weatherDescription.Contains("Rain") || weatherDescription.Contains("Drizzle") || weatherDescription.Contains("Light rain") || weatherDescription.Contains("Heavy rain") || weatherDescription.Contains("Fog") || weatherDescription.Contains("Mist"))
            weatherAnimations["Rain"].SetActive(true);
        else if (weatherDescription.Contains("Snow") || weatherDescription.Contains("Light snow") || weatherDescription.Contains("Heavy snow"))
            weatherAnimations["Snow"].SetActive(true);
        else if (weatherDescription.Contains("Sunny") || weatherDescription.Contains("Clear") || weatherDescription.Contains("Partly cloudy") || weatherDescription.Contains("Cloudy") || weatherDescription.Contains("Overcast"))
            weatherAnimations["Sunny"].SetActive(true);
        else if (weatherDescription.Contains("Thunderstorm"))
            weatherAnimations["Thunderstorm"].SetActive(true);
    }

    void DeactivateAllAnimations()
    {
        foreach (var anim in weatherAnimations.Values)
        {
            anim.SetActive(false);
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
        public string[] weather_descriptions; // Array of weather descriptions
        public int wind_speed; // Wind speed in km/h
    }
}