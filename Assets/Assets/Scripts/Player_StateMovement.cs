using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StateMovement : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,       // ѕерсонаж стоит на месте
        MovingForward,
        MovingBackward,
        MovingRight,
        MovingLeft,
        MovingJump
    }

    private PlayerState currentState; // “екущее состо€ние игрока

    public PlayerState GetPlayerState()
    {
        return currentState; 
    }

    Player_Movement PlMove;

    void Start()
    {
        currentState = PlayerState.Idle; // »значально персонаж стоит на месте
        PlMove = gameObject.GetComponent<Player_Movement>();
    }

    void Update()
    {
        UpdatePlayerState(); // ќбновление состо€ни€ персонажа
    }

    // Ћогика изменени€ состо€ни€ игрока
    private void UpdatePlayerState()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ќпредел€ем состо€ние игрока в зависимости от направлени€ его движени€
        if (!PlMove.GetGround())
        {
            currentState = PlayerState.MovingJump;
        }
        else if(moveVertical > 0)
        {
            currentState = PlayerState.MovingForward;
        }
        else if (moveVertical < 0)
        {
            currentState = PlayerState.MovingBackward;
        }
        else if (moveHorizontal > 0)
        {
            currentState = PlayerState.MovingRight;
        }
        else if (moveHorizontal < 0)
        {
            currentState = PlayerState.MovingLeft;
        } 
        else
        {
            currentState = PlayerState.Idle; // ≈сли нет движени€, то состо€ние "—тоит на месте"
        }

        // ¬ыводим текущее состо€ние в консоль (можно использовать дл€ отладки)
        Debug.Log("Current State: " + currentState);
    }
}
