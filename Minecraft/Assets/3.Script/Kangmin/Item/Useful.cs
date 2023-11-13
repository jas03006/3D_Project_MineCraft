using UnityEngine;

public interface IUseFul
{
    void R_None();//���� ���콺
    void L_Swing(); //������ ���콺
}
public enum Useful_type
{
    axe=0,
    sword,
    pickaxe
}

[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Useful", order = int.MaxValue)]
public class Useful : InventoryData, IUseFul
{
    public int Durability; //������ - ĥ������ ������
    public int attack; //���ݷ�
    public int att_speed; //���� �ӵ�
    public PlayerState_Y player_state;
    public Animator player_animator;
    public Useful_type useful_Type;
    public string sub_info
    {
        get
        {
            string tmp = $"�ַ� ����ϴ� �տ� ���� ��:\n+{attack} ���� ����\n+{att_speed} ���� �ӵ�";
            return tmp;
        }
    }

    public void R_None() //������ - X
    {
       
    }

    public void L_Swing() // ���� - �ֵθ���
    {
        //���� ���� �� �Ҵ�
        if (player_state == null || player_animator == null)
        {
            player_state = FindObjectOfType<PlayerState_Y>();
            player_animator = player_state.gameObject.GetComponent<Animator>();
            Debug.Log($"{player_state.gameObject.name}");

            //animation
        }
    }

}
