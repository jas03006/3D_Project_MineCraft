using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asdfsdfsdf : MonoBehaviour
{
    public Vector3 inputVec;
    public float moveSpeed;
    private Rigidbody charRigidbody;


    private void Awake()
    {
        charRigidbody = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(inputVec.x, 0, inputVec.z);

        charRigidbody.velocity = inputDir * moveSpeed;

        transform.LookAt(transform.position + inputDir);

    }
}
