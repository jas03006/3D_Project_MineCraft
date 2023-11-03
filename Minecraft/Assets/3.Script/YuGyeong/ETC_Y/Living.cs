using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : MonoBehaviour
{
    public int starthealth = 20;
    public int curhealth;
    public bool isDead;

    protected virtual void OnEnable()
    {
        isDead = false;
        curhealth = starthealth;
    }

    public virtual void OnDamage(int Damage)
    {
        curhealth -= Damage;
        //�׾����� ���׾�����
        if (curhealth <= 0 && !isDead)
        {
            //�״� �޼ҵ带 ȣ��
            Die();
        }
    }

    public virtual void Die()
    {
        isDead = true;
    }
}
