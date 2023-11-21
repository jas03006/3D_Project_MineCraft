using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Y : MonoBehaviour
{
    [SerializeField] public int recipe_index;
    [SerializeField] public Item_ID_TG item_id_1;
    [SerializeField] public Item_ID_TG item_id_2;
    [SerializeField] public Item_ID_TG item_result;

    [SerializeField] public int num_1;
    [SerializeField] public int num_2;
    [SerializeField] public int num_result;

    [SerializeField] ID2Datalist_YG id2data;
    [SerializeField] Image image_1;
    [SerializeField] Image image_2;
    [SerializeField] Image image_result;

    [SerializeField] NPC_Y npc;
    [SerializeField] recipe2NPC_Y recipe2NPC;

    private void OnEnable()
    {
        //get_recipe(recipe2NPC_Y.get_recipe(recipe_index));
        image_1.sprite = id2data.Get_data(item_id_1).Itemsprite;
        image_2.sprite = id2data.Get_data(item_id_2).Itemsprite;
        image_result.sprite = id2data.Get_data(item_result).Itemsprite;
    }

    public void click_button()
    {
        //npc.button = this;
    }

    public void get_recipe(NPC_recipe_Y recipe)
    {
        item_id_1 = recipe.item_id_1;
        item_id_2 = recipe.item_id_2;
        item_result = recipe.item_result;
    }

    public void reset_recipe()
    {
        item_id_1 = Item_ID_TG.None;
        item_id_2 = Item_ID_TG.None;
        item_result = Item_ID_TG.None;
    }
}
