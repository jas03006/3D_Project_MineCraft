using UnityEngine;

public interface IWear
{
    void defense_up();
    void defense_down();
}

public enum armor_type
{
    helmet = 0,
    armor,
    leggings,
    boots
}

[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Useful", order = int.MaxValue)]
public class Wear : InventoryData, IWear
{
    public armor_type type;
    public int defense_power;
    public PlayerState_Y player_state;
    public Animator player_animator;
    public Slot_Y slot;
    public void defense_up()
    {
        //정보 없을 때 할당
        if (player_state == null || player_animator == null || slot == null)
        {
            player_state = FindObjectOfType<PlayerState_Y>();
            player_animator = player_state.gameObject.GetComponent<Animator>();
            slot = Inventory.instance.playerItemList[27 + Inventory.instance.UIslot_index];
            Debug.Log($"{player_state.gameObject.name} : {slot.name}");
        }
    }

    public void defense_down()
    {
    
    }
}