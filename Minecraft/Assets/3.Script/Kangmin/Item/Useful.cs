using UnityEngine;

public interface IUseFul
{
    void L_None();//왼쪽 마우스
    void R_Swing(); //오른쪽 마우스
}

[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Useful", order = int.MaxValue)]
public class Useful : InventoryData, IUseFul
{
    public int Durability; //내구성 - 칠때마다 떨어짐
    public int attack; //공격력
    public PlayerState_Y player;

    public void L_None() //왼쪽 - X
    {
       
    }

    public void R_Swing() // 오른쪽 - 휘두르기
    {

    }
}
