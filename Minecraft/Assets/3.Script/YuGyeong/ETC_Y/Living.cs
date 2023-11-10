using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : MonoBehaviour
{
    [Header("체력")]
    public int curhealth;
    public int starthealth;
    public bool isDead { get; protected set; }
    // Start is called before the first frame update
    protected virtual void OnEnable()
    {
        isDead = false;
        curhealth = starthealth;
    }
    // Update is called once per frame
    public virtual void OnDamage(int Damage)
    {
        curhealth -= Damage;
        if (curhealth <= 0 && !isDead)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        isDead = true;
    }
}
