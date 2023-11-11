using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggy : Monster_controll
{
    private float runtime;
    private Rigidbody rigi;
    private bool move = false;
    private void Start()
    {
        rigi = GetComponent<Rigidbody>();
    }
    //������ ������ STAND�� ���� ������ ���� ���� �ݺ�


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(MonsterRunout());
            Invoke("MonsterHurt",1f);
        }
        MonsterMove();
    }
    public override void MonsterHurt()    // ���Ͱ� �´°� 
    {
        move = true;
        
    }

    private void running()
    {
        Look_otherside();
        Vector3 dir = transform.position - player.transform.position;
        Vector3 piggy = transform.position;
        dir.y = 0;
        piggy.y = 0;
        rigi.velocity = piggy + dir + new Vector3(Random.Range(-30, 30), 0,0); 
    }
    private IEnumerator MonsterRunout()  //�ǰ����� �˹� ��������
    {
        Vector3 dir = transform.position - player.transform.position;
        rigi.AddForce(transform.up * 150f);
        rigi.AddForce(dir* 2f);
        Invoke("Look_otherside", 0.6f);


        yield return null;
    }

    private void Look_otherside()
    {
        Vector3 dir = transform.position - player.transform.position;
        dir.y = 0;

        Quaternion rot = Quaternion.LookRotation(dir.normalized);

        transform.rotation = rot;   //�÷��̾� �ݴ���� ����
    }
    protected override void MonsterMove()
    {
        if (move)
        {
            runtime += Time.deltaTime;
            if (runtime > 0.5f)
            {
                move = false;
                runtime = 0;
            }
            running();
        }
        else
        {
            base.MonsterStand();

        }
    }

}
