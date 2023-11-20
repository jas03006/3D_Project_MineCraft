using System.Collections.Generic;
using UnityEngine;

public class Exp_pooling : MonoBehaviour
{
    public static Exp_pooling instance = null;
    private Queue<GameObject> Exp_queue;
    [SerializeField] private GameObject exp_prefab;
    [SerializeField] private Vector3 pooling_point;
    private int init_count;
    public GameObject player;

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
        player = GameObject.FindGameObjectWithTag("Player");
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

    //큐에서 꺼내기
    public GameObject get_pool(Vector3 transform, Quaternion quaternion)
    {
        //있으면 꺼내 쓰기
        if (Exp_queue.Count != 0)
        {
            GameObject obj = Exp_queue.Dequeue();
            obj.transform.position = transform;
            obj.transform.rotation = quaternion;
            obj.SetActive(true);
            return obj;
        }
        //없으면 새로 만들기
        else
        {
            GameObject obj = Instantiate(exp_prefab,transform,quaternion);
            obj.SetActive(true);
            return obj;
        }
    }

    public void generate_exp(int exp_num, Vector3 transform) {
        for (int i =0; i < exp_num; i++) {
            GameObject go = get_pool(transform + Vector3.forward * Random.Range(-0.3f, 0.3f) + Vector3.right * Random.Range(-0.3f, 0.3f), Quaternion.LookRotation(player.transform.forward * -1f));
        }
    }

    //큐에 반납하기
    public void return_pool(GameObject exp)
    {
        Exp_queue.Enqueue(exp);
        exp.SetActive(false);
    }

}
