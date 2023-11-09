using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Armor_Data
{
    public float Armor;
}

public abstract class Armor_J : MonoBehaviour
{
    public Armor_Data armor_Data;
    public Item_ID_TG id;
    public Item_ID_TG Armor_ID;
    public bool Wearing = false;

    public abstract void information();

    public virtual void Wear()
    {
        if (Armor_ID == id)
        {
            Wearing = true;
        }
        if (Wearing)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }





}
