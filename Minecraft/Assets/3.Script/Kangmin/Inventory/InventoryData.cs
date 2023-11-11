using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Inventory Data", menuName = "Scriptable Object/Inventory Data", order = int.MaxValue)]
public class InventoryData : ScriptableObject
{
    [SerializeField] private string itemName; // ������ �̸�
    public string ItemName => itemName;

    [SerializeField] private int maxValue; // �ִ밹��
    public int MaxValue => maxValue;

    [SerializeField] private Sprite itemsprite; //������ 2d sprite
    public Sprite Itemsprite => itemsprite;

    [SerializeField] private string classname;
    public string Classname  => classname;

    /*----------------------------------------------------------------------*/
    [SerializeField] private bool isequipable; // ���� ���� ����
    public bool IsEquipable => isequipable;

    [SerializeField] private bool isinstallable; // ��ġ ���� ����
    public bool Isinstallable => isinstallable;

    [SerializeField] private bool isinteractable; // ��ġ �� ��ȣ�ۿ밡�� ����
    public bool Isinteractable => isinteractable;

    [SerializeField] private bool iseatable;
    public bool Iseatable => iseatable;

    [SerializeField] private bool isuseful;
    public bool Isuseful => isuseful;

    [SerializeField] private int fuel_efficiency;
    public int Fuel_efficiency => fuel_efficiency;

    [SerializeField] Item_ID_TG baked_item;
    public Item_ID_TG Baked_item => baked_item; 

}
