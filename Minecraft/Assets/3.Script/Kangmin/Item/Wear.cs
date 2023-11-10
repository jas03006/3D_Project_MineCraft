using UnityEngine;

//public interface IWear
//{

//}

[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Useful", order = int.MaxValue)]
public class Wear : InventoryData//, IWear
{
    public int defense_power = 5;
    public PlayerState_Y player_state;
    public Animator player_animator;
    public Slot_Y slot;
}