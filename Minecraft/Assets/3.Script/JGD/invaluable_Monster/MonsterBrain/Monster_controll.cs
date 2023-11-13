using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Monster_controll : Living
{
    /*
     ���ʹ� �����ִ°�
    Hp���ְ�
    �������� �ְ�
    �̵��� �ϸ�
    ������ �������� ���´�
    ��ҿ��� �������� �̵��ϰ�
    ������ �¸��� ���������� �ǰ�ó���� �˹�޴´�.
     */
    protected Vector3 pos;
    [Header("����")]
    [SerializeField] protected float Monster_Speed;
    [SerializeField] protected float Monster_Damage;
    [Header("�÷��̾� ��ġ")]
    [SerializeField] protected GameObject player;
    protected Color monstercolor;
    [Header("���� ID")]
    [SerializeField] protected Monster_ID_J monsterID;
    [Header("���� ����")]
    [SerializeField] protected AnimationCurve ani;
    protected Color Hitcolor = new Color(1f, 0.3f, 0.3f, 1f);
    protected Renderer render;
    protected Rigidbody rigi;
    protected bool move = true;
    public virtual void MonsterAtteck()
    {

    }
    protected float CurveWeighedRandom(AnimationCurve curve)
    {
        return curve.Evaluate(Random.value);
    }
    protected virtual IEnumerator MonsterStand()      //���Ͱ� ������ ������
    {
        var moveTime = CurveWeighedRandom(ani);
        var dir = new Vector3();
        dir.x = Random.Range(-3f, 3f);
        dir.y = 0;
        dir.z = Random.Range(-3f, 3f);
        pos = dir + this.transform.position;
        transform.forward = dir.normalized;
        if (move)
        {
            while (true)
            {

                float Maxtimer = 0f;
                Maxtimer += Time.deltaTime;
                this.transform.position += transform.forward * Monster_Speed * Time.deltaTime;

                float distance = Vector3.Distance(transform.position, pos);

                if (distance <= 0.1f || Maxtimer > 3f)
                {
                    Maxtimer = 0f;

                    yield return new WaitForSeconds(Random.Range(1f, moveTime));

                    dir.x = Random.Range(-3f, 3f);
                    dir.z = Random.Range(-3f, 3f);
                    pos = dir + this.transform.position;
                    transform.forward = dir.normalized;
                }
                yield return null;
            }
        }
    }
    //�����̵� 
    //�׷��� �������� �־����
    protected abstract IEnumerator MonsterFracture();  //�ǰ����� �˹� ��������


    protected abstract void MonsterMove();

    public abstract void MonsterHurt();


}