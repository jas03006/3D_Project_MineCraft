using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float timeMultiplier;

    [SerializeField] private float startHour;

    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private Light sunLight;

    [SerializeField] private float sunriseHour;

    [SerializeField] private float sunsetHour;

    [SerializeField] private Color dayAmbientLightt;

    [SerializeField] private Color nightAmbientLightt;

    [SerializeField] AnimationCurve lightChangeCurve;

    [SerializeField] private float maxSunLightIntensity;

    [SerializeField] private Light moonLight;

    [SerializeField] private float maxMoonLightIntensity;


    private DateTime currentTime;

    private TimeSpan sunriseTime; 
    private TimeSpan sunsetTime;

    private void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);

    }
    private void Update()
    {
        UpdateTimeOfDay();
        Rotatesun();
        UpdateLightSetting();
    }
    private void UpdateTimeOfDay()    // 시계 시스템
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }
    private void Rotatesun()   //자전주기 
    {
        float sunRotation;
        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunrisetosunset = CalculateTimeDifffence(sunriseTime, sunsetTime);
            TimeSpan timeSinceSurise = CalculateTimeDifffence(sunriseTime, currentTime.TimeOfDay);
    
            double percentage = timeSinceSurise.TotalMinutes / sunrisetosunset.TotalMinutes;
    
            sunRotation = Mathf.Lerp(0, 180,(float)percentage);
        }
        else
        {
            TimeSpan sunset = CalculateTimeDifffence(sunsetTime, sunriseTime);
            TimeSpan timeSinceSset = CalculateTimeDifffence(sunsetTime, currentTime.TimeOfDay);
    
            double percentage = timeSinceSset.TotalMinutes / sunset.TotalMinutes;
    
            sunRotation = Mathf.Lerp(180, 360, (float)percentage);
        }
        sunLight.transform.rotation = Quaternion.AngleAxis(sunRotation, Vector3.right);
    }
    private TimeSpan CalculateTimeDifffence(TimeSpan formTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - formTime;

        if (difference.TotalSeconds <0)
        {
            difference += TimeSpan.FromHours(24);
        }
        return difference;
    }
    private void UpdateLightSetting()    // 달빛 조절
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity,0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLightt, dayAmbientLightt, lightChangeCurve.Evaluate(dotProduct));
    }
}
