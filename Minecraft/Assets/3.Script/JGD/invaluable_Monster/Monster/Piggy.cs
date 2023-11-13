using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggy : Monster_controll
{
    

    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
        render = GetComponentInChildren<Renderer>();
        monstercolor = render.material.color;
    }
    private void Start()
    {
        StartCoroutine(MonsterStand());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //StartCoroutine(MonsterRunout());
            StartCoroutine(MonsterRunout());
            Invoke("MonsterHurt",3f);
        }
    }

    public override void MonsterHurt()    // ���Ͱ� �´°� 
    {
        StopCoroutine(base.MonsterStand());
        move = true;
    }

    //private IEnumerator MonsterFracture()  //�ǰ����� �˹� ��������
    //{
    //    Vector3 dir = transform.position - player.transform.position;
    //    yield return new WaitForSeconds(0.1f);
    //    render.material.color = Hitcolor;
    //    yield return new WaitForSeconds(0.1f);
    //    render.material.color = monstercolor;
    //    rigi.AddForce(transform.up * 150f);
    //    rigi.AddForce(dir* 10f);
    //    Invoke("Look_otherside", 0.6f);
    //
    //    yield return null;
    //}
    private void Look_otherside()
    {
        Vector3 dir = transform.position - player.transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;   //�÷��̾� �ݴ����
    }
    protected override void MonsterMove()    //stand ���� �����ŷ� ��ȯ
    {
    
    }

    public IEnumerator MonsterRunout()
    {
        yield return StartCoroutine(MonsterFracture());
        yield return new WaitForSeconds(1f);

        move = false;
        var dir = new Vector3();
        dir.x = Random.Range(-3f, 3f);
        dir.y = 0;
        dir.z = Random.Range(-3f, 3f);
        pos = dir + this.transform.position;
        transform.forward = dir.normalized;
        float Standtimer = 0f;
        float Maxtimer = 0f;
        while (true)
        {
            Maxtimer += Time.deltaTime;
            Standtimer += Time.deltaTime;
            this.transform.position += transform.forward * Monster_Speed*5 * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, pos);

            if (distance <= 0.1f || Maxtimer > 3f)
            {
                Maxtimer = 0f;

                yield return new WaitForSeconds(0.1f);

                dir.x = Random.Range(-3f, 3f);
                dir.z = Random.Range(-3f, 3f);
                pos = dir + this.transform.position;
                transform.forward = dir.normalized;

            }
            yield return null;
            if (Standtimer > 5f)
            {
                move = true;
                yield break;
            }
        }

    }

    protected override IEnumerator MonsterFracture()
    {
        Vector3 dir = transform.position - player.transform.position;
        yield return new WaitForSeconds(0.1f);
        render.material.color = Hitcolor;
        yield return new WaitForSeconds(0.1f);
        render.material.color = monstercolor;
        rigi.AddForce(transform.up * 150f);
        rigi.AddForce(dir * 10f);

        yield break;

    }
}

