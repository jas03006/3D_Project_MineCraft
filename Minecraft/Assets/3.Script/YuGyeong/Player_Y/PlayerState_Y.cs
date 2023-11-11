using System.Collections;
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
public class PlayerState_Y : Living
{
    private PlayerMovement_Y p_movement;
    [Header("ü��")]
    //Living : startHealth, curhealth
    public Sprite[] H_State; //0:emty,1:half,2:full
    public Image[] H_object;
    [SerializeField] private AudioClip hitclip;
    [SerializeField] private AudioClip healclip;

    [Header("�����")]
    private int starthungry;
    [SerializeField] public int curhungry
    {
        get
        {
            return private_curhungry;
        }
        set
        {
            if (value > 20)
            {
                private_curhungry = 20;
            }
            else
            {
                private_curhungry = value;
            }
        }
    }
    private int private_curhungry;
    public Sprite[] F_State; //0:emty,1:half,2:full
    public Image[] F_object;
    [SerializeField] private AudioClip eatclip;

    [Header("����ġ")]
    private int level;
    private float totalexp;
    private float curexp;
    [SerializeField] private Slider expslider;
    [SerializeField] private Text exptext;
    [SerializeField] private PlayerData_Y maxexpdata;
    [SerializeField] private AudioClip expclip;

    [Header("���ݰ��� ����")]
    public int attack_power; //���ݷ¸�ŭ ���ؼ� ������ ������
    public int att_speed; //���� �ӵ�
    public int defense_power; //���¸�ŭ ���� ������ �Ա�


    void Start()
    {
        starthealth = 20;
        starthungry = 20;
        level = 1;
        OnEnable();
        p_movement = GetComponentInParent<PlayerMovement_Y>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        curhungry = starthungry;
        curexp = 0;
        totalexp = 0;
        expslider.maxValue = maxexpdata.maxexp[0];
    }

    void Update()
    {
        Test(); //����׿�
        HungryInteraction();
        UpdateUI();
    }

    private void UpdateUI()
    {
        //ü��
        int tmp = curhealth;
        for (int i = 0; i < starthealth / 2; i++)
        {
            if (tmp <= 0)
            {
                H_object[i].sprite = H_State[0];
                H_object[i].gameObject.GetComponent<Outline>().enabled = false;
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
        int tmp2 = curhungry;
        for (int i = 0; i < starthungry / 2; i++)
        {
            if (tmp2 <= 0)
            {
                F_object[i].sprite = F_State[0];
                F_object[i].gameObject.GetComponent<Outline>().enabled = false;
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

        //exp
        expslider.value = curexp;
    }
    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnDamage(1);
            Debug.Log("ü��" + curhealth);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            curhungry -= 1;
            //HungryInteraction();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GetExp(1);
            Debug.Log($"Level:{level}/EXP:{curexp} /TotalEXP:{totalexp}");
        }
    }
    public override void OnDamage(int Damage)
    {
        if (curhealth <= 0)
        {
            return;
        }
        base.OnDamage(Damage);
    }
    public void GetExp(float exp)
    {
        curexp += exp;
        totalexp += exp;
        LevelUp();
        exptext.text = $"{level}";
    }
    private void LevelUp()
    {
        if (curexp >= maxexpdata.maxexp[level - 1])
        {
            Debug.Log("������!");
            expslider.maxValue = maxexpdata.maxexp[level];
            curexp = 0;
            expslider.value = 0;
            level++;
        }
    }
    private void HungryInteraction()
    {
        if (curhealth >= 20)
        {
            return;
        }
        Debug.Log($"hungry :{curhungry} / health :{curhealth}");
        if (curhungry >= 20)
        {
            Debug.Log($"curhungry >= 20");
            StartCoroutine(Health(1, 0.5f, 6f));
            p_movement.canrun = true;
        }
        else if (curhungry >= 18)
        {
            Debug.Log($"curhungry >= 18");
            StartCoroutine(Health(1, 4f, 8f));
            p_movement.canrun = true;
        }
        else if (curhungry > 6)
        {
            Debug.Log($"curhungry > 6");
            p_movement.canrun = true;
        }
        else if (curhungry <= 6)
        {
            Debug.Log($"curhungry <= 6");
            p_movement.canrun = false;
        }
        else if (curhungry <= 0)
        {
            Debug.Log($"curhungry <= 0");
            StartCoroutine(Health(-1, 4f, 100f));
        }
    }
    public void Hungry_cure(int hungry_cure)
    {
        curhungry += hungry_cure;
    }
    IEnumerator Hungry(int hungry, float playtime, float endtime)
    {
        if (curhungry >= 20)
        {
            curhungry = 20;
            yield break;
        }
        float tmp = 0;
        while (tmp < endtime)
        {
            tmp += Time.time;
            curhungry += hungry;
            yield return new WaitForSeconds(playtime);
        }
    }
    IEnumerator Health(int health, float playtime, float endtime)
    {
        if (curhealth >= 20)
        {
            curhealth = 20;
            yield break;
        }
        float tmp = 0;
        while (tmp < endtime)
        {
            tmp += Time.time;
            curhealth += health;
            yield return new WaitForSeconds(playtime);
            Debug.Log($"{health} / {playtime} / {endtime}");
        }
    }
    public override void Die()
    {
        base.Die();
        Debug.Log("�ױ� ����");
    }
}
