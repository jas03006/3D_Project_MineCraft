using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Attack : MonoBehaviour
{
    Animator animator;
    Collider col;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        col = GetComponent<Collider>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("ZombieAttack");
        }
    }

}
