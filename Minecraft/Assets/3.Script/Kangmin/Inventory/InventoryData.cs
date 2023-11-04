using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Inventory Data", order = int.MaxValue)]

public class InventoryData : ScriptableObject
{
    [SerializeField] private string itemName; // ������ �̸�
    public string ItemName => itemName;

    [SerializeField] private int maxValue; // �ִ밹��
    public int MaxValue => maxValue;

    [SerializeField] public Sprite itemSprite; //������ 2d sprite

    [SerializeField] private bool isEquipable; // ���� ���� ����
    public bool IsEquipable => isEquipable;

    [SerializeField] private bool isInstallable; // ��ġ ���� ����
    public bool IsInstallable => isInstallable;

    [SerializeField] private bool isWeapon; // ���� ����
    public bool IsWeapon => isWeapon;

    [SerializeField] private bool isArmor; // �� ����
    public bool IsArmor => isArmor;

    [SerializeField] private bool isInteractable; // ��ġ �� ��ȣ�ۿ밡�� ����
    public bool IsInteractable => isInteractable;

    [SerializeField] private bool isCombinable; // ��ġ�� ����
    public bool IsCombinable => isCombinable;

}
