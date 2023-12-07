using UnityEngine;

public class SnailIdleState : MEnemiesIdleState
{
    private SnailManager _snailManager;

    public override void EnterState(CharactersManager charactersManager)
    {
        base.EnterState(charactersManager);
        _snailManager = (SnailManager)charactersManager;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void Update()
    {
        if (CheckIfCanPatrol())
            _snailManager.ChangeState(_snailManager.SnailPatrolState);
        else if (CheckIfCanAttack())
            _snailManager.Invoke("AllowAttackPlayer", _snailManager.GetAttackDelay());
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}