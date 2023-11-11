using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace_Y : Box_Y
{   // 0: 재료, 1: 연료, 2: 결과물
    public bool is_on=false;
    [SerializeField] public Slider fire_slider;
    [SerializeField] public GameObject fire_image;
    public Furnace_TG furnace_tg;
    private int fuel_count = 0;
    // Start is called before the first frame update
    private void Update()
    {
        is_on = check_is_on();
        if (is_on) {
            fire();
        }
    }

    private bool check_is_on() {
        if (Furnace_System_TG.instance.is_ingredient(slots[0].item_id) 
            && (Furnace_System_TG.instance.is_fuel(slots[1].item_id) || fuel_count > 0) 
            && (slots[2].item_id ==  Item_ID_TG.None || slots[2].item_id == Furnace_System_TG.instance.get_furnace_result(slots[0].item_id))) {
            if (is_on == false) {
                Debug.Log(Furnace_System_TG.instance.get_furnace_result(slots[0].item_id));
                fire_image.SetActive(true);
                furnace_tg.is_open = true;
                if (fuel_count <= 0) {
                    update_fuel_slot();
                }                
            }
            return true;
        }
        if (is_on == true || fire_slider.value>0)
        {
            fire_slider.value = 0;
            fire_image.SetActive(false);
            furnace_tg.is_open = false;
        }
        return false;
    }

    public void set_furnace() {
        /*if (furnace_tg != null && slots != null)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                furnace_tg.contain_data.Add();
            }
        }*/
    }

    public void Get_data(List<KeyValuePair<Item_ID_TG, int>> data, List<float> time_data)
    {
        fire_slider.maxValue = Furnace_System_TG.instance.unit_time;
        for (int i = 0; i < data.Count; i++)
        {
            slots[i].ResetItem();
            slots[i].GetItem(data[i].Key, data[i].Value);
        }
        fire_slider.value = time_data[1];
        fuel_count = (int)time_data[3];
        is_on = false;
    }
    private void fire()
    {
        fire_slider.value = fire_slider.value + Time.deltaTime;        
        if (fire_slider.value >= fire_slider.maxValue) {
            fire_slider.value = 0;
            
            slots[0].number--;            
            slots[2].GetItem(Furnace_System_TG.instance.get_furnace_result(slots[0].item_id), slots[2].number+1);
            if (slots[0].number <= 0)
            {
                slots[0].ResetItem();
            }
            fuel_count--;
            if (fuel_count == 0)
            {
                update_fuel_slot();
            }

        }
        furnace_tg.time_data[2] = Time.time;
        furnace_tg.time_data[1] = fire_slider.value;
        furnace_tg.time_data[3] = fuel_count;
    }

    private void update_fuel_slot() {
        fuel_count = Furnace_System_TG.instance.get_fuel_energy(slots[1].item_id);
        slots[1].number--;
        if (slots[1].number <= 0)
        {
            slots[1].ResetItem();
        }        
    }
    
}
