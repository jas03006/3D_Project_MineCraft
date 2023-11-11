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
        contain_data = new List<KeyValuePair<Item_ID_TG, int>>(); // 0: ���, 1: ����, 2: �����
        time_data = new List<float>(); // 0: �ʿ�ð�, 1: ���� ����ð�, 2: ����ð�
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
            //���� �ð� ����ؼ� ���� ����
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
