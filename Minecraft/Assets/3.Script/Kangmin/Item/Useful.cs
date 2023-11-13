using UnityEngine;

public interface IUseFul
{
    void R_None();//왼쪽 마우스
    void L_Swing(); //오른쪽 마우스
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
    public int Durability; //내구성 - 칠때마다 떨어짐
    public int attack; //공격력
    public int att_speed; //공격 속도
    public PlayerState_Y player_state;
    public Animator player_animator;
    public Useful_type useful_Type;
    public string sub_info
    {
        get
        {
            string tmp = $"주로 사용하는 손에 있을 때:\n+{attack} 공격 피해\n+{att_speed} 공격 속도";
            return tmp;
        }
    }

    public void R_None() //오른쪽 - X
    {
       
    }

    public void L_Swing() // 왼쪽 - 휘두르기
    {
        //정보 없을 때 할당
        if (player_state == null || player_animator == null)
        {
            player_state = FindObjectOfType<PlayerState_Y>();
            player_animator = player_state.gameObject.GetComponent<Animator>();
            Debug.Log($"{player_state.gameObject.name}");

            //animation
        }
    }

}
