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

    [SerializeField] private Light sumLight;

    private DateTime currentTime;

    private void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromSeconds(startHour);
    }
    private void Update()
    {
        UpdateTimeOfDay();
    }
    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime + timeMultiplier);
        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }
}
