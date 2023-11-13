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
    [SerializeField] protected float Monster_Damage;
    [Header("플레이어 위치")]
    [SerializeField] protected GameObject player;
    protected Color monstercolor;
    [Header("몬스터 ID")]
    [SerializeField] protected Monster_ID_J monsterID;
    [Header("몬스터 설정")]
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
    protected virtual IEnumerator MonsterStand()      //몬스터가 가만히 있을때
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
    //랜덤이동 
    //그러나 가만히도 있어야함
    protected abstract IEnumerator MonsterFracture();  //피격판정 넉백 도망까지


    protected abstract void MonsterMove();

    public abstract void MonsterHurt();


}