using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����,�� ���� ������ ���� �ļ����� �α�� ��

public class Equipable : Item
{
    public int defense;
    private PlayerState_Y player;
    
    void Start()
    {
        player = FindObjectOfType<PlayerState_Y>();
    }

    public override void Act()
    {

    }
}
