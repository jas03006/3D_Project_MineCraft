using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggy : Monster_controll
{
    [SerializeField] private AnimationCurve ani;
    private float runtime;
    private Rigidbody rigi;
    private bool move = false;
    private void Start()
    {
        rigi = GetComponent<Rigidbody>();
        //StartCoroutine(MonsterStand());
        StartCoroutine(testmove());
    }
    //������ ������ STAND�� ���� ������ ���� ���� �ݺ�


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StopCoroutine(MonsterStand());
            StartCoroutine(MonsterRunout());
            Invoke("MonsterHurt", 1f);
        }
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
    float CurveWeighedRandom(AnimationCurve curve)
    {
        return curve.Evaluate(Random.value);
    }
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

    public IEnumerator testmove()
    {
        pos = new Vector3();
        pos.x = Random.Range(-3f, 3f);
        pos.y = 0.9f;
        pos.z = Random.Range(-3f, 3f);

        while (true)
        {
            var dir = (pos - this.transform.position).normalized;
            this.transform.LookAt(pos);
            this.transform.position += dir * Monster_Speed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, pos);
            if (distance <= 0.1f)
            {
                yield return new WaitForSeconds(Random.Range(1f, 3f));
                pos.x = Random.Range(-3f, 3f);
                pos.z = Random.Range(-3f, 3f);
            }
        }
        yield return null;
    }
    public override IEnumerator MonsterStand()
    {
        while (true)
        {
            float dir1 = Random.Range(-3f, 3f);
            float dir2 = Random.Range(-3f, 3f);
            Vector3 target = new Vector3(dir1, 0, dir2);
            var maxTime = CurveWeighedRandom(ani);
            yield return new WaitForSeconds(Random.Range(1f,maxTime));
            Quaternion rot = Quaternion.LookRotation(new Vector3(dir1,0,dir2));
            transform.rotation = rot;
            rigi.AddForce(transform.forward*Random.Range(3600f,4650f),ForceMode.Force);
        }
    }
}
