using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Creat_Recipe",order = 6)]
public class Creat_Recipe : ScriptableObject
{
    public Item_ID_TG id;
    Item_ID_TG[,] Maker = new Item_ID_TG[3, 3];

    [Header("1  ¼¼·Î·Î")]
    [SerializeField] private Item_ID_TG tG1;
    [SerializeField] private Item_ID_TG tG4;
    [SerializeField] private Item_ID_TG tG7;
    [Header("2")]
    [SerializeField] private Item_ID_TG tG2;
    [SerializeField] private Item_ID_TG tG5;
    [SerializeField] private Item_ID_TG tG8;
    [Header("3")]
    [SerializeField] private Item_ID_TG tG3;
    [SerializeField] private Item_ID_TG tG6;
    [SerializeField] private Item_ID_TG tG9;

    private void Start()
    {
        Maker[0, 0] = tG1;
        Maker[0, 1] = tG2;
        Maker[0, 2] = tG3;
        Maker[1, 0] = tG4;
        Maker[1, 1] = tG5;
        Maker[1, 2] = tG6;
        Maker[2, 0] = tG7;
        Maker[2, 1] = tG8;
        Maker[2, 2] = tG9;
    }




}
