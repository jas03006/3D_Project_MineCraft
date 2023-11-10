using UnityEngine;

public interface IEat
{
    void L_Eat();//���� ���콺
    void R_None(); //������ ���콺
}

[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Eat", order = int.MaxValue)]
public class Food : InventoryData, IEat
{
    //slot�� ������ �Ҵ��Ű��??
    public int hungry_cure;
    //������ ����
    public PlayerState_Y player_state;
    public Animator player_animator;
    public Slot_Y slot;


    public void L_Eat() //���� - �Ա�
    {
        //�÷��̾� ���� �Ҵ�
        if (player_state == null && player_animator == null && slot == null)
        {
            player_state = FindObjectOfType<PlayerState_Y>();
            player_animator = player_state.gameObject.GetComponent<Animator>();
           // slot = Inventory.instance.playerItemList[27+ Inventory.instance.UIslot_index]
        }

        //ü�� ȸ�� ����
        player_state.Hungry_cure(hungry_cure);

        slot.number--;        //������ ����--;
        if (slot.number == 0)
        {
            slot.ResetItem();        //������ ���� 0�̸� �ʱ�ȭ
        }

        //player_animator.set �ִϸ��̼� �־������
    }

    public void R_None()
    {
        return;
    }
}
