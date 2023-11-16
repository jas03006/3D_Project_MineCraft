using System.Collections.Generic;
using UnityEngine;

public class Exp_pooling : MonoBehaviour
{
    public static Exp_pooling instance = null;
    private Queue<GameObject> Exp_queue;
    [SerializeField] private GameObject exp_prefab;
    [SerializeField] private Vector3 pooling_point;
    private int init_count;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void Start()
    {
        init_count = 10;
        init_pool();
    }

    public void init_pool()
    {
        Exp_queue = new Queue<GameObject>();
        for (int i = 0; i < init_count; i++)
        {
            GameObject obj = Instantiate(exp_prefab, pooling_point, Quaternion.identity);
            Exp_queue.Enqueue(obj);
            obj.SetActive(false);
        }
    }

    //ť���� ������
    public GameObject get_pool(Vector3 transform, Quaternion quaternion)
    {
        //������ ���� ����
        if (Exp_queue.Count != 0)
        {
            GameObject obj = Exp_queue.Dequeue();
            obj.transform.position = transform;
            obj.transform.rotation = quaternion;
            obj.SetActive(true);
            return obj;
        }
        //������ ���� �����
        else
        {
            GameObject obj = Instantiate(exp_prefab,transform,quaternion);
            obj.SetActive(true);
            return obj;
        }
    }

    //ť�� �ݳ��ϱ�
    public void return_pool(GameObject exp)
    {
        exp = Exp_queue.Dequeue();
        exp.SetActive(false);
    }

}
