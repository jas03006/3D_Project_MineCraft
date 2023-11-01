using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Test_TG : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jump_force = 100f;
    [SerializeField] private Rigidbody rigid_body;
    // Start is called before the first frame update
    void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
        deactivate_gravity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            float tilt = 0f;
            if (Input.GetKey(KeyCode.E))
            {
                tilt = 1f;
            } else if (Input.GetKey(KeyCode.Q)) {
                tilt = -1f;
            }
            transform.Translate((hor* transform.right + ver*transform.forward + tilt*transform.up) *Time.deltaTime* speed);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("jump");
                rigid_body.AddForce(jump_force * Vector3.up);
            }
        }
    }

    public void deactivate_gravity()
    {
        rigid_body.useGravity = false;
    }
    public void activate_gravity() {
        rigid_body.useGravity = true;
    }
    
}
