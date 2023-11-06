using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_Block_Item : MonoBehaviour
{
    [SerializeField] private float power;
    private bool blockDown = true;
    public float hight;
    public Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, 100f * Time.deltaTime, 0));
        //transform.position = new Vector3(transform.localPosition.x,  transform.localPosition.y+0.1f+ hight, transform.localPosition.z);
        //
        //
        //if (transform.localPosition.y + hight <= transform.localPosition.y || blockDown)
        //{
        //    hight += power * Time.deltaTime;
        //    blockDown = false;
        //}
        //if (hight >= 2  )
        //{
        //    hight -= power * Time.deltaTime;
        //    blockDown = true;
        //}     //둥실둥실 실패버전 진동이 나....


    }
    private void OnCollisionEnter(Collision collision)
    {
        rigidbody.AddForce(transform.up * power);
        power -= 10;
    }


}
