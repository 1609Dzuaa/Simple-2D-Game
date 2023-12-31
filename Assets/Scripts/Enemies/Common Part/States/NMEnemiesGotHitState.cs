using UnityEngine;

public class NMEnemiesGotHitState : NMEnemiesBaseState
{
    protected float Zdegree = 0f;
    protected float lastRotateTime;

    public override void EnterState(CharactersManager charactersManager)
    {
        base.EnterState(charactersManager);
        _nmEnemiesManager.Animator.SetInteger(GameConstants.ANIM_PARA_STATE, (int)GameEnums.ENMEnemiesState.gotHit);
        _nmEnemiesManager.GetCollider2D.enabled = false;
        lastRotateTime = Time.time;
        ApplyForce();
        //Debug.Log("GH");
    }

    public override void ExitState() { }

    public override void Update()
    {
        if (Time.time - lastRotateTime >= _nmEnemiesManager.GetTimeEachRotate())
        {
            Zdegree -= _nmEnemiesManager.GetDegreeEachRotation();
            _nmEnemiesManager.transform.Rotate(0f, 0f, Zdegree);
            lastRotateTime = Time.time;
        }
    }

    protected void ApplyForce()
    {
        _nmEnemiesManager.GetRigidbody2D().AddForce(_nmEnemiesManager.KnockForce, ForceMode2D.Impulse);
    }
}
