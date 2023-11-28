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
        if (!File.Exists(path))//해당 경로에 파일이 있나? 없을 때 조건발동
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
        //현재 connection open이 아니라면
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
        //직접적으로 db에서 데이터를 가지고 올것임, 조회되는 데이터 없으면 false반환하고 조회되면 true반환하는데 위에서 선언한 info에 담은 담음 반환할것
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
            //리더에 읽은 데이터가 1개 이상 존재하는가
            if (reader.HasRows)
            {
                //존재하면 하나씩 나열해줘
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
                    else//로그인 실패
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
