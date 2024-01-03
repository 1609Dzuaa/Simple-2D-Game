﻿using UnityEngine;

//Snail sẽ 0 attack Player, thay vào đó nó sẽ rúc vào vỏ defend khi detected Player
public class SnailAttackState : MEnemiesAttackState
{
    private SnailManager _snailManager;
    private bool _hasChangedState;
    private bool _hasBeenHit;

    public bool HasBeenHit { set { _hasBeenHit = value; } }

    public override void EnterState(CharactersManager charactersManager)
    {
        base.EnterState(charactersManager);
        _snailManager = (SnailManager)charactersManager;
        Debug.Log("Attack");
        //Chỉnh lại Box Trigger khi Defend
    }

    public override void ExitState()
    {
        base.ExitState();
        _hasChangedState = false;
    }

    public override void Update()
    {
        if (!_snailManager.HasDetectedPlayer && !_hasChangedState)
        {
            _hasChangedState = true;
            if (!_hasBeenHit)
                _snailManager.Invoke("ChangeToIdle", _snailManager.DelayIdleTime);
            else
                _snailManager.Invoke("ChangeToIdle", _snailManager.DelayIdleAfterGotHit);
        }
        else if(_snailManager.HasDetectedPlayer)
        {
            _hasChangedState = false;
            _snailManager.CancelInvoke();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
