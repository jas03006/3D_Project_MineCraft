using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace_TG : Block_TG, Interactive_TG
{
    public bool is_canvas_open = false;
    public GameObject fire_face;
    //public int fuel_count = 0;
    private void OnEnable()
    {
        is_canvas_open = false;
        is_open = false;   // 화로가 구울 수 있는 상황인지     
        if (contain_data == null || contain_data.Count == 0) {
            contain_data = new List<KeyValuePair<Item_ID_TG, int>>(); // 0: 재료, 1: 연료, 2: 결과물
            for (int i =0; i< 3; i++) {
                contain_data.Add(new KeyValuePair<Item_ID_TG, int>(Item_ID_TG.None, 0));
            }
        }

        if (time_data == null || time_data.Count ==0) {
            time_data = new List<float>(); // 0: 필요시간, 1: 현재 진행시간, 2: 현재시간, 3: fuel count (int 처럼 활용)
            for (int i = 0; i < 4; i++)
            {
                time_data.Add(0f);
            }
        }               
    }

    private void Update()
    {
        if (( is_canvas_open && is_open) || (Time.time - time_data[2] < time_data[0]) )
        {
            if (!fire_face.activeSelf) {
                fire_face.SetActive(true);
            }            
        }
        else {
            if (fire_face.activeSelf)
            {
                fire_face.SetActive(false);
            }
        }
    }

    public void react()
    {
        Debug.Log(contain_data[0].Key);
        Debug.Log(contain_data[1].Key);
        Debug.Log(contain_data[2].Key);
        cal_furnace_result();
        is_canvas_open = true;
        Action<List<Slot_Y>> callback = close;
        Inventory.instance.show_furnace(this, contain_data, time_data, callback);
    }
    public void close(List<Slot_Y> data)
    {
        contain_data.Clear();
        for (int i = 0; i < data.Count; i++)
        {
            //Debug.Log($"{i}: {data[i].item_id}");
            contain_data.Add(new KeyValuePair<Item_ID_TG, int>(data[i].item_id, data[i].number));            
        }
        save_furnace_start();
        is_canvas_open = false;
    }
    public override void init(bool is_open_, List<KeyValuePair<Item_ID_TG, int>> contain_data_,List<float> time_data_)
    {
        contain_data = contain_data_;
        is_open = is_open_;
        time_data = time_data_;
        is_canvas_open = false;
        //cal_furnace_result();
    }
    public override void drop_items()
    {
        cal_furnace_result();
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
    public void save_furnace_start()// 캔버스가 닫힐 때 현재 시간과 필요시간 등 정보를 저장
        // 진행시간과 시작시간은 furnace_y에서 저장해준 상태
    {
        if (!is_open)
        {
            time_data[0] = 0f;
        }
        else {
            time_data[0] = Mathf.Min(contain_data[0].Value , contain_data[1].Value * Furnace_System_TG.instance.get_fuel_energy(contain_data[1].Key) + time_data[3]) * Furnace_System_TG.instance.unit_time - time_data[1]; 
        }
    }
    public void cal_furnace_result() { // 캔버스를 열거나 부숴지거나 새로 렌더링 될 때, 화로 작업 결과를 계산
        if (!is_open) {
            time_data[1] = 0;
            return;
        }
        float unit_time = Furnace_System_TG.instance.unit_time;
        int fuel_energy = Furnace_System_TG.instance.get_fuel_energy(contain_data[1].Key);
        float burn_time = Mathf.Min(time_data[0], Time.time - time_data[2] - (unit_time - time_data[1]));//현재 굽는 재료를 다 태운 이후의 시간 (음수 가능)
        
        int cnt = (int)(burn_time / unit_time) + 1;

        if (burn_time < 0)
        {
            cnt = 0;
            time_data[1] = time_data[1] + Time.time - time_data[2];
        }
        else {
            time_data[1] = burn_time - ((int)(burn_time / unit_time)) * unit_time;
        }
        Item_ID_TG temp_id;
        if (cnt > 0)
        {
            temp_id = contain_data[2].Key;
            if (temp_id == Item_ID_TG.None)
            {
                temp_id = Furnace_System_TG.instance.get_furnace_result(contain_data[0].Key);
            }
            contain_data[2] = new KeyValuePair<Item_ID_TG, int>(temp_id, contain_data[2].Value + cnt);
        }

        temp_id = contain_data[0].Key;
        if (contain_data[0].Value - cnt ==0) {
            temp_id = Item_ID_TG.None;
        }
        contain_data[0] = new KeyValuePair<Item_ID_TG, int>(temp_id, contain_data[0].Value - cnt);

        temp_id= contain_data[1].Key;
        if (cnt > time_data[3])
        {
            int fuel_use_cnt = (int)((cnt - time_data[3]) / fuel_energy);
            fuel_use_cnt += (fuel_use_cnt == ((cnt - time_data[3]) / fuel_energy) ? 0 : 1);
            if (contain_data[1].Value == fuel_use_cnt)
            {
                temp_id = Item_ID_TG.None;
            }
            contain_data[1] = new KeyValuePair<Item_ID_TG, int>(temp_id, contain_data[1].Value - fuel_use_cnt);
            time_data[3] = time_data[3]+ fuel_use_cnt * fuel_energy - cnt;
        }
        else {
            time_data[3] -= cnt;
        }

        
                
    }
}
