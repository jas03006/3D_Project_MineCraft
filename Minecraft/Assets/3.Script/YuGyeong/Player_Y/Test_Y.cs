using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Y : MonoBehaviour
{
    public float moveSpeed;
    public Transform spot;
    //x축으로 60도 돌린 다음에 원 그리기
    // Start is called before the first frame update
    void Start()
    {
        
        transform.Rotate(new Vector3(30, 0, 180), Space.Self);
        //Debug.Log("돌아");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ani_righthand();
    }

    private void ani_righthand()
    {
        transform.RotateAround(spot.position, Vector3.forward, 5);
    }
}
