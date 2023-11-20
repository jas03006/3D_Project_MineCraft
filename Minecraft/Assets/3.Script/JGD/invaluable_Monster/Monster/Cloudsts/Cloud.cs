using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float directionTime;
    [SerializeField] private float directionTimeControll;

    private static float directionX;
    private static float directionZ;
    GameObject player;
    Rigidbody rigi;
    Rigidbody cloudrigii;

    Cloud_Pooling _Pooling;
    
    private void Awake()
    {
        _Pooling = GetComponent<Cloud_Pooling>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        cloudrigii = GetComponent<Rigidbody>();
        rigi = player.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        CloudMovement();
        CloudScroll(48);
    }

    private void CloudMovement()
    {
        Direction();
        cloudrigii.velocity = new Vector3(directionX,0,directionZ);
    }

    private void Direction()
    {
        directionTime += Time.deltaTime;

        if (directionTime >= directionTimeControll)
        {
            directionTime = 0f;
            directionX = Random.Range(-1f, 1f);
            directionZ = Random.Range(-1f, 1f);
        }
    }

    private void CloudScroll(float lender)         //¿Ã∞‘ «ŸΩ…
    {
        lender *= 3;
        Vector3 pos = player.transform.position;
        pos.y = 0;
        Vector3 myPos = this.transform.position;
        myPos.y = 0;

        float distance = Vector3.Distance(myPos, pos);

        float difX = Mathf.Abs(pos.x - myPos.x);
        float difZ = Mathf.Abs(pos.z - myPos.z);

        Vector3 playerDir = rigi.velocity;
        Debug.Log(rigi.velocity);
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirZ = playerDir.z < 0 ? -1 : 1;
        
        if (distance >= lender)
        {
            if (difX > difZ)
            {
                transform.Translate(playerDir * 10 + new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f)));
            }
            else if (difX < difZ)
            {
                transform.Translate(playerDir * 10 + new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f)));
            }
        }
    }

}