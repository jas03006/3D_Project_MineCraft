using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace_Y : Box_Y
{
    public bool is_on=false;
    [SerializeField] public Slider fire_slider;
    [SerializeField] public GameObject fire_image;
    public Furnace_TG furnace_tg;
    // Start is called before the first frame update
    private void Update()
    {
        
        if (is_on) {
            fire();
        }
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
    private void start_fire() {
        is_on = true;
        fire_image.SetActive(true);
    }
    private void stop_fire()
    {
        is_on = false;
        fire_image.SetActive(false);
    }
    private void fire()
    {
        fire_slider.value = fire_slider.value + Time.deltaTime;
        if (fire_slider.value >= fire_slider.maxValue) {
            fire_slider.value = 0;
        }
    }
}
