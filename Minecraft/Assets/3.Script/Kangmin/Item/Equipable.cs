using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//공격,방어를 할지 안할지 몰라서 후순위로 두기로 함

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
