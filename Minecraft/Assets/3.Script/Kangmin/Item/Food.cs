using UnityEngine;

public interface IEat
{
    void R_Eat();//왼쪽 마우스
    void L_None(); //오른쪽 마우스
}

[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Food", order = int.MaxValue)]
public class Food : InventoryData, IEat
{
    //slot에 데이터 할당시키기??
    public int hungry_cure;
    public PlayerState_Y player_state;
    public Animator player_animator;
    public Slot_Y slot;


    public void R_Eat() //오른쪽 - 먹기
    {
        //정보 없을 때 할당
        if (player_state == null || player_animator == null || slot == null)
        {
            player_state = FindObjectOfType<PlayerState_Y>();
            player_animator = player_state.gameObject.GetComponent<Animator>();
            slot = Inventory.instance.playerItemList[27 + Inventory.instance.UIslot_index];
            Debug.Log($"{player_state.gameObject.name} : {slot.name}");
        }

        //체력 회복 적용
        player_state.Hungry_cure(hungry_cure);

        slot.number--;        //아이템 갯수--;

        if (slot.number == 0)
        {
            slot.ResetItem();        //아이템 갯수 0이면 초기화
        }
        //player_animator.set 애니메이션 넣어줘야함
    }

    public void L_None() //왼쪽 - 없음
    {
        return;
    }
}
