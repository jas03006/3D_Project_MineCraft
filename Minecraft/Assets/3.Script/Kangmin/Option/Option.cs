using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    private Transform[] children;

    private bool isOptionOpen = false; // 버튼으로 bool값 바꾸기 위해
    

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();

        for (int i = 1; i < children.Length; i++) // 0번째 인덱스 = Canvas
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
                for (int i = 1; i < children.Length; i++) // 0번째 인덱스 = gameObject.name("E")
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
    



    public void closeOptionButton()
    {
        if (isOptionOpen == true)
        {
            for (int i = 1; i < children.Length; i++)
            {
                children[i].gameObject.SetActive(false);
            }
            isOptionOpen = false;
            Cursor.visible = false;
        }
    }
    public void changeOptionOpen(bool check) // esc 두번 누름을 방지하기위해 button에 부가적으로 추가함
    {
        isOptionOpen = check;
    }
}
