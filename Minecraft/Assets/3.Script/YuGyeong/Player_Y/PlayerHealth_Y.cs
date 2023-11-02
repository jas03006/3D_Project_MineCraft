using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#region ����ġ
/*
 <ĳ���� ü�� ����>
- ��� : Living script
//����
    //ü��
        //���º� ��������Ʈ[]
        //�ٲ� �̹���[]

//�޼���
    //Start
        // 
    //Update
        // 
    //ü��
        //������
        //���ĸԱ�
        //�ױ�
        //UI����Ʈ�ϱ�

//�߰��� �ؾ��� ��
 */
#endregion
public class PlayerHealth_Y : Living
{
    [Header("ü��")]
    //Living : startHealth, curhealth
    public Sprite[] H_State; //0:emty,1:half,2:full
    public Image[] H_object;

    [Header("�����")]
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
        //ü��
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

        //�����
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
            Debug.Log("ü��" + curhealth);
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            OnDamage(1);
            Debug.Log("�����" + curhungry);
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
        Debug.Log("�ױ� ����");
    }


}
