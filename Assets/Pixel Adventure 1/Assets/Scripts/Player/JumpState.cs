﻿using Unity.VisualScripting;
using UnityEngine;

public class JumpState : BaseState
{
    public override void EnterState(StateManager stateManager, PlayerController playerController)
    {
        stateManager.GetAnimator().SetInteger("state", (int)StateManager.EnumState.jump);
    }

    public override void ExitState(StateManager stateManager, PlayerController playerController)
    {

    }

    public override void UpdateState(StateManager stateManager, PlayerController playerController)
    {
        UpdateJumpLogic(stateManager, playerController);

        if (playerController.GetRigidbody2D().velocity.y < -0.1f)
            stateManager.ChangeState(stateManager.fallState);

        UpdateHorizontalLogic(stateManager, playerController);
    }

    void UpdateJumpLogic(StateManager stateManager, PlayerController playerController)
    {
        if (playerController.GetDirY() < 0)
        {
            if (playerController.GetIsOnGround())
            {
                playerController.GetRigidbody2D().velocity = new Vector2(playerController.GetRigidbody2D().velocity.x, playerController.GetvY());
                playerController.SetIsOnGround(false);
            }
        }
    }

    void UpdateHorizontalLogic(StateManager stateManager, PlayerController playerController)
    {
        if (playerController.GetDirX() != 0)
        {
            //Lật mặt
            if (playerController.GetDirX() < 0)
                stateManager.GetSpriteRenderer().flipX = true;
            else
                stateManager.GetSpriteRenderer().flipX = false;

            playerController.GetRigidbody2D().velocity = new Vector2(playerController.GetvX() * playerController.GetDirX(), playerController.GetRigidbody2D().velocity.y);
        }
    }

    public override void FixedUpdate(StateManager stateManager, PlayerController playerController)
    {

    }

    /*private void OnCollisionEnter2D(Collision collision)
    {
        if(collision.collider.CompareTag("Rock"))

    }*/

}
