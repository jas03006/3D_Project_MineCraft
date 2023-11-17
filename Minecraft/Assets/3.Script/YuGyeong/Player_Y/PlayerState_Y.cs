using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum consumption_Y
{
    jump = 0,
    mon_attack,
    block_attack
}

public class PlayerState_Y : Living
{
    private PlayerMovement_Y p_movement;
    [Header("health")]
    //Living : startHealth, curhealth
    public Sprite[] H_State; //0:emty,1:half,2:full
    public Image[] H_object;
    [SerializeField] private AudioClip hitclip;
    [SerializeField] private AudioClip healclip;
    public int Curhealth
    {
        get
        {
            return curhealth;
        }
        set
        {
            curhealth = value;
            if (value > 20)
            {
                curhealth = 20;
            }
        }
    }

    [Header("hungry")]
    private int starthungry;
    [SerializeField]
    public int curhungry
    {
        get
        {
            return private_curhungry;
        }
        set
        {
            private_curhungry = value;
            if (value > 20)
            {
                private_curhungry = 20;
            }
        }
    }
    private int private_curhungry;
    public Sprite[] F_State; //0:emty,1:half,2:full
    public Image[] F_object;
    [SerializeField] private AudioClip eatclip;
    public Coroutine hungry_recover_co;
    [SerializeField] private float consumption;
    [SerializeField] private int max_consumption;

    [Header("exp")]
    private int level;
    public float totalexp;
    private float curexp
    {
        get { return private_curexp; }
        set
        {
            private_curexp = value;
        }
    }
    private float private_curexp;
    [SerializeField] private Slider expslider;
    [SerializeField] private Text exptext;
    [SerializeField] private PlayerData_Y maxexpdata;
    [SerializeField] private AudioClip expclip;

    [Header("damage")]
    public int attack_power = 2;
    public float att_speed = 1;
    public int defense_power = 1;

    public Vector3 original_spawn_position;
    public Bed_TG respawn_bed = null;

    void Start()
    {
        OnEnable();
        p_movement = GetComponentInParent<PlayerMovement_Y>();
        StartCoroutine(Hungry(1, 30));
    }

    protected override void OnEnable() // 초기화
    {
        base.OnEnable();
        starthealth = 20;
        starthungry = 20;
        curhungry = starthungry;
        curexp = 0;
        totalexp = 0;
        level = 1;
        expslider.maxValue = maxexpdata.maxexp[0];
        max_consumption = 4;

        UpdateUI_health();
        Update_hungry();
        UpdateUI_exp();
    }

    void Update()
    {
        Test();
       // Debug.Log($"curhealth :{Curhealth}");
    }

    private void UpdateUI_health()
    {
        //health
        int tmp = Curhealth;
        for (int i = 0; i < starthealth / 2; i++)
        {
            if (tmp == 0)
            {
                H_object[i].sprite = H_State[0];
                H_object[i].gameObject.GetComponent<Outline>().enabled = false;
            }
            else if (tmp == 1)
            {
                H_object[i].sprite = H_State[1];
                H_object[i].gameObject.GetComponent<Outline>().enabled = true;
            }
            else
            {
                H_object[i].sprite = H_State[2];
                H_object[i].gameObject.GetComponent<Outline>().enabled = true;
            }
            tmp -= 2;
        }
    }//Update UI
    private void Update_hungry()
    {
        //상호작용
        HungryInteraction();

        //Update UI
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
                F_object[i].gameObject.GetComponent<Outline>().enabled = true;
            }
            else
            {
                F_object[i].sprite = F_State[2];
                F_object[i].gameObject.GetComponent<Outline>().enabled = true;
            }
            tmp2 -= 2;
        }

    }//Update UI + HungryInteraction
    private void UpdateUI_exp()
    {
        //exp
        expslider.value = curexp;
    }//Update UI

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnDamage(1);
            Debug.Log(curhealth);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GetExp(1);
        }
    } //Test

    public override void OnDamage(int Damage)
    {
        base.OnDamage(Damage);
        UpdateUI_health();
    }
    IEnumerator Health(int health, float playtime, float endtime)
    {
        float time = 0;
        while (time < endtime)
        {
            //Debug.Log($"{health} / {playtime} / {endtime} / curhealth : {curhealth}");
            time += playtime;
            Curhealth += health;
            UpdateUI_health();
            yield return new WaitForSeconds(playtime);
        }
    }

    public void Consumption_Up(consumption_Y con)
    {
        float value = 0;
        switch (con)
        {
            case consumption_Y.jump:
                value = 0.05f;
                break;
            case consumption_Y.mon_attack:
                value = 0.1f;
                break;
            case consumption_Y.block_attack:
                value = 0.005f;
                break;
        }
        consumption += value;

        if (consumption > max_consumption)
        {
            consumption = 0;
            curhungry--;
            Update_hungry();
        }
    }

    public void GetExp(float exp)
    {
        curexp += exp;
        totalexp += exp;
        LevelUp();
        exptext.text = $"{level}";
        UpdateUI_exp();
    }

    private void LevelUp()
    {
        if (curexp >= maxexpdata.maxexp[level - 1])
        {
            expslider.maxValue = maxexpdata.maxexp[level];
            curexp = 0;
            expslider.value = 0;
            level++;
        }
    }

    private void HungryInteraction()
    {
        //Debug.Log("HungryInteraction");
        if (curhungry == 0 && curhealth != 0) //배고픔0이고 안죽었을때
        {
            if (hungry_recover_co != null)
            {
                StopCoroutine(hungry_recover_co);
                //Debug.Log("StopCoroutine(hungry_recover_co);");
            }
            hungry_recover_co = StartCoroutine(Health(-1, 1, 1000));
        }

        if (curhungry == starthungry && curhealth != starthealth) // 풀피 아니고 배고픔 풀일때
        {
            if (hungry_recover_co != null)
            {
                StopCoroutine(hungry_recover_co);
                //Debug.Log("StopCoroutine(hungry_recover_co);");
            }
            hungry_recover_co = StartCoroutine(Health(1, 1, 4));
        }

        if (curhungry < starthungry && curhungry > starthungry - 2 && curhealth != starthealth) // 풀피> 현재체력> 풀피-2 이고 풀피 아닐때
        {
            if (hungry_recover_co != null)
            {
                StopCoroutine(hungry_recover_co);
                //Debug.Log("StopCoroutine(hungry_recover_co);");
            }
            hungry_recover_co = StartCoroutine(Health(1, 4, 8));
        }

        //배고픔 6이하면 뛸 수 없음
        //if (curhungry > 6)
        //{
        //    p_movement.canrun = true;
        //}
        //else
        //{
        //    p_movement.canrun = false;
        //}
    }
    public void Hungry_cure(int hungry_cure)
    {
        curhungry += hungry_cure;
        HungryInteraction();
    }
    IEnumerator Hungry(int hungry, float playtime)
    {
        float tmp = 0;
        while (true)
        {
            yield return new WaitForSeconds(playtime);
            tmp += playtime;
            curhungry -= hungry;
            Update_hungry();
        }
    }
    

    public override void Die()
    {
        base.Die();
        respawn();
    }
    public void respawn()
    {
        if (hungry_recover_co != null) {
            StopCoroutine(hungry_recover_co);
        }
        Curhealth = 20;
        curhungry = 20;
        curexp = 0;
        totalexp = 0;
        level = 1;
        isDead = false;

        for (int i = 0; i < H_object.Length; i++)
        {
            H_object[i].gameObject.GetComponent<Outline>().enabled = true;
            F_object[i].gameObject.GetComponent<Outline>().enabled = true;
        }

        (p_movement as Player_Test_TG).stop();

        Biom_Manager.instance.return_all_chunk();
        transform.position = get_respawn_position();
        StartCoroutine(lose_gravity_co());
        Snow_TG.instance.reset_snows();
    }
    private IEnumerator lose_gravity_co()
    {
        (p_movement as Player_Test_TG).deactivate_gravity();
        yield return new WaitForSeconds(1.2f);
        (p_movement as Player_Test_TG).activate_gravity();
    }
    public Vector3 get_respawn_position()    {
        
        if (respawn_bed == null)
        {
            return original_spawn_position;
        }
        return respawn_bed.get_respawn_position();
    }
}
