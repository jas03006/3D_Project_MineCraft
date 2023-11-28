using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//.net���̺귯��
using System;
//���� ����� �ϱ� ���� ���̺귯��
using System.Net;
using System.Net.Sockets;
using System.IO;//�����͸� �а� ���� ���� ���̺귯��
using System.Threading;//��Ƽ �������� �ϱ� ���� ���̺귯��


public class TCPManager : MonoBehaviour
{
    public InputField IPAdress;
    public InputField Port;

    [SerializeField] private Text Status;
    //�⺻���� �������
    //.net >> �⺻���� ��Ŷ >> ���⼱ stream�̶���
    //�����͸� �д� �κ� >> thread

    StreamReader reader; //������ �д� ��
    StreamWriter writer; //������ ���� ��

    public InputField MessageBox;
    private MessagePool Message;

    private Queue<string> log = new Queue<string>();
    void StatusMS()
    {
        if (log.Count >0)
        {
            Status.text = log.Dequeue();
        }
    }
    #region Server
    public void ServerOpen()
    {
        Message = FindObjectOfType<MessagePool>();
        Thread thread = new Thread(ServerConnect);
        thread.IsBackground = true;
        thread.Start();
    }
    private void ServerConnect()//������ �����ִ� �� = ����� ��
    {
        //���������� ��� >> updateó��(�޼����� ���ö����� ����)
        //�帧�� ����ó���� �ʿ��� >> Try-Catch�� Ȱ��
        try
        {
            TcpListener tcp = new TcpListener(IPAddress.Parse(IPAdress.text), int.Parse(Port.text));
            //tcplistener ��ü ����
            tcp.Start(); //�������� = ��������
            log.Enqueue("Server Open");
            //tcplistener�� ������ �ɶ����� ��ٷȴٰ� client�� �Ҵ�����
            TcpClient client = tcp.AcceptTcpClient();
            log.Enqueue("Client Connected To Server");
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
            writer.AutoFlush = true;

            while (client.Connected)
            {
                string readData = reader.ReadLine();
                Message.Message(readData);
            }

        }
        catch (Exception e)
        {
            log.Enqueue(e.Message);            
        }


    }
    #endregion
    
    #region Client
    public void ClientConnect()
    {
        Message = FindObjectOfType<MessagePool>();
        log.Enqueue("Client connect");
        Thread thread = new Thread(clientConnect);
        thread.IsBackground = true;
        thread.Start();
    }
    private void clientConnect()
    {
        try
        {
            TcpClient client = new TcpClient();
            //������ ip�������̰� Ŭ���̾�Ʈ�� ip�������̴�
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IPAdress.text), int.Parse(Port.text));
            client.Connect(iPEnd);
            log.Enqueue("Client Server Connect Complete");

            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
            writer.AutoFlush = true;

            while (client.Connected)
            {
                string readerData = reader.ReadLine();
                Message.Message(readerData);
            }

        }
        catch (Exception e)
        {
            log.Enqueue(e.Message);
        }
    }
    #endregion
    private bool SandMS(string ms)
    {
        if (writer != null)
        {
            writer.WriteLine(ms);
            return true;
        }
        else
        {
            Debug.Log("Writer is null");
            return false;
        }
    }

    public void SandingMS()
    {
        if (SandMS(SQLManager.Instance.info.User_name + " : " + MessageBox.text))
        {
            Message.Message(SQLManager.Instance.info.User_name + " : " + MessageBox.text);
            MessageBox.text = string.Empty;
        }
    }

    private void Update()
    {
        StatusMS();
    }

}
