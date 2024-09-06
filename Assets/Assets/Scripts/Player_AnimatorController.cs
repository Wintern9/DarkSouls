using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player_StateMovement;

public class Player_AnimatorController : MonoBehaviour
{
    Animator animator;
    public Player_StateMovement playerState;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    PlayerState currentState;

    void Update()
    {
        currentState = playerState.GetPlayerState();
        PlayerAnimationRun();
    }

    public void PlayerAnimationRun()
    {
        if (PlayerState.MovingForward == currentState || PlayerState.MovingRight == currentState || PlayerState.MovingLeft == currentState || PlayerState.MovingBackward == currentState)
        {
            animator.SetBool("Run", true);
        }
        else if (PlayerState.MovingJump == currentState)
            animator.SetBool("Jump", true);
        else if (PlayerState.Idle == currentState)
        {
            animator.SetBool("Run", false);
        }
    }
}
