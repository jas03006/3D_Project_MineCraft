using UnityEngine;

public interface IUseFul
{
    void R_None();//���� ���콺
    void L_Swing(); //������ ���콺
}

[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Useful", order = int.MaxValue)]
public class Useful : InventoryData, IUseFul
{
    public int Durability; //������ - ĥ������ ������
    public int attack; //���ݷ�
    public PlayerState_Y player_state;
    public Animator player_animator;
    public Slot_Y slot;

    public void R_None() //������ - X
    {
       
    }

    public void L_Swing() // ���� - �ֵθ���
    {
        //���� ���� �� �Ҵ�
        if (player_state == null || player_animator == null || slot == null)
        {
            player_state = FindObjectOfType<PlayerState_Y>();
            player_animator = player_state.gameObject.GetComponent<Animator>();
            Debug.Log($"{player_state.gameObject.name} : {slot.name}");

            //animation
        }
    }

}
