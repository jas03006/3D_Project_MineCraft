using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot_Y : MonoBehaviour
{
    public Item_ID_TG item_id; //������ id
    public int value; //����
    public ID2Datalist_YG id2data; //id -> ������ ����
    public Image image;
    public Image highlight;
    public bool is_active;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        highlight = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_active)
        {
            highlight.enabled = true;
        }
    }
}
