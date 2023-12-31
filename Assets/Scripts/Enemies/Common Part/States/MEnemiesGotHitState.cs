﻿using UnityEngine;

public class MEnemiesGotHitState : MEnemiesBaseState
{
    public override void EnterState(CharactersManager charactersManager)
    {
        base.EnterState(charactersManager);
        _mEnemiesManager.Animator.SetInteger(GameConstants.ANIM_PARA_STATE, (int)GameEnums.EMEnemiesState.gotHit);
        HandleBeforeDestroy();
        //Debug.Log("GH");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void KnockUpLeft()
    {
        _mEnemiesManager.GetRigidbody2D().AddForce(_mEnemiesManager.KnockForce);
        //Debug.Log("Knock");
    }

    protected virtual void HandleBeforeDestroy()
    {
        _mEnemiesManager.GetRigidbody2D().velocity = Vector2.zero; //Cố định vị trí
        KnockUpLeft();
        _mEnemiesManager.GetCollider2D.enabled = false;
    }
}
