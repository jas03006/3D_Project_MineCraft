using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatable : Item
{
    public int hungry_cure;
    private PlayerState_Y player;

    void Start()
    {
        player = FindObjectOfType<PlayerState_Y>();
    }

    public override void Act()
    {
        player.Hungry_cure(hungry_cure);
    }
}
