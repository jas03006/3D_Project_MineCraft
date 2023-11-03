using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Inventory Data", order = int.MaxValue)]

public class InventoryData : ScriptableObject
{
    [SerializeField] private int itemCode; // 아이템 코드
    public int ItemCode => itemCode;

    [SerializeField] private string itemName; // 아이템 이름
    public string ItemName => itemName;

    [SerializeField] private int value; // 갯수
    public int Value => value;

    public Image itemImage; // 아이템 이미지

    [SerializeField] private bool isEquipable; // 장착 가능 여부
    public bool IsEquipable => isEquipable;

    [SerializeField] private bool isInstallable; // 설치 가능 여부
    public bool IsInstallable => isInstallable;

    [SerializeField] private bool isWeapon; // 무기 여부
    public bool IsWeapon => isWeapon;

    [SerializeField] private bool isArmor; // 방어구 여부
    public bool IsArmor => isArmor;

    [SerializeField] private bool isInteractable; // 설치 시 상호작용가능 여부
    public bool IsInteractable => isInteractable;

    [SerializeField] private bool isCombinable; // 합치기 여부
    public bool IsCombinable => isCombinable;
}
