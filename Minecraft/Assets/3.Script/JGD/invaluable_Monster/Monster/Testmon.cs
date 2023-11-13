using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testmon : MonoBehaviour
{
    [SerializeField] protected float Monster_Speed;
    [SerializeField] protected float Monster_Damage;
    [SerializeField] protected GameObject player;
    private Vector3 pos; //아래 monsterstand

    public float Speed = 50.0f;
    private Transform myTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveAmount = Speed * Vector3.forward * Time.deltaTime;
        myTransform.Translate(moveAmount);

        if (myTransform.position.z >= 10.0f)
        {
            myTransform.position = new Vector3(Random.Range(-60.0f, 60.0f), 0.0f, 0.0f);
        }
    }

    public virtual IEnumerator MonsterStand()
    {
        //랜덤이동 
        //그러나 가만히도 있어야함

        pos = new Vector3();
        pos.x = Random.Range(-3f, 3f);
        pos.z = Random.Range(-3f, 3f);

        while (true)
        {
            var dir = (pos - this.transform.position).normalized;
            this.transform.LookAt(pos);
            this.transform.position += dir * Monster_Speed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, pos);

            if (distance <= 0.1f)
            {
                yield return new WaitForSeconds(Random.Range(1f, 5f));
                pos.x = Random.Range(-3f, 3f);
                pos.z = Random.Range(-3f, 3f);
            }

            yield return null;
        }

    }
}
