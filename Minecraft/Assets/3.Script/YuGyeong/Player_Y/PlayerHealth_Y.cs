using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#region 스케치
/*
 <캐릭터 체력 구현>
- 상속 : Living script
//변수
    //체력
        //상태별 스프라이트[]
        //바꿀 이미지[]

//메서드
    //Start
        // 
    //Update
        // 
    //체력
        //데미지
        //음식먹기
        //죽기
        //UI프린트하기

//추가로 해야할 것
 */
#endregion
public class PlayerHealth_Y : Living
{
    [Header("체력")]
    //Living : startHealth, curhealth
    public Sprite[] H_State; //0:emty,1:half,2:full
    public Image[] H_object;

    [Header("배고픔")]
    private int starthungry;
    private int curhungry;
    public Sprite[] F_State; //0:emty,1:half,2:full
    public Image[] F_object;

    void Start()
    {
        starthealth = 20;
        starthungry = 20;
        OnEnable();
    }

    void Update()
    {
        Test();
    }

    private void UpdateUI()
    {
        //체력
        if (curhealth > starthealth)
        {
            curhealth = starthealth;
        }

        int tmp = curhealth;
        for (int i = 0; i < starthealth / 2; i++)
        {
            if (tmp <= 0)
            {
                H_object[i].sprite = H_State[0];
            }
            else if (tmp == 1)
            {
                H_object[i].sprite = H_State[1];
            }
            else
            {
                H_object[i].sprite = H_State[2];
            }
            tmp -= 2;
        }

        //배고픔
        if (curhungry > starthungry)
        {
            curhungry = starthungry;
        }

        int tmp2 = curhungry;
        for (int i = 0; i < starthungry / 2; i++)
        {
            if (tmp2 <= 0)
            {
                F_object[i].sprite = F_State[0];
            }
            else if (tmp2 == 1)
            {
                F_object[i].sprite = F_State[1];
            }
            else
            {
                F_object[i].sprite = F_State[2];
            }
            tmp2 -= 2;
        }
    }
    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnDamage(1);
            Debug.Log("체력" + curhealth);
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            OnDamage(1);
            Debug.Log("배고픔" + curhungry);
            UpdateUI();
        }
    }

    public override void OnDamage(int Damage)
    {
        base.OnDamage(Damage);
    }

    public void OnHungry(int hungry)
    {
        curhungry -= hungry;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("죽기 성공");
    }


}
