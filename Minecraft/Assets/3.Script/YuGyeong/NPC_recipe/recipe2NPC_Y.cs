using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recipe2NPC_Y : ScriptableObject
{
    [SerializeField] private List<NPC_recipe_Y> recipe_list;

    public NPC_recipe_Y get_recipe(int index)
    {
        NPC_recipe_Y recipe_Y = recipe_list[index];
        return recipe_Y;
    }
}
