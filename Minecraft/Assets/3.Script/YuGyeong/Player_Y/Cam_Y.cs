using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Y : MonoBehaviour
{
    public float speed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
    }
}
