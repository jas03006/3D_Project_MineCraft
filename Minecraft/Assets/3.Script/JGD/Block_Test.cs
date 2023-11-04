using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Test : MonoBehaviour
{

    public Item_ID_TG id;
    public float Max_Block_Hp = 100f;
    public float blockHp = 100f;


    public void Destroy_Block()
    {
        if (blockHp <= 0f)
        {
            Destroy(gameObject);
            itemposition();
            //Block_Objectpooling.instance.Get(blocknum);
        }
    }

    public void itemposition()
    {
        Block_Objectpooling.instance.Get(id);
        
    }


}
