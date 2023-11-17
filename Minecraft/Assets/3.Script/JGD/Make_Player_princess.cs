using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Make_Player_princess : MonoBehaviour
{
    public static Make_Player_princess instance = null;

    [SerializeField] private GameObject Dia_Helmat;
    [SerializeField] private GameObject Boot_Diamond_L;
    [SerializeField] private GameObject Boot_Diamond_R;
    [SerializeField] private GameObject Chestplate_Diamond_Arm_L;
    [SerializeField] private GameObject Chestplate_Diamond_Arm_R;
    [SerializeField] private GameObject Chestplate_Iron_Arm_L;
    [SerializeField] private GameObject Chestplate_Iron_Arm_R;
    [SerializeField] private GameObject Dia_Armer;
    [SerializeField] private GameObject Helmet_Iron;
    [SerializeField] private GameObject Iron_Armor;
    [SerializeField] private GameObject Iron_025;
    [SerializeField] private GameObject Iron_026;
    [SerializeField] private GameObject Iron_027;
    [SerializeField] private GameObject Iron_031;
    [SerializeField] private GameObject Leg_Diamond_L;
    [SerializeField] private GameObject Leg_Diamond_R;
    [SerializeField] private GameObject Waist_Diamond;
    [SerializeField] private GameObject Waist_Iron;

    

    public Item_ID_TG[] id = new Item_ID_TG[4] {Item_ID_TG.None, Item_ID_TG.None, Item_ID_TG.None, Item_ID_TG.None }; // [Çï¸ä, °©¿Ê , ¹ÙÁö , ½Å¹ß ] 

    /*private void Update()
    {
        Player_Makeup();  //Å×½ºÆ®¿ë
    }*/

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public void update_equipment(armor_type a_type, Item_ID_TG id_) {
        id[(int)a_type] = id_;
        Player_Makeup();
    }

    public void Player_Makeup()
    {
        switch (id[0])
        { 
            case Item_ID_TG.iron_helmet:
                Helmet_Iron.SetActive(true);
                break;
            case Item_ID_TG.diamond_helmet:
                Dia_Helmat.SetActive(true);
                break;
            default:
                Dia_Helmat.SetActive(false);
                Helmet_Iron.SetActive(false);
                break;

        }
        switch (id[1])
        {
            case Item_ID_TG.iron_armor:
                Chestplate_Iron_Arm_L.SetActive(true);
                Chestplate_Iron_Arm_R.SetActive(true);
                Iron_Armor.SetActive(true);
                break;
            case Item_ID_TG.diamond_armor:
                Chestplate_Diamond_Arm_L.SetActive(true);
                Chestplate_Diamond_Arm_R.SetActive(true);
                Dia_Armer.SetActive(true);
                break;
            default:
                Chestplate_Diamond_Arm_L.SetActive(false);
                Chestplate_Diamond_Arm_R.SetActive(false);
                Dia_Armer.SetActive(false);
                Chestplate_Iron_Arm_L.SetActive(false);
                Chestplate_Iron_Arm_R.SetActive(false);
                Iron_Armor.SetActive(false);
                break;
        }
        switch (id[2])
        {
            case Item_ID_TG.iron_leggings:
                Iron_027.SetActive(true);
                Iron_025.SetActive(true);
                Waist_Iron.SetActive(true);
                break;
            case Item_ID_TG.diamond_leggings:
                Leg_Diamond_L.SetActive(true);
                Leg_Diamond_R.SetActive(true);
                Waist_Diamond.SetActive(true);
                break;
            default:
                Iron_027.SetActive(false);
                Iron_025.SetActive(false);
                Waist_Iron.SetActive(false);
                Leg_Diamond_L.SetActive(false);
                Leg_Diamond_R.SetActive(false);
                Waist_Diamond.SetActive(false);
                break;
        }
        switch (id[3])
        {
            case Item_ID_TG.iron_boots:
                Iron_031.SetActive(true);
                Iron_026.SetActive(true);
                break;
            case Item_ID_TG.diamond_boots:
                Boot_Diamond_L.SetActive(true);
                Boot_Diamond_R.SetActive(true);
                break;
            default:
                Iron_031.SetActive(false);
                Iron_026.SetActive(false);
                Boot_Diamond_L.SetActive(false);
                Boot_Diamond_R.SetActive(false);
                break;
        }
    }








}
