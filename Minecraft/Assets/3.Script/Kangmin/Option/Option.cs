using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    private Transform[] children;

    private bool isOptionOpen = false;

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();

        for (int i = 1; i < children.Length; i++) // 0¹øÂ° ÀÎµ¦½º = Canvas
        {
            children[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        OptionInteraction();
    }

    private void OptionInteraction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOptionOpen == false)
            {
                for (int i = 1; i < children.Length; i++) // 0¹øÂ° ÀÎµ¦½º = gameObject.name("E")
                {
                    children[i].gameObject.SetActive(true);
                }
                isOptionOpen = true;
                Cursor.visible = true;
            }
            else if (isOptionOpen == true)
            {
                for (int i = 1; i < children.Length; i++)
                {
                    children[i].gameObject.SetActive(false);
                }
                isOptionOpen = false;
                Cursor.visible = false;
            }
        }
    }
}
