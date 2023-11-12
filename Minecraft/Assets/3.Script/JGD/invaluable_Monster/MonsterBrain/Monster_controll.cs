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
    [SerializeField] protected float Monster_Speed;
    [SerializeField] protected float Monster_Damage;
    [SerializeField] protected GameObject player;


    public virtual void MonsterAtteck()
    {

    }

    public abstract IEnumerator MonsterStand();
        //�����̵� 
        //�׷��� �������� �־����


    protected abstract void MonsterMove();

    public abstract void MonsterHurt();


}