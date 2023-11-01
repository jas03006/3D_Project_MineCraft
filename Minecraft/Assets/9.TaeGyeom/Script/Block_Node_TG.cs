using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Node_TG : MonoBehaviour
{
    public int id { get; private set;}
    public Block_Node_TG parent;
    public Block_Node_TG child;
    public Vector3 local_pos_in_chunk = Vector3.one * -1;
    public bool is_blocking = true;
    public void destroy()
    {
        if (parent != null) {
            parent.child = null;
            parent.destroy();
        }
        if (child != null)
        {
            child.parent = null;
            child.destroy();
        }
    }

    public void show_block()
    {
        gameObject.layer = 0;
        for (int ch = 0; ch < transform.childCount; ch++)
        {
            transform.GetChild(ch).gameObject.layer = 0;
        }
    }
    public void hide_block()
    {
        gameObject.layer = 0;
        for (int ch = 0; ch < transform.childCount; ch++)
        {
            transform.GetChild(ch).gameObject.layer = LayerMask.NameToLayer("Hidden_Block");
        }
    }

    public void set_local_pos(int x, int y, int z)
    {
        local_pos_in_chunk = new Vector3(x,y,z);
    }
}
