using System.Collections.Generic;
using UnityEngine;

public class Combat_system : MonoBehaviour
{
    public static Combat_system instance = null;

    [SerializeField]private List<Item_ID_TG> axe_list_item
        = new List<Item_ID_TG>() {
            Item_ID_TG.tree,
            Item_ID_TG.box,
            Item_ID_TG.craft_box,
            Item_ID_TG.board,
            Item_ID_TG.bed,
        };
    [SerializeField] private List<Item_ID_TG> pickaxe_list
        = new List<Item_ID_TG>() {
            Item_ID_TG.diamond,
            Item_ID_TG.coal,
            Item_ID_TG.iron,
            Item_ID_TG.stone,
            Item_ID_TG.furnace,
        };
    [SerializeField] private List<Monster_ID_J> monster_list
        = new List<Monster_ID_J>() {
            Monster_ID_J.Pig,
            Monster_ID_J.zombie,
        };

    private PlayerState_Y psy;
    private ID2Datalist_YG id2data;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    private void Start()
    {
        psy = FindObjectOfType<PlayerState_Y>();
    }

    public int cal_combat_block(Item_ID_TG target_id)
    {
        //Useful?
        Item_ID_TG player_item = Inventory.instance.UIslot_Item();
        InventoryData player_data = id2data.Get_data(player_item);
        if (player_data is Useful)
        {
            Useful useful = player_data as Useful;

            //list contain targetID?
            switch (useful.useful_Type)
            {
                case Useful_type.pickaxe:
                    return pickaxe_list.Contains(target_id) ? psy.attack_power : useful.attack + psy.attack_power;
                case Useful_type.axe:
                    return axe_list_item.Contains(target_id) ? psy.attack_power : useful.attack + psy.attack_power;
                default:
                    return psy.attack_power;
            }
        }
        return psy.attack_power;
    }
    public int cal_combat_monster(Monster_ID_J target_id)
    {
        //Useful?
        Item_ID_TG player_item = Inventory.instance.UIslot_Item();
        InventoryData player_data = id2data.Get_data(player_item);
        if (player_data is Useful)
        {
            //list contain targetID?
            Useful useful = player_data as Useful;
            if (useful.useful_Type == Useful_type.sword || useful.useful_Type == Useful_type.axe)
            {
                return monster_list.Contains(target_id) ? psy.attack_power : useful.attack + psy.attack_power;
            }
        }
        return psy.attack_power;
    }
}
