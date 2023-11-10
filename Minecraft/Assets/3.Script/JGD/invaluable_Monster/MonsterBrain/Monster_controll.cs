using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MonsterData
{
    [SerializeField] public float Monster_Speed;
    [SerializeField]public float Monster_Damage;
}

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
    public MonsterData monsterData;
    [SerializeField] public GameObject player;

    public virtual void MonsterAtteck()
    {

    }

    public virtual void MonsterStand()
    {

    }

    protected abstract void MonsterMove();

    public abstract void MonsterHurt();


}