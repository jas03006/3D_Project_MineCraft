using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggy : Monster_controll
{
    public override void MonsterHurt()    // 몬스터가 맞는거 
    {

    }


    private IEnumerator MonsterRunout()  //피격판정 넉백 도망까지
    {



        yield return null;
    }
    private void Look_otherside()
    {
        Vector3 dir = transform.position - player.transform.position;
        dir.y = 0;

        Quaternion rot = Quaternion.LookRotation(dir.normalized);

        transform.rotation = rot;   //플레이어 반대방향 보기
    }
    protected override void MonsterMove()
    {
        base.MonsterStand();
    }

}
