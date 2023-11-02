using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : MonoBehaviour
{
    [Header("ü��")]
    public int starthealth = 20;
    public int curhealth { get; protected set; }
    public bool isDead { get; protected set; }

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
