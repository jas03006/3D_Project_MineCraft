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
            string tmp1 = " ";
            switch (type)
            {
                case armor_type.helmet:
                    tmp1 = "�Ӹ�";
                    break;
                case armor_type.armor:
                    tmp1 = "����";
                    break;
                case armor_type.leggings:
                    tmp1 = "�ٸ�";
                    break;
                case armor_type.boots:
                    tmp1 = "�Ź�";
                    break;
            }
            string tmp2 = $"�� ���� ��:\n+{defense_power} ����";
            return tmp1 + tmp2;
        }
    }
    public void defense_up()
    {
        //���� ���� �� �Ҵ�
        if (player_state == null || player_animator == null)
        {
            player_state = FindObjectOfType<PlayerState_Y>();
            player_animator = player_state.gameObject.GetComponent<Animator>();
        }
        player_state.defense_power += defense_power;
    }

    public void defense_down()
    {   
        //���� ���� �� �Ҵ�
        if (player_state == null || player_animator == null )
        {
            player_state = FindObjectOfType<PlayerState_Y>();
            player_animator = player_state.gameObject.GetComponent<Animator>();
        }
        player_state.defense_power -= defense_power;
    }
}