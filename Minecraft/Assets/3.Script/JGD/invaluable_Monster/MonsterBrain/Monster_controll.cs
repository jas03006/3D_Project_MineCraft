using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    protected Vector3 pos;
    [SerializeField] protected float Monster_Speed;
    [SerializeField] protected float Monster_Damage;
    [SerializeField] protected GameObject player;


    public virtual void MonsterAtteck()
    {

    }

    public abstract IEnumerator MonsterStand();
        //랜덤이동 
        //그러나 가만히도 있어야함


    protected abstract void MonsterMove();

    public abstract void MonsterHurt();


}