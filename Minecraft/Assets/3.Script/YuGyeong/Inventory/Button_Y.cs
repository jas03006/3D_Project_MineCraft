using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Y : MonoBehaviour
{
    [SerializeField] public Item_ID_TG item_id_1;
    [SerializeField] public Item_ID_TG item_id_2;
    [SerializeField] public Item_ID_TG item_result;

    [SerializeField] public int num_1;
    [SerializeField] public int num_2;
    [SerializeField] public int num_result;

    [SerializeField] public Text text_1;
    [SerializeField] public Text text_2;
    [SerializeField] public Text text_result;

    [SerializeField] ID2Datalist_YG id2data;
    [SerializeField] Image image_1;
    [SerializeField] Image image_2;
    [SerializeField] Image image_result;

    [SerializeField] NPC_Y npc;
    //[SerializeField] recipe2NPC_Y recipe2NPC;

    private void OnEnable()
    {
        //get_recipe(recipe2NPC_Y.get_recipe(recipe_index));
    }
    public void update_Button()
    {
        //image
        image_1.sprite = id2data.Get_data(item_id_1).Itemsprite;
        image_2.sprite = id2data.Get_data(item_id_2).Itemsprite;
        image_result.sprite = id2data.Get_data(item_result).Itemsprite;

        //text
        text_1.text = $"{num_1}";
        text_2.text = $"{num_2}";
        text_1.text = $"{num_result}";
    }

    public void click_button()
    {
        npc.selected_button = this;
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

    //public void get_index(int index)
    //{
    //    recipe_index = index;
    //}
}
