using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Pooling : MonoBehaviour
{
    public static Cloud_Pooling instance = null;
    [SerializeField] private int CloudCount;
    [SerializeField] private int Cloudhight;
    GameObject player;
    Rigidbody rigi;
    public Queue<GameObject> CloudPool = new Queue<GameObject>();
    public GameObject[] Cloud;

    private void Awake()
    {
        Singleton();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        MakePool();
        MakeSkyGOAT(48);
    }
    private void Update()
    {
    }
    private void MakePool()
    {
        for (int i = 0; i < CloudCount; i++)
        {
            GameObject gameObject = Instantiate(Cloud[i%6]);
            gameObject.SetActive(false);
            CloudPool.Enqueue(gameObject);
        }
    }
    private void MakeSkyGOAT(float location)
    {
        location *= 2f;
        for (int i = 0; i < CloudCount; i++)
        {
            GameObject cloud = CloudPool.Dequeue();
            cloud.SetActive(true);
            cloud.transform.position = new Vector3(Random.Range(-location, location), Cloudhight, Random.Range(-location, location));
        }
    }

    private void Singleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
