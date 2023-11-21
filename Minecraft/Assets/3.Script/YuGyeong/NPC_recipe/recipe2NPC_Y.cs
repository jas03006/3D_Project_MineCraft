using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "recipe2NPC_Y", menuName = "Scriptable Object/recipe2NPC_Y", order = int.MaxValue)]
public class recipe2NPC_Y : ScriptableObject
{
    [SerializeField] private List<NPC_recipe_Y> recipe_list;

    public NPC_recipe_Y get_recipe(int index)
    {
        NPC_recipe_Y recipe_Y = recipe_list[index];
        return recipe_Y;
    }
}
