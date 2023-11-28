using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using LitJson;

public class UserInfo
{
    public string User_name { get; private set; }
    public string User_Password { get; private set; }

    public UserInfo(string name, string password)
    {
        User_name = name;
        User_Password = password;
    }
}

public class SQLManager : MonoBehaviour
{
    public UserInfo info;

    public MySqlConnection connection;
    public MySqlDataReader reader;

    public string DBPath = string.Empty;


    public static SQLManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DBPath = Application.dataPath + "/Database";
        string serverinfo = ServerSet(DBPath);

        try
        {
            if (serverinfo.Equals(string.Empty))
            {
                Debug.Log("SQL Server Json Error!!!");                
                return;
            }
            connection = new MySqlConnection(serverinfo);
            connection.Open();
            Debug.Log("SQL Server Open Complete!");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);            
        }
    }

    private string ServerSet(string path)
    {
        if (!File.Exists(path))//�ش� ��ο� ������ �ֳ�? ���� �� ���ǹߵ�
        {
            Directory.CreateDirectory(path);
        }
        string JsonString = File.ReadAllText(path + "/config.json");

        JsonData itemData = JsonMapper.ToObject(JsonString);
        string serverInfo =
            $"Server = {itemData[0]["IP"]};" +
            $"Database = {itemData[0]["TableName"]};" +
            $"Uid = {itemData[0]["ID"]};" +
            $"Pwd = {itemData[0]["PW"]};" +
            $"Port = {itemData[0]["PORT"]};" +
            $"CharSet = utf8;";
        
        return serverInfo;
    }


    private bool connectionCheck(MySqlConnection con)
    {
        //���� connection open�� �ƴ϶��
        if (con.State != System.Data.ConnectionState.Open)
        {
            con.Open();
            if (con.State != System.Data.ConnectionState.Open)
            {
                return false;
            }
        }
        return true;

    }

    public bool Login(string id, string pw)
    {
        //���������� db���� �����͸� ������ �ð���, ��ȸ�Ǵ� ������ ������ false��ȯ�ϰ� ��ȸ�Ǹ� true��ȯ�ϴµ� ������ ������ info�� ���� ���� ��ȯ�Ұ�
        try
        {
            if (!connectionCheck(connection))
            {
                return false;
            }

            string SQLCommand = string.Format(@"
                                SELECT User_name, User_Password FROM user_info
                                WHERE User_name = '{0}' AND User_Password = '{1}';",
                                id, pw);
            MySqlCommand cmd = new MySqlCommand(SQLCommand, connection);
            reader = cmd.ExecuteReader();
            //������ ���� �����Ͱ� 1�� �̻� �����ϴ°�
            if (reader.HasRows)
            {
                //�����ϸ� �ϳ��� ��������
                while (reader.Read())
                {
                    string name = (reader.IsDBNull(0)) ? string.Empty : reader["User_Name"].ToString();
                    string pass = (reader.IsDBNull(1)) ? string.Empty : reader["User_Password"].ToString();
                    if (!name.Equals(string.Empty) || !pass.Equals(string.Empty))
                    {
                        info = new UserInfo(name, pass);
                        if (!reader.IsClosed) reader.Close();
                        return true;
                    }
                    else//�α��� ����
                    {
                        break;
                    }
                }
            }
            if (!reader.IsClosed) reader.Close();
            return false;

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            if (!reader.IsClosed) reader.Close();
            return false;
        }

    }





}
