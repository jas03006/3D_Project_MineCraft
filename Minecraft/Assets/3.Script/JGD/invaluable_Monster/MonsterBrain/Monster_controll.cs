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
    protected GameObject player;
    [Header("���� ID")]
    [SerializeField] protected Monster_ID_J monsterID;
    [Header("���� ����")]
    [SerializeField] protected AnimationCurve ani;
    protected Color Hitcolor = new Color(1f, 0.3f, 0.3f, 1f);
    protected Color monstercolor;
    protected Renderer render;
    protected Rigidbody rigi;
    protected float CurveWeighedRandom(AnimationCurve curve)
    {
        return curve.Evaluate(Random.value);
    }
    protected abstract IEnumerator MonsterStand();      //���Ͱ� ������ ������
    //�����̵� 
    //�׷��� �������� �־����
    protected abstract IEnumerator MonsterFracture();  //�ǰ����� �˹� ��������
    protected abstract void MonsterDead();   //���� ���


    protected abstract void MonsterMove();

    public abstract void MonsterHurt(int PlayerDamage);     //���Ͱ� �¾�����


}