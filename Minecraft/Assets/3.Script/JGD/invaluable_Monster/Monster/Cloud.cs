using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float directionTime;
    [SerializeField] private float directionTimeControll;

    private static float directionX;
    private static float directionZ;
    Rigidbody rigi;

    [SerializeField] GameObject Player;

    private void Start()
    {
        Player = GetComponent<GameObject>();
        rigi = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        CloudMovement();
    }

    private void CloudMovement()
    {
        Direction();
        rigi.velocity = new Vector3(directionX,0,directionZ);
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
    private void CloudSystem()
    {
        if (this.transform.position.z)
        {

        }
    }
}