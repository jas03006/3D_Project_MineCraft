using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_TG : Block_TG, Interactive_TG
{
    //private bool is_open = false;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        is_open = false;
        contain_data = new List<KeyValuePair<Item_ID_TG, int>>();
    }
    public void react()
    {
        is_open = true;
        animator.SetBool("Is_Open",is_open);
        Action<List<Slot_Y>> callback = close;
        Inventory.instance.show_box(contain_data,callback);
    }

    public void close(List<Slot_Y> data) {
        contain_data.Clear();
        for (int i =0; i < data.Count; i++) {
            //Debug.Log($"{i}: {data[i].item_id}");
            contain_data.Add(new KeyValuePair<Item_ID_TG, int>(data[i].item_id, data[i].number));
        }
        is_open = false;
        animator.SetBool("Is_Open", is_open);        
    }
    public override void init(bool is_open_, List<KeyValuePair<Item_ID_TG, int>> contain_data_) {
        contain_data = contain_data_;
        is_open = is_open_;
        animator.SetBool("Is_Open", is_open);
    }

    public override void drop_items()
    {
        Inventory.instance.hide();
        if (contain_data != null) {
            for (int i = 0; i < contain_data.Count; i++)
            {
                for (int n = 0; n < contain_data[i].Value; n++)
                {
                    Block_Objectpooling.instance.Get(contain_data[i].Key, transform.position);
                }
            }
        }        
    }
}

