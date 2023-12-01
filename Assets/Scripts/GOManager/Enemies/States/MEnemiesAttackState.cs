﻿using UnityEngine;

public class MEnemiesAttackState : MEnemiesBaseState
{

    public override void EnterState(CharactersManager charactersManager)
    {
        base.EnterState(charactersManager);
        _mEnemiesManager.Animator.SetInteger("state", (int)EnumState.EMEnemiesState.attack);
        Debug.Log("Attack");
    }

    public override void ExitState()
    {
        
    }

    public override void Update()
    {
        LogicUpdate();
    }

    private void LogicUpdate()
    {
        if (!_mEnemiesManager.HasDetectedPlayer)
        {
            //Debug.Log("Here");
            _mEnemiesManager.ChangeState(_mEnemiesManager.MEnemiesIdleState);
        }
    }

    public override void FixedUpdate()
    {
        Attack();
    }

    protected virtual void Attack()
    {
        //Base Attack will be chasing Player
        //Any Attack State derived from this class can ovveride this func
        //to perform Different Attack
        _mEnemiesManager.GetRigidbody2D().velocity = _mEnemiesManager.GetChaseSpeed();
    }

}
