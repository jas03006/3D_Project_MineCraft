using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_YG : MonoBehaviour
{
    public Item_ID_TG id;
    public InventoryData data;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = data.itemSprite;
    }

    public virtual void Interaction_YG()
    {
        
    }
}
