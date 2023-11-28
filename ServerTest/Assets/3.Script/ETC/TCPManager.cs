using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//.net라이브러리
using System;
//소켓 통신을 하기 위한 라이브러리
using System.Net;
using System.Net.Sockets;
using System.IO;//데이터를 읽고 쓰기 위한 라이브러리
using System.Threading;//멀티 스레딩을 하기 위한 라이브러리


public class TCPManager : MonoBehaviour
{
    public InputField IPAdress;
    public InputField Port;

    [SerializeField] private Text Status;
    //기본적인 소켓통신
    //.net >> 기본단위 패킷 >> 여기선 stream이라함
    //데이터를 읽는 부분 >> thread

    StreamReader reader; //데이터 읽는 놈
    StreamWriter writer; //데이터 쓰는 놈

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
    private void ServerConnect()//서버를 열어주는 쪽 = 만드는 쪽
    {
        //지속적으로 사용 >> update처럼(메세지가 들어올때마다 오픈)
        //흐름에 예외처리가 필요함 >> Try-Catch문 활용
        try
        {
            TcpListener tcp = new TcpListener(IPAddress.Parse(IPAdress.text), int.Parse(Port.text));
            //tcplistener 객체 생성
            tcp.Start(); //서버시작 = 서버열림
            log.Enqueue("Server Open");
            //tcplistener에 연결이 될때까지 기다렸다가 client에 할당해줌
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
            //서버는 ip시작점이고 클라이언트는 ip종료점이다
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
