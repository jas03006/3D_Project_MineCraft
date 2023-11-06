using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_TG : Block_TG, Interactive_TG
{
    private bool is_open = false;
    [SerializeField] private Animator animator;
    public void react()
    {
        is_open = !is_open;
        animator.SetBool("Is_Open",is_open);
    }
}
