using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Node_TG : MonoBehaviour
{
    public int id { get; private set;}
    public Block_Node_TG parent;
    public Block_Node_TG child;

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
}
