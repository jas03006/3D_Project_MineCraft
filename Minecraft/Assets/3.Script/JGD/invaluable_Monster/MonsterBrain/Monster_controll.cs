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
    [Header("스탯")]
    [SerializeField] protected float Monster_Speed;
    [SerializeField] public float Monster_Damage;
    [Header("플레이어 위치")]
    protected GameObject player;
    [Header("몬스터 ID")]
    [SerializeField] public Monster_ID_J monsterID;
    [Header("몬스터 설정")]
    [SerializeField] protected AnimationCurve ani;
    protected Color Hitcolor = new Color(1f, 0.3f, 0.3f, 1f);
    protected Color monstercolor;
    protected Renderer render;
    protected Rigidbody rigi;
    protected float CurveWeighedRandom(AnimationCurve curve)
    {
        return curve.Evaluate(Random.value);
    }
    protected abstract IEnumerator MonsterStand();      //몬스터가 가만히 있을때
    //랜덤이동 
    //그러나 가만히도 있어야함
    protected abstract IEnumerator MonsterFracture();  //피격판정 넉백 도망까지
    protected virtual void MonsterDead()   //몬스터 사망
    {
        //Destroy(gameObject);
        Exp_pooling.instance.generate_exp(4, transform.position, player.transform.forward);
    }

    protected virtual void MonsterHide() {
        Biom_Manager.instance.kill_monster(monsterID, this.gameObject);
    }

    protected abstract void MonsterMove();

    public abstract void MonsterHurt(int PlayerDamage);     //몬스터가 맞았을때


}