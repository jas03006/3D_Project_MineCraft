using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace_TG : Block_TG, Interactive_TG
{
    public bool is_on;
    private void OnEnable()
    {
        is_open = false;        
        contain_data = new List<KeyValuePair<Item_ID_TG, int>>(); // 0: 재료, 1: 연료, 2: 결과물
        time_data = new List<float>(); // 0: 필요시간, 1: 현재 진행시간, 2: 현재시간
    }

    private void Update()
    {
         
    }

    public void react()
    {
        Action<List<Slot_Y>> callback = close;
        Inventory.instance.show_furnace(this, contain_data, callback);
    }
    public void close(List<Slot_Y> data)
    {
        contain_data.Clear();
        for (int i = 0; i < data.Count; i++)
        {
            //Debug.Log($"{i}: {data[i].item_id}");
            contain_data.Add(new KeyValuePair<Item_ID_TG, int>(data[i].item_id, data[i].number));
        }
    }
    public override void init(bool is_open_, List<KeyValuePair<Item_ID_TG, int>> contain_data_,List<float> time_data_)
    {
        contain_data = contain_data_;
        is_open = is_open_;
        time_data = time_data_;
        if (is_open) { 
            //지난 시간 계산해서 갯수 조정
        }
    }
    public override void drop_items()
    {
        Inventory.instance.hide();
        if (contain_data != null)
        {
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
