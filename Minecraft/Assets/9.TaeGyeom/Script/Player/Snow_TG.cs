using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow_TG : MonoBehaviour
{
    public static Snow_TG instance = null;
    [SerializeField] GameObject snow_particle_prefab;
    public List<Snow_Particle_System_TG> snow_system_list;
    public float width = 80f;
    public float height = 80f;
    private float width_unit;
    private float height_unit;
    public float width_cnt = 4;
    public float height_cnt = 4;

    public int max_particle_cnt = 2400;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (width_cnt > 16) {
            width_cnt = 16;
        }
        if (height_cnt > 16)
        {
            height_cnt = 16;
        }
        width_unit = width / width_cnt;
        height_unit = height / height_cnt;
        int max_cnt_per_unit = max_particle_cnt / (int)width_cnt / (int)height_cnt;
        Vector3 start_pos = new Vector3(-width_unit * (width_cnt/2 - 0.5f),0, -height_unit * (height_cnt / 2 - 0.5f));
        for (int i =0; i < width_cnt; i++) {
            for (int j = 0; j < height_cnt; j++)
            {
                GameObject go = Instantiate(snow_particle_prefab, Vector3.zero,Quaternion.identity);
                go.transform.SetParent(this.transform);
                go.transform.localPosition = start_pos + Vector3.right * width_unit * i + Vector3.forward * height_unit * j;
                Snow_Particle_System_TG sps = go.GetComponent<Snow_Particle_System_TG>();
                snow_system_list.Add(sps);
                sps.set_size(width_unit, height_unit, max_cnt_per_unit);
            }
        }   
    }

    public void reset_snows() {
        for (int i =0; i < snow_system_list.Count; i++) {
            snow_system_list[i].reset_cnt();
        }
    }
}
