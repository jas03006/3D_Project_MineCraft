using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginControl : MonoBehaviour
{
    public InputField IDinput;
    public InputField PWinput;

    [SerializeField] private Text Log;

    public void LogBt()
    {
        if (IDinput.text.Equals(string.Empty) || PWinput.text.Equals(string.Empty))
        {
            Log.text = "���̵�� ��й�ȣ�� �Է��ϼ���";
            return;
        }

        if (SQLManager.Instance.Login(IDinput.text,PWinput.text))
        {
            UserInfo info = SQLManager.Instance.info;
            Debug.Log(info.User_name + " / " + info.User_Password);
            gameObject.SetActive(false);


        }
        else
        {
            Log.text = "���̵�� ��й�ȣ�� Ȯ�����ּ���";
        }





    }

}
