﻿using UnityEngine;

public class WallJumpState : PlayerBaseState
{
    //2 biến để dùng đếm giờ và đánh dấu hết bị disable input directionX
    private float _disableStart;
    private bool _isEndDisable;

    public bool IsEndDisable { get { return _isEndDisable; } }

    public override void EnterState(PlayerStateManager playerStateManager)
    {
        base.EnterState(playerStateManager);
        _playerStateManager.GetAnimator().SetInteger("state", (int)EnumState.EPlayerState.wallJump);
        _playerStateManager.FlipSpriteAfterWallSlide();
        HandleWallJump();
        //Debug.Log("WallJump");
        _disableStart = Time.time;
    }

    public override void ExitState()
    {
        _isEndDisable = false;
    }

    public override void Update()
    {
        //Okay on? r`
        if (Time.time - _disableStart >= _playerStateManager.DisableTime)
            _isEndDisable = true;

        if (CheckIfCanDbJump())
            _playerStateManager.ChangeState(_playerStateManager.doubleJumpState);
        else if (CheckIfCanFall())
            _playerStateManager.ChangeState(_playerStateManager.fallState);
        else if (CheckIfCanWallSlide())
            _playerStateManager.ChangeState(_playerStateManager.wallSlideState);
        else if (CheckIfCanDash())
            _playerStateManager.ChangeState(_playerStateManager.dashState);

    }

    private bool CheckIfCanDbJump()
    {
        //Press Space While WallJump => Double Jump
        return Input.GetButtonDown("Jump");
    }

    private bool CheckIfCanFall()
    {
        return _playerStateManager.GetRigidBody2D().velocity.y < -0.1f;
    }

    private bool CheckIfCanWallSlide()
    {
        return _playerStateManager.GetIsWallTouch() && _isEndDisable;
    }

    private bool CheckIfCanDash()
    {
        return Input.GetKeyDown(KeyCode.E)
            && Time.time - _playerStateManager.dashState.DashDelayStart >= _playerStateManager.GetPlayerStats.DelayDashTime
            || Input.GetKeyDown(KeyCode.E) && _playerStateManager.dashState.IsFirstTimeDash;
    }

    public override void FixedUpdate()
    {
        if (_isEndDisable)
        {
            if (_playerStateManager.GetDirX() != 0)
            {
                if (!PlayerSpeedBuff.Instance.IsAllowToUpdate)
                    _playerStateManager.GetRigidBody2D().velocity = new Vector2(_playerStateManager.GetPlayerStats.SpeedX * _playerStateManager.GetDirX(), _playerStateManager.GetRigidBody2D().velocity.y);
                else
                    _playerStateManager.GetRigidBody2D().velocity = new Vector2(_playerStateManager.GetPlayerStats.SpeedX * _playerStateManager.GetDirX() * PlayerSpeedBuff.Instance.SpeedMultiplier, _playerStateManager.GetRigidBody2D().velocity.y);
            }
        }
    }

    private void HandleWallJump()
    {
        //Nếu nhảy khi đang trượt tường thì nhảy xéo
        if (_playerStateManager.WallHit.normal.x >= 1f)
            _playerStateManager.GetRigidBody2D().velocity = new Vector2(_playerStateManager.GetPlayerStats.WallJumpSpeed.x, _playerStateManager.GetPlayerStats.WallJumpSpeed.y);
        else if (_playerStateManager.WallHit.normal.x <= -1f)
            _playerStateManager.GetRigidBody2D().velocity = new Vector2(-_playerStateManager.GetPlayerStats.WallJumpSpeed.x, _playerStateManager.GetPlayerStats.WallJumpSpeed.y);

        _playerStateManager.GetJumpSound().Play();
    }
}
