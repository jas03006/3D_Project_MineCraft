using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggy : Monster_controll
{
    public override void MonsterHurt()    // ���Ͱ� �´°� 
    {

    }


    private IEnumerator MonsterRunout()  //�ǰ����� �˹� ��������
    {



        yield return null;
    }
    private void Look_otherside()
    {
        Vector3 dir = transform.position - player.transform.position;
        dir.y = 0;

        Quaternion rot = Quaternion.LookRotation(dir.normalized);

        transform.rotation = rot;   //�÷��̾� �ݴ���� ����
    }
    protected override void MonsterMove()
    {
        base.MonsterStand();
    }

}
