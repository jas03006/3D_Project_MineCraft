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
        //죽었는지 안죽었는지
        if (curhealth <= 0 && !isDead)
        {
            //죽는 메소드를 호출
            Die();
        }
    }

    public virtual void Die()
    {
        isDead = true;
    }
}
