using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePool : MonoBehaviour
{
    [SerializeField] private Text[] MessageBox;

    public Action<string> Message;

    string currentMS = string.Empty;
    string pastMS;

    private void Start()
    {
        MessageBox = transform.GetComponentsInChildren<Text>();
        Message = AddMS;
        pastMS = currentMS;
    }

    private void Update()
    {
        if (pastMS.Equals(currentMS))   return;
        ReadMS(currentMS);
        pastMS = currentMS;


    }

    public void AddMS(string m)
    {
        currentMS = m;
    }

    public void ReadMS(string ms)
    {
        bool isInput = false;
        for (int i = 0; i < MessageBox.Length; i++)
        {
            if (MessageBox[i].text.Equals(""))
            {
                MessageBox[i].text = ms;
                isInput = true;
                break;
            }
        }
        if (!isInput)
        {
            for (int i = MessageBox.Length - 1; i > 0 ; i--)
            {
                MessageBox[i].text = MessageBox[i - 1].text;
            }
            MessageBox[0].text = ms;
        }
    }


}
