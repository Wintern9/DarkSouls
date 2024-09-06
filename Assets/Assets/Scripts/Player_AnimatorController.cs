using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimatorController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void PlayerAnimationRun()
    {
        animator.SetBool("Run", !animator.GetBool("Run"));
    }
}
