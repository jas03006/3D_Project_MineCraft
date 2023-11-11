using UnityEngine;

public interface IUseFul
{
    void R_None();//왼쪽 마우스
    void L_Swing(); //오른쪽 마우스
}

[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Useful", order = int.MaxValue)]
public class Useful : InventoryData, IUseFul
{
    public int Durability; //내구성 - 칠때마다 떨어짐
    public int attack; //공격력
    public PlayerState_Y player_state;
    public Animator player_animator;
    public Slot_Y slot;

    public void R_None() //오른쪽 - X
    {
       
    }

    public void L_Swing() // 왼쪽 - 휘두르기
    {
        //정보 없을 때 할당
        if (player_state == null || player_animator == null || slot == null)
        {
            player_state = FindObjectOfType<PlayerState_Y>();
            player_animator = player_state.gameObject.GetComponent<Animator>();
            Debug.Log($"{player_state.gameObject.name} : {slot.name}");

            //animation
        }
    }

}
