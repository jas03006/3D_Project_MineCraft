using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Y : MonoBehaviour
{
    [SerializeField] private NPC_UI_Y npc_UI;
    private int[] recipe_num = new int[2] { -1,-1};

    private void Start()
    {
        npc_UI = Inventory.instance.NPC_UI.GetComponent<NPC_UI_Y>();//FindObjectOfType<NPC_UI_Y>();
        
    }
    public void active()
    {
        make_num();
        npc_UI.set_index(recipe_num);
        Inventory.instance.show_NPC();        
    }

    private void make_num()
    {
        //랜덤으로 0부터 recipe2NPC_Y.recipe_list.Length-1까지 숫자 생성
        int max = npc_UI.recipe2NPC_Y.recipe_list.Count;
        if (recipe_num[0] == -1)
        {
            for (int i = 0; i < 2; i++)
            {
                int random = Random.Range(0, max);
                recipe_num[i] = random;
            }
        }
    }
}
