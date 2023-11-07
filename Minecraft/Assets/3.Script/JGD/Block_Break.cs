using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Break : MonoBehaviour
{
    
    public Item_ID_TG id;
    public float Max_Block_Hp = 100f;
    public float blockHp = 100f;
    public float block_break = 0;

    public int i = 1;

    [SerializeField] GameObject Box;
    public Material[] mat = new Material[11];

    MeshRenderer meshRenderer;


    private void Start()
    {
        meshRenderer = transform.parent.GetComponent<MeshRenderer>();
    }

    public void Destroy_Block()
    {
        if (blockHp <= 0f)
        {
            //Destroy(Box);
            Box.SetActive(false);
            itemposition();
            block_return();
        }
    }

    private void Update()
    {
        Block_break_motion();
    }


    public void itemposition()
    {
        Block_Objectpooling.instance.Get(id, transform.position);
        
    }



    private void Block_break_motion()
    {
        //gameObject.GetComponent<MeshRenderer>().materials.SetValue(mat[((int)Max_Block_Hp - (int)blockHp)/10],1) ;

        meshRenderer.material = mat[((int)Max_Block_Hp - (int)blockHp) / 10];//meshRenderer.material.name.StartsWith(mat[i].name) ?;


    }
    public void block_return() //Hp풀로 채우고 사진 원위치
    {
        blockHp = Max_Block_Hp;
    }
}
