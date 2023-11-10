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
     몬스터는 뭐가있는가
    Hp가있고
    데미지가 있고
    이동을 하며
    죽으면 아이템이 나온다
    평소에는 랜덤으로 이동하고
    맞으면 온몸이 빨간색으로 피격처리후 넉백받는다.
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