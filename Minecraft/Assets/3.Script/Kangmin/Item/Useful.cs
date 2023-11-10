using UnityEngine;

public interface IUseFul
{
    void L_None();//���� ���콺
    void R_Swing(); //������ ���콺
}

[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Useful", order = int.MaxValue)]
public class Useful : InventoryData, IUseFul
{
    public int Durability; //������ - ĥ������ ������
    public int attack; //���ݷ�
    public PlayerState_Y player;

    public void L_None() //���� - X
    {
       
    }

    public void R_Swing() // ������ - �ֵθ���
    {

    }
}
