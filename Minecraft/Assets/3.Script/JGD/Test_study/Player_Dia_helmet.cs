using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dia_helmet : Armor_J
{
    public override void information()
    {
        armor_Data.Armor = 1.5f;

    }

    public override void Wear()
    {
        base.Wear();
    }
}
