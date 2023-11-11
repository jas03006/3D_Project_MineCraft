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

[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Wear", order = int.MaxValue)]
public class Wear : InventoryData, IWear
{
    public armor_type type;
    public int defense_power;
    public PlayerState_Y player_state;
    public Animator player_animator;
    public string sub_info
    {
        get
        {
            string tmp0 = "<color =#858282>";
            string tmp1 = " ";
            switch (type)
            {
                case armor_type.helmet:
                    tmp1 = "머리";
                    break;
                case armor_type.armor:
                    tmp1 = "몸통";
                    break;
                case armor_type.leggings:
                    tmp1 = "다리";
                    break;
                case armor_type.boots:
                    tmp1 = "신발";
                    break;
            }
            string tmp2 = $"에 있을 때:</Color>\n<color=0069FF>+{defense_power} 방어력</Color>";
            return tmp0 + tmp1 + tmp2;
        }
    }
    public void defense_up()
    {
        //정보 없을 때 할당
        if (player_state == null || player_animator == null)
        {
            player_state = FindObjectOfType<PlayerState_Y>();
            player_animator = player_state.gameObject.GetComponent<Animator>();
        }
        player_state.defense_power += defense_power;
    }

    public void defense_down()
    {   
        //정보 없을 때 할당
        if (player_state == null || player_animator == null )
        {
            player_state = FindObjectOfType<PlayerState_Y>();
            player_animator = player_state.gameObject.GetComponent<Animator>();
        }
        player_state.defense_power -= defense_power;
    }
}