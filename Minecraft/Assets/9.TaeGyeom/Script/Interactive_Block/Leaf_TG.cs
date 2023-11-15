using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf_TG : Block_TG {

    public override void itemposition()
    {
        if (Random.Range(0,4)==0) {
            base.itemposition();
        }        
    }
}
