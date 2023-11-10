using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_TG : Block_TG, Interactive_TG
{
    //private bool is_open = false;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        is_open = false;
    }
    public void react()
    {
        is_open = !is_open;
        animator.SetBool("Is_Open",is_open);
    }
    public override void init(bool is_open_) {
        is_open = is_open_;
        animator.SetBool("Is_Open", is_open);
    }
}
