using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Y : MonoBehaviour
{
    [SerializeField] public Item_ID_TG[] item_id_list;
    #region 내용
    //[SerializeField] public Item_ID_TG item_id_1;
    //[SerializeField] public Item_ID_TG item_id_2;
    //[SerializeField] public Item_ID_TG item_result;
    #endregion

    [SerializeField] public int[] num_list;
    #region 내용
    //[SerializeField] public int num_1;
    //[SerializeField] public int num_2;
    //[SerializeField] public int num_result;
    #endregion

    [SerializeField] public Text[] text_list;
    #region 내용
    //[SerializeField] public Text text_1;
    //[SerializeField] public Text text_2;
    //[SerializeField] public Text text_result;
    #endregion

    [SerializeField] Image[] image_list;
    #region 내용
    //[SerializeField] Image image_1;
    //[SerializeField] Image image_2;
    //[SerializeField] Image image_result;
    #endregion

    [SerializeField] ID2Datalist_YG id2data;
    [SerializeField] NPC_UI_Y npc;

    public void update_Button()
    {
        for (int i = 0; i < item_id_list.Length; i++)
        {
            //image 교체
            InventoryData data = id2data.Get_data(item_id_list[i]);
            image_list[i].sprite = data.Itemsprite;

            //none이면 투명도 변경
            if (item_id_list[i] == Item_ID_TG.None)
            {
                Color color = image_list[i].color;
                color.a = 0;
                image_list[i].color = color;
            }
            else
            {
                Color color = image_list[i].color;
                color.a = 1;
                image_list[i].color = color;
            }

            //text교체
            text_list[i].text = $"{num_list[i]}";
            if (num_list[i] == 0)
            {
                text_list[i].enabled = false;
            }
            else
            {
                text_list[i].enabled = true;
            }
            
        }
    }

    public void click_button()
    {
        npc.selected_button = this;
    }

    public void get_recipe(NPC_recipe_Y recipe)
    {
        Debug.Log(recipe);

        //id 받아오기
        for (int i = 0; i < item_id_list.Length; i++)
        {
            item_id_list[i] = recipe.item_id_list[i];
            Debug.Log($"{item_id_list[i]}");
        }

        //개수 받아오기
        for (int i = 0; i < num_list.Length; i++)
        {
            num_list[i] = recipe.num_list[i];
        }
    }

    public void reset_recipe()
    {
        for (int i = 0; i < item_id_list.Length; i++)
        {
            item_id_list[i] = Item_ID_TG.None;
        }
    }

}
