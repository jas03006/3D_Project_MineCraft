using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : Monster_controll
{
    [SerializeField] private float piggyJumppower;
    Villager villager;
    Animator animation;
    protected bool move = true;
    private int VillagerHp;
    private int ItemCount = 1;
    private int JumpCount = 1;
    float Maxtimer = 0;
    [SerializeField] private GameObject body;
    [Header("마을사람 사운드")]
    [SerializeField] private AudioClip[] VillgerCall;
    [SerializeField] private AudioClip[] VillgerHit;
    [SerializeField] private AudioClip VillgerDead;
    private AudioSource audioSource;
    [Header("마을사람 맞을때")]
    [SerializeField] private GameObject Eyes;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        move = true;
        animation = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        render = GetComponentInChildren<Renderer>();
        monstercolor = render.material.color;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ItemCount = 1;
        VillagerHp = starthealth;
        StartCoroutine(MonsterStand());
        Eyes.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            MonsterHurt(20);
        }
    }
    protected override IEnumerator MonsterStand()
    {
        while (true)
        {
            pos = player.transform.position;
            float sqr_distance = (pos - this.transform.position).sqrMagnitude;
            if (sqr_distance <= 4f)
            {
                Vector3 look = pos - body.transform.position;
                look.y = 90;
                body.transform.rotation = Quaternion.LookRotation(look).normalized;
            }
            else
            {
            }
            yield return null;
        }
    }

    protected override IEnumerator MonsterFracture()
    {
        Vector3 dir = transform.position - player.transform.position;
        yield return new WaitForSeconds(0.1f);
        render.material.color = Hitcolor;
        yield return new WaitForSeconds(0.1f);
        render.material.color = monstercolor;
        rigi.AddForce(transform.up * 150f);
        rigi.AddForce(dir * 10f);

        yield break;
    }
   

    protected override void MonsterMove()
    {
        throw new System.NotImplementedException();
    }

    public override void MonsterHurt(int PlayerDamage)
    {
        VillagerHp = VillagerHp - PlayerDamage;
        Eyes.SetActive(true);
        if (VillagerHp > 0)
        {
            VillagerHitSound();
            StartCoroutine(MonsterFracture());
            StopCoroutine(MonsterStand());
            move = true;
            OnDamage(PlayerDamage);
            Invoke("Villagerfrighten",2f);
        }
        else if (VillagerHp <= 0)
        {
            StopAllCoroutines();
            if (ItemCount == 1)
            {
                StartCoroutine(MonsterFracture());
                Villager_Dead();
                animation.SetTrigger("NPCDead");
                ItemCount--;
            }
            MonsterDead();
            Invoke("MonsterHide", 2f);
        }
    }

    private void Look_otherside()     // 플레이어 방대방향 보기
    {
        Vector3 dir = transform.position - player.transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;
    }
   
    private void Villagerfrighten()
    {
        Eyes.SetActive(false);
    }
    private void VillagerHitSound()  //마을사람 맞을때
    {
        audioSource.clip = VillgerHit[Random.Range(0,2)]; //이거 픽스
        audioSource.Play();
    }

    private void Villager_Dead()     //마을사람 죽을때
    {
        audioSource.clip = VillgerDead;
        audioSource.Play();
    }
}