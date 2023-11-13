using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Monster_controll
{
    [SerializeField] Renderer[] renders;
    Color zomcolor = new Color(1f, 0.2f, 0.2f, 1f);
    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
        renders = GetComponentsInChildren<Renderer>();
        //monstercolor = renders.material.color;
    }
    private void Start()
    {
        Rengering();
        StartCoroutine(ZombieStand());
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            MonsterHurt();
        }
    }
    public override void MonsterHurt()
    {
        move = false;
        StartCoroutine(MonsterFracture());
    }

    private void Rengering()
    {
        for (int i = 0; i < renders.Length; i++)
        {
            monstercolor = renders[i].material.color;
        }
    }

    protected virtual IEnumerator ZombieStand()      //좀비가 가만히 있을때
    {
        var moveTime = CurveWeighedRandom(ani);
        var dir = new Vector3();
        dir.x = Random.Range(-10f, 10f);
        dir.y = 0;
        dir.z = Random.Range(-10f, 10f);
        pos = dir + this.transform.position;
        transform.forward = dir.normalized;
        if (move)
        {
            while (true)
            {

                float zombietimer = 0f;
                zombietimer += Time.deltaTime;
                this.transform.position += transform.forward * Monster_Speed * Time.deltaTime;

                float distance = Vector3.Distance(transform.position, pos);

                if (distance <= 0.1f || zombietimer > 0.3f) //얘 왜 0.3임? 진짜 모름 ㄹㅇ
                {
                    zombietimer = 0f;

                    yield return new WaitForSeconds(Random.Range(1f, moveTime));

                    dir.x = Random.Range(-3f, 3f);
                    dir.z = Random.Range(-3f, 3f);
                    pos = dir + this.transform.position;
                    transform.forward = dir.normalized;
                }
                yield return null;
            }
        }
    }
    protected override void MonsterMove()
    {

    }
    protected override IEnumerator MonsterFracture()
    {
        Vector3 dir = transform.position - player.transform.position;

        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < renders.Length; i++)
        {
            for (int j = 0; j < renders.Length; j++)
            {
                renders[i].materials[j].color = zomcolor;
            }
        }
        //renders[0].materials[0].color = Color.red;

        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < renders.Length; i++)
        {
            for (int j = 0; j < renders.Length; j++)
            {
                renders[i].materials[j].color = monstercolor;
            }
        }

        //renders[0].material.color = monstercolor;
        //renders[1].material.color = monstercolor;
        //renders[2].material.color = monstercolor;
        //renders[3].material.color = monstercolor;
        //renders[4].material.color = monstercolor;
        //renders[5].material.color = monstercolor;


        rigi.AddForce(transform.up * 150f);
        rigi.AddForce(dir * 15f);

        move = true;
        yield break;

    }
}
